using System;
using System.Drawing;
using System.Windows.Forms;
using RPG.DoiTuong;

namespace RPG.Controls
{
    // Nút admin để test, dùng chung cho màn Chỉ số và Cây kỹ năng
    public class AdminPanel : GroupBox
    {
        private readonly Player player;

        // Báo cho màn hình chứa nó vẽ lại sau khi bấm nút
        public event EventHandler Changed;

        public AdminPanel(Player player)
        {
            this.player = player;

            Text = "Admin (dùng để test)";
            Size = new Size(400, 70);
            BackColor = ColorTranslator.FromHtml("#FFF4E5");
            ForeColor = ColorTranslator.FromHtml("#8A4B08");

            AddButton("+1 Cấp", 12, () => player.LevelUp());
            AddButton("-1 Cấp", 137, () => player.LevelDown());
            AddButton("Reset chỉ số + cây", 262, () => player.ResetBuild());
        }

        private void AddButton(string text, int left, Action action)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(left, 25),
                Size = new Size(125, 32),
                BackColor = ColorTranslator.FromHtml("#FFFFFF"),
                ForeColor = ColorTranslator.FromHtml("#333333")
            };
            button.Click += (_, _) =>
            {
                action();
                Changed?.Invoke(this, EventArgs.Empty);
            };
            Controls.Add(button);
        }
    }
}
