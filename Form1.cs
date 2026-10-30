using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using RPG.Controls;
using RPG.DoiTuong;
using RPG.Services;

namespace RPG
{
    public partial class Form1 : Form
    {
        
        GameState gameState;
        private UserControl currentControl;
        private MapService mapService = new MapService();
        private readonly SaveGameService saveGameService = new SaveGameService();
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            // Hiển thị menu chính khi Form1 được khởi tạo
            ShowMainMenu();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentControl is MapControl map)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        map.SetMoveUp(true);
                        break;

                    case Keys.S:
                        map.SetMoveDown(true);
                        break;

                    case Keys.A:
                        map.SetMoveLeft(true);
                        break;

                    case Keys.D:
                        map.SetMoveRight(true);
                        break;
                }
            }
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentControl is MapControl map)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        map.SetMoveUp(false);
                        break;

                    case Keys.S:
                        map.SetMoveDown(false);
                        break;

                    case Keys.A:
                        map.SetMoveLeft(false);
                        break;

                    case Keys.D:
                        map.SetMoveRight(false);
                        break;
                }
            }
        }


        private void ShowMainMenu()
        {
            MainMenuControl menu = new MainMenuControl();
            // Đăng ký các sự kiện từ menu để xử lý khi người dùng nhấn các nút
            // Khi người dùng nhấn nút "New Game", sự kiện NewGameClicked sẽ được kích hoạt và gọi phương thức Menu_NewGameClicked và tương tự cho các nút khác
            menu.NewGameClicked += Menu_NewGameClicked;
            menu.LoadGameClicked += Menu_LoadGameClicked;
            menu.ExitClicked += Menu_ExitClicked;


            // Hiển thị menu chính trong panelMain
            ShowControl(menu);
        }


        public void Menu_NewGameClicked(object sender, EventArgs e)
        {

            gameState = GameState.CreateNewGame();
            ShowMap();
        }
        public void Menu_LoadGameClicked(object sender, EventArgs e)
        {
            using Form loadDialog = new Form
            {
                Text = "Quản lý bản lưu",
                ClientSize = new Size(900, 470),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lblInfo = new Label
            {
                Text = "Chọn một bản lưu để tải hoặc xóa:",
                Location = new Point(15, 15),
                Size = new Size(700, 25)
            };

            ListView saveList = new ListView
            {
                Location = new Point(15, 48),
                Size = new Size(870, 350),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false,
                HideSelection = false,
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            saveList.Columns.Add("Tên bản lưu", 230);
            saveList.Columns.Add("Bản đồ", 160);
            saveList.Columns.Add("Trạng thái", 150);
            saveList.Columns.Add("Thời gian lưu", 210);

            Button btnLoad = new Button
            {
                Text = "Tải bản lưu",
                Size = new Size(115, 32)
            };

            Button btnDelete = new Button
            {
                Text = "Xóa bản lưu",
                Size = new Size(115, 32)
            };

            Button btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(90, 32),
                DialogResult = DialogResult.Cancel
            };

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Location = new Point(15, 412),
                Size = new Size(870, 42),
                Anchor = AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            buttonPanel.Controls.Add(btnClose);
            buttonPanel.Controls.Add(btnDelete);
            buttonPanel.Controls.Add(btnLoad);

            loadDialog.Controls.Add(lblInfo);
            loadDialog.Controls.Add(saveList);
            loadDialog.Controls.Add(buttonPanel);

            // Nạp danh sách từ SQL Server.
            try
            {
                RefreshSaveList(saveList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Không thể đọc danh sách bản lưu.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (saveList.Items.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "Chưa có bản lưu nào.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int selectedSaveId = 0;

            // TẢI BẢN LƯU
            btnLoad.Click += (_, _) =>
            {
                if (saveList.SelectedItems.Count == 0)
                {
                    MessageBox.Show(
                        loadDialog,
                        "Vui lòng chọn bản lưu cần tải.");

                    return;
                }

                selectedSaveId = Convert.ToInt32(
                    saveList.SelectedItems[0].Tag);

                loadDialog.DialogResult = DialogResult.OK;
                loadDialog.Close();
            };

            // XÓA BẢN LƯU
            btnDelete.Click += (_, _) =>
            {
                if (saveList.SelectedItems.Count == 0)
                {
                    MessageBox.Show(
                        loadDialog,
                        "Vui lòng chọn bản lưu cần xóa.");

                    return;
                }

                ListViewItem selectedItem =
                    saveList.SelectedItems[0];

                int saveId = Convert.ToInt32(selectedItem.Tag);
                string saveName = selectedItem.Text;

                DialogResult result = MessageBox.Show(
                    loadDialog,
                    $"Bạn có chắc muốn xóa bản lưu '{saveName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    bool deleted = saveGameService.DeleteSave(saveId);

                    if (!deleted)
                    {
                        MessageBox.Show(
                            loadDialog,
                            "Bản lưu không tồn tại hoặc đã bị xóa.");

                        return;
                    }

                    // Làm mới danh sách sau khi xóa.
                    RefreshSaveList(saveList);

                    MessageBox.Show(
                        loadDialog,
                        $"Đã xóa bản lưu '{saveName}'.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        loadDialog,
                        "Không thể xóa bản lưu.\n" + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };

            loadDialog.AcceptButton = btnLoad;
            loadDialog.CancelButton = btnClose;

            if (loadDialog.ShowDialog(this) != DialogResult.OK ||
                selectedSaveId == 0)
            {
                return;
            }

            // Tải trạng thái trò chơi đã chọn.
            try
            {
                gameState = saveGameService.LoadGame(selectedSaveId);

                if (gameState.IsGameWon)
                {
                    ShowVictory();
                }
                else
                {
                    ShowMap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Không thể tải bản lưu.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void RefreshSaveList(ListView saveList)
        {
            saveList.Items.Clear();

            List<GameSave> saves = saveGameService.GetAllSaves();

            foreach (GameSave save in saves)
            {
                string status = save.IsGameWon
                    ? "Đã chiến thắng"
                    : "Chưa hoàn thành";

                ListViewItem item = new ListViewItem(save.SaveName);

                item.SubItems.Add(save.CurrentMapId);
                item.SubItems.Add(status);
                item.SubItems.Add(
                    save.SavedAt.ToString("dd/MM/yyyy HH:mm:ss"));

                // Giữ Id thật của bản lưu, không dựa vào vị trí trong danh sách.
                item.Tag = save.Id;

                saveList.Items.Add(item);
            }
        }
        public void Menu_ExitClicked(object sender, EventArgs e)
        {
            Close();
        }
        private void ShowControl(UserControl control)
        {
            // Nếu có control hiện tại, hủy nó trước khi hiển thị control mới
            if (currentControl != null)
            {
                currentControl.Dispose();
                currentControl = null;
            }
            panelMain.Controls.Clear();
            currentControl = control;

            control.Dock = DockStyle.Fill;

            panelMain.Controls.Add(control);

        }
        public void Map_BattleRequested(object sender, EventArgs e)
        {
            // Khi sự kiện BattleRequested được kích hoạt từ MapControl, hiển thị BattleControl
            ShowBattle();
        }
        public void Map_InventoryRequested(object sender, EventArgs e)
        {
            // Khi sự kiện InventoryRequested được kích hoạt từ MapControl, hiển thị InventoryControl
            ShowInventory();
        }
        public void Map_HomeRequested(object sender, EventArgs e)
        {
            // Khi sự kiện HomeRequested được kích hoạt từ MapControl, hiển thị HomeControl
            ShowHome();
        }
        private void Map_MapExitRequested(object sender, EventArgs e)
        {
            // Lấy dữ liệu Map hiện tại
            MapData currentMap = mapService.GetMap(gameState.CurrentMapId);

            if (currentMap == null)
            {
                MessageBox.Show("Không tìm thấy Map hiện tại.");
                return;
            }

            // Từ Start Area: vào tầng cao nhất đã mở khóa
            if (!currentMap.IsDungeon)
            {
                int floor = gameState.HighestUnlockedFloor;

                if (floor < 1 || floor > 2)
                {
                    MessageBox.Show("Không có tầng Dungeon hợp lệ.");
                    return;
                }

                MapData nextMap = mapService.GetMap(
                    $"DungeonFloor{floor}"
                );

                if (nextMap == null)
                {
                    MessageBox.Show("Không tìm thấy tầng Dungeon.");
                    return;
                }

                gameState.CurrentDungeonFloor = floor;
                gameState.CurrentMapId = nextMap.Id;

                gameState.Player.X = nextMap.EntryX;
                gameState.Player.Y = nextMap.EntryY;

                ShowMap();
                return;
            }

            // Đang ở Dungeon: kiểm tra còn quái không
            bool hasRemainingEnemies = gameState.Enemies.Any(
                enemy => enemy.MapId == currentMap.Id
            );

            if (hasRemainingEnemies)
            {
                MessageBox.Show( "Hãy đánh bại tất cả quái vật trước khi rời tầng." );
                if(currentControl is MapControl map)
                {
                    map.ResumeMovement();
                }
                return;
            }

            int clearedFloor = currentMap.FloorNumber;

            if (!gameState.ClearedFloors.Contains(clearedFloor))
            {
                gameState.ClearedFloors.Add(clearedFloor);
            }

            // Mở khóa tầng kế tiếp, tối đa tầng 2
            if (clearedFloor < 2)
            {
                gameState.HighestUnlockedFloor = Math.Max(
                    gameState.HighestUnlockedFloor,
                    clearedFloor + 1
                );
            }

            // Sau khi hoàn thành tầng, quay về Start Area
            MapData startArea = mapService.GetMap("StartArea");

            if (startArea == null)
            {
                MessageBox.Show("Không tìm thấy Start Area.");
                return;
            }

            gameState.CurrentMapId = startArea.Id;

            gameState.Player.X = startArea.EntryX;
            gameState.Player.Y = startArea.EntryY;

            ShowMap();
        }

        private void ShowMap()
        {
            MapData mapData = mapService.GetMap(gameState.CurrentMapId);
            // Tạo một instance của MapControl và truyền gameState vào constructor
            MapControl map = new MapControl(gameState, mapData);
            
            // Đăng ký sự kiện BattleRequested từ MapControl để xử lý khi người chơi gặp kẻ thù
            map.BattleRequested += Map_BattleRequested;
            map.InventoryRequested += Map_InventoryRequested;
            map.HomeRequested += Map_HomeRequested;
            map.MapExitRequested += Map_MapExitRequested;
            map.ShopRequested += Map_ShopRequested;
            map.SaveRequested += Map_SaveRequested;
            map.StatsRequested += (_, _) => ShowStats();
            map.SkillTreeRequested += (_, _) => ShowSkillTree();
            // Hiển thị MapControl trong panelMain
            ShowControl(map);
        }
        private void ShowBattle()
        {
            // Tạo một instance của BattleControl và truyền gameState vào constructor
            BattleControl battle = new BattleControl(gameState);
            battle.BattleWon += Battle_BattleWon;
            battle.BattleLost += Battle_BattleLost;
            battle.GameWon += Battle_GameWon;

            // Đăng ký sự kiện tự động lưu khi thắng Boss cuối
            battle.AutoSaveRequested += Battle_AutoSaveRequested;
            // Hiển thị BattleControl trong panelMain
            ShowControl(battle);
        }
        private void Battle_BattleWon(object sender, EventArgs e)
        {
            // Khi người chơi thắng trận đấu, hiển thị bản đồ trở lại
            ShowMap();
        }
        private void Battle_BattleLost(object sender, EventArgs e)
        {
            gameState = GameState.CreateNewGame();

            ShowMainMenu();
        }
        private void Battle_GameWon(object sender, EventArgs e)
        {
            ShowVictory();
        }
        private void Battle_AutoSaveRequested(object sender, EventArgs e)
        {
            try
            {
                // Lưu riêng vào ô Auto Save
                saveGameService.SaveGame(gameState, "Auto Save");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Bạn đã đánh bại Boss cuối nhưng không thể tự động lưu game.\n" +
                    "Bạn vẫn có thể tiếp tục xem màn hình chiến thắng.\n\n" +
                    "Chi tiết lỗi: " + ex.Message,
                    "Lỗi tự động lưu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void ShowInventory()
        {
            InventoryControl inventory = new InventoryControl(gameState);
            inventory.CloseClicked += Inventory_CloseClicked;
            ShowControl(inventory);
        }
        private void Inventory_CloseClicked(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void ShowHome()
        {
            HomeControl home = new HomeControl(gameState);

            home.StartAdventureClicked += Home_StartAdventureClicked;
            home.InventoryClicked += Home_InventoryClicked;
            home.BackToMainMenuClicked += Home_BackToMainMenuClicked;

            ShowControl(home);
        }
        private void Home_StartAdventureClicked(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void Home_InventoryClicked(object sender, EventArgs e)
        {
            ShowInventory();
        }
        private void Home_BackToMainMenuClicked(object sender, EventArgs e)
        {
            ShowMainMenu();
        }
        private void ShowVictory()
        {
            VictoryControl victory = new VictoryControl();

            victory.BackToMainMenuClicked += Victory_BackToMainMenuClicked;

            ShowControl(victory);
        }
        private void Victory_BackToMainMenuClicked(object sender, EventArgs e)
        {
            ShowMainMenu();
        }
        private void Map_ShopRequested(object sender, EventArgs e)
        {
            ShowShop();
        }
        private void ShowShop()
        {
            ShopControl shop = new ShopControl(gameState);

            shop.CloseClicked += Shop_CloseClicked;

            ShowControl(shop);
        }
        private void Shop_CloseClicked(object sender, EventArgs e)
        {
            ShowMap();
        }

        private void ShowStats()
        {
            StatsControl stats = new StatsControl(gameState);
            stats.CloseClicked += (_, _) => ShowMap();
            ShowControl(stats);
        }

        private void ShowSkillTree()
        {
            SkillTreeControl skillTree = new SkillTreeControl(gameState);
            skillTree.CloseClicked += (_, _) => ShowMap();
            ShowControl(skillTree);
        }
        private void Map_SaveRequested(object sender, EventArgs e)
        {
            using Form prompt = new Form
            {
                Text = "Lưu trò chơi",
                ClientSize = new Size(440, 160),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lblName = new Label
            {
                Text = "Nhập tên bản lưu:",
                Location = new Point(15, 15),
                AutoSize = true
            };

            TextBox txtName = new TextBox
            {
                Location = new Point(15, 42),
                Width = 405
            };

            Button btnSave = new Button
            {
                Text = "Lưu",
                Location = new Point(235, 90),
                Size = new Size(85, 32)
            };

            Button btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(335, 90),
                Size = new Size(85, 32),
                DialogResult = DialogResult.Cancel
            };

            btnSave.Click += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show(
                        prompt,
                        "Thông báo",
                        "Vui lòng nhập tên bản lưu.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtName.Focus();
                    return;
                }

                prompt.DialogResult = DialogResult.OK;
                prompt.Close();
            };

            prompt.Controls.Add(lblName);
            prompt.Controls.Add(txtName);
            prompt.Controls.Add(btnSave);
            prompt.Controls.Add(btnCancel);

            prompt.AcceptButton = btnSave;
            prompt.CancelButton = btnCancel;

            if (prompt.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string saveName = txtName.Text.Trim();

            // Dành riêng tên Auto Save cho chức năng tự động lưu.
            if (string.Equals(
                saveName,
                "Auto Save",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    this,
                    "Tên Auto Save được dành riêng cho hệ thống tự động lưu.",
                    "Không thể lưu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Kiểm tra tên này đã tồn tại chưa.
                bool exists = saveGameService.GetAllSaves()
                    .Any(s => string.Equals(
                        s.SaveName,
                        saveName,
                        StringComparison.OrdinalIgnoreCase));

                if (exists)
                {
                    DialogResult result = MessageBox.Show(
                        this,
                        $"Bản lưu '{saveName}' đã tồn tại.\n" +
                        "Bạn có muốn ghi đè bản lưu này không?",
                        "Xác nhận ghi đè",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                int saveId = saveGameService.SaveGame(
                    gameState,
                    saveName);

                MessageBox.Show(
                    this,
                    $"Đã lưu trò chơi thành công!\n" +
                    $"Tên bản lưu: {saveName}\n" +
                    $"Mã bản lưu: {saveId}",
                    "Lưu trò chơi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Không thể lưu trò chơi.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
