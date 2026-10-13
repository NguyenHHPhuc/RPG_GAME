using RPG.DoiTuong;
using RPG.Services;

namespace RPG.Controls
{
    public partial class SaveSlotControl : UserControl
    {
        private readonly bool creatingNewGame;
        private List<SaveSlot?> slots;
        private int selectedSlotIndex;
        private int lastClickedSlotIndex = -1;
        private DateTime lastSlotClickTime;

        public event EventHandler<SaveSlotEventArgs>? GameReady;
        public event EventHandler? BackClicked;

        public SaveSlotControl(bool creatingNewGame)
        {
            InitializeComponent();
            this.creatingNewGame = creatingNewGame;
            slots = SaveSlotService.LoadSlots();

            lblTitle.Text = creatingNewGame ? "Tạo nhân vật mới" : "Chọn nhân vật";
            lblSubtitle.Text = creatingNewGame
                ? "Chọn một ô trống, đặt tên và chọn lớp nhân vật."
                : "Chọn một ô đã lưu để tiếp tục cuộc phiêu lưu.";
            btnAction.Text = creatingNewGame ? "Tạo nhân vật" : "Vào game";
            cmbPlayerClass.SelectedIndex = 0;

            RefreshSlotButtons();
            selectedSlotIndex = FindDefaultSlot();
            ShowSelectedSlot();
        }

        private int FindDefaultSlot()
        {
            int index = creatingNewGame
                ? slots.FindIndex(slot => slot == null)
                : slots.FindIndex(slot => slot != null);
            return index >= 0 ? index : 0;
        }

        private void RefreshSlotButtons()
        {
            Button[] buttons = { btnSlot1, btnSlot2, btnSlot3 };
            for (int i = 0; i < buttons.Length; i++)
            {
                SaveSlot? slot = slots[i];
                buttons[i].Tag = i;
                buttons[i].Text = slot == null
                    ? $"Ô lưu {i + 1}\r\nTrống"
                    : $"Ô lưu {i + 1}\r\n{slot.State.Player.Name} - {slot.State.Player.ClassName}";
                buttons[i].BackColor = i == selectedSlotIndex
                    ? Color.FromArgb(220, 235, 250)
                    : Color.White;
            }
        }

        private void ShowSelectedSlot()
        {
            RefreshSlotButtons();
            SaveSlot? slot = slots[selectedSlotIndex];
            bool isEmpty = slot == null;

            if (slot != null)
            {
                txtPlayerName.Text = slot.State.Player.Name;
                int classIndex = cmbPlayerClass.Items.IndexOf(slot.State.Player.ClassName);
                cmbPlayerClass.SelectedIndex = classIndex >= 0 ? classIndex : 0;
                lblDetailsTitle.Text = $"Ô lưu {selectedSlotIndex + 1}";
            }
            else
            {
                txtPlayerName.Text = "";
                lblDetailsTitle.Text = $"Ô lưu {selectedSlotIndex + 1} - Trống";
            }

            bool canCreate = creatingNewGame && isEmpty;
            txtPlayerName.Enabled = canCreate;
            cmbPlayerClass.Enabled = canCreate;
            btnAction.Enabled = creatingNewGame ? canCreate : !isEmpty;
            btnAction.Text = creatingNewGame
                ? (isEmpty ? "Tạo nhân vật" : "Ô lưu đã có dữ liệu")
                : (isEmpty ? "Ô lưu đang trống" : "Vào game");
            btnDelete.Enabled = !isEmpty;
            lblClassDescription.Text = GetClassDescription(cmbPlayerClass.Text);
        }

        private static string GetClassDescription(string className)
        {
            return className switch
            {
                "Cung thủ" => "Máu thấp hơn, nhưng sát thương và năng lượng cao.",
                "Đỡ đòn" => "Máu và phòng thủ cao, phù hợp đứng vững trong giao tranh.",
                _ => "Chỉ số cân bằng, phù hợp cho lần chơi đầu tiên."
            };
        }

        private void btnSlot_Click(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int index)
            {
                DateTime now = DateTime.UtcNow;
                bool doubleClicked = index == lastClickedSlotIndex
                    && (now - lastSlotClickTime).TotalMilliseconds <= SystemInformation.DoubleClickTime;
                selectedSlotIndex = index;
                ShowSelectedSlot();

                if (doubleClicked)
                {
                    lastClickedSlotIndex = -1;
                    if (!creatingNewGame)
                        OpenSelectedSlot();
                }
                else
                {
                    lastClickedSlotIndex = index;
                    lastSlotClickTime = now;
                }
            }
        }

        private void cmbPlayerClass_SelectedIndexChanged(object? sender, EventArgs e)
        {
            lblClassDescription.Text = GetClassDescription(cmbPlayerClass.Text);
        }

        private void btnAction_Click(object? sender, EventArgs e)
        {
            if (creatingNewGame)
            {
                string playerName = txtPlayerName.Text.Trim();
                if (playerName.Length == 0)
                {
                    MessageBox.Show("Bạn hãy nhập tên nhân vật.", "Thiếu tên nhân vật",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPlayerName.Focus();
                    return;
                }

                GameState newGame = GameState.CreateNewGame(playerName, cmbPlayerClass.Text);
                GameReady?.Invoke(this, new SaveSlotEventArgs(selectedSlotIndex + 1, newGame));
                return;
            }

            OpenSelectedSlot();
        }

        private void OpenSelectedSlot()
        {
            SaveSlot? selected = slots[selectedSlotIndex];
            if (selected != null)
                GameReady?.Invoke(this, new SaveSlotEventArgs(selected.SlotNumber, selected.State));
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (slots[selectedSlotIndex] == null)
                return;

            DialogResult answer = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân vật trong ô lưu này không?",
                "Xóa ô lưu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
                return;

            SaveSlotService.DeleteSlot(selectedSlotIndex + 1);
            slots = SaveSlotService.LoadSlots();
            ShowSelectedSlot();
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            BackClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class SaveSlotEventArgs : EventArgs
    {
        public int SlotNumber { get; }
        public GameState State { get; }

        public SaveSlotEventArgs(int slotNumber, GameState state)
        {
            SlotNumber = slotNumber;
            State = state;
        }
    }
}
