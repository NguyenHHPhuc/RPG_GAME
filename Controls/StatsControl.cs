using System;
using System.Drawing;
using System.Windows.Forms;
using RPG.DoiTuong;

namespace RPG.Controls
{
    // Màn hình cộng điểm chỉ số, giao diện tạo bằng code
    public class StatsControl : UserControl
    {
        private readonly Player player;

        private readonly Label lblPoints = new Label();
        private readonly Label lblHealth = new Label();
        private readonly Label lblAttack = new Label();
        private readonly Label lblDefense = new Label();
        private readonly Label lblSummary = new Label();
        private readonly Button btnAddHealth;
        private readonly Button btnAddAttack;
        private readonly Button btnAddDefense;

        public event EventHandler CloseClicked;

        public StatsControl(GameState gameState)
        {
            player = gameState.Player;
            BackColor = ColorTranslator.FromHtml("#F4F1E8");
            Font = new Font("Segoe UI", 11F);

            AddLabel(new Label(), "CHỈ SỐ NHÂN VẬT", 40, 25, 500, 36,
                new Font("Segoe UI", 18F, FontStyle.Bold), "#3B2F2F");
            AddLabel(lblPoints, "", 40, 70, 500, 28,
                new Font("Segoe UI", 12F, FontStyle.Bold), "#C0392B");

            // Mỗi dòng: tên chỉ số + nút "+"
            AddLabel(lblHealth, "", 40, 120, 480, 30, Font, "#E05656");
            btnAddHealth = AddPlusButton(530, 116, StatType.Health);
            AddLabel(lblAttack, "", 40, 165, 480, 30, Font, "#E67E22");
            btnAddAttack = AddPlusButton(530, 161, StatType.Attack);
            AddLabel(lblDefense, "", 40, 210, 480, 30, Font, "#4A90E2");
            btnAddDefense = AddPlusButton(530, 206, StatType.Defense);

            AddLabel(lblSummary, "", 40, 260, 560, 60, Font, "#555555");

            AdminPanel adminPanel = new AdminPanel(player) { Location = new Point(40, 335) };
            adminPanel.Changed += (_, _) => RefreshStats();
            Controls.Add(adminPanel);

            Button btnBack = new Button
            {
                Text = "Quay lại bản đồ",
                Location = new Point(40, 425),
                Size = new Size(180, 40),
                BackColor = ColorTranslator.FromHtml("#FFFFFF")
            };
            btnBack.Click += (_, _) => CloseClicked?.Invoke(this, EventArgs.Empty);
            Controls.Add(btnBack);

            RefreshStats();
        }

        private void AddLabel(Label label, string text, int x, int y, int width, int height, Font font, string hexColor)
        {
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(width, height);
            label.Font = font;
            label.ForeColor = ColorTranslator.FromHtml(hexColor);
            Controls.Add(label);
        }

        private Button AddPlusButton(int x, int y, StatType stat)
        {
            Button button = new Button
            {
                Text = "+",
                Location = new Point(x, y),
                Size = new Size(44, 34),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = ColorTranslator.FromHtml("#FFFFFF")
            };
            button.Click += (_, _) =>
            {
                player.AddStatPoint(stat);
                RefreshStats();
            };
            Controls.Add(button);
            return button;
        }

        private void RefreshStats()
        {
            lblPoints.Text = $"Điểm chỉ số còn lại: {player.StatPoints}   (Cấp {player.Level}, mỗi cấp +{Player.StatPointsPerLevel} điểm)";

            lblHealth.Text = $"Máu: {player.HP} / {player.MaxHP}   " +
                $"(đã cộng {player.AllocatedHealth} điểm, +{Player.HealthPerPoint} mỗi điểm)";
            lblAttack.Text = $"Tấn công: {player.MinDamage} - {player.MaxDamage}   " +
                $"(đã cộng {player.AllocatedAttack} điểm, +{Player.AttackPerPoint} mỗi điểm)";
            lblDefense.Text = $"Phòng thủ: {player.Defense}   " +
                $"(đã cộng {player.AllocatedDefense} điểm, +{Player.DefensePerPoint} mỗi điểm)";

            lblSummary.Text =
                $"Tính cả trang bị: Tấn công {player.EffectiveMinDamage} - {player.EffectiveMaxDamage}, " +
                $"Phòng thủ {player.EffectiveDefense}\n" +
                "Phòng thủ trừ thẳng vào sát thương nhận vào (tối thiểu vẫn mất 1 HP).";

            bool hasPoints = player.StatPoints > 0;
            btnAddHealth.Enabled = hasPoints;
            btnAddAttack.Enabled = hasPoints;
            btnAddDefense.Enabled = hasPoints;
        }
    }
}
