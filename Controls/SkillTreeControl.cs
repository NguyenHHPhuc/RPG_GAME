using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RPG.DoiTuong;

namespace RPG.Controls
{
    // Màn hình Cây kỹ năng, giao diện tạo bằng code
    public class SkillTreeControl : UserControl
    {
        private static readonly Size NodeSize = new Size(130, 46);

        private static readonly Dictionary<SkillBranch, string> BranchColors = new Dictionary<SkillBranch, string>
        {
            { SkillBranch.Root, "#C9A227" },
            { SkillBranch.Health, "#E05656" },
            { SkillBranch.Attack, "#E67E22" },
            { SkillBranch.Defense, "#4A90E2" }
        };

        private readonly Player player;
        private readonly SkillTree tree;

        private readonly Dictionary<SkillTreeNode, Button> nodeButtons = new Dictionary<SkillTreeNode, Button>();
        private readonly Panel pnlTree = new Panel();
        private readonly Label lblPoints = new Label();
        private readonly Label lblNodeName = new Label();
        private readonly Label lblNodeInfo = new Label();
        private readonly Label lblNodeStatus = new Label();
        private readonly Button btnLearn = new Button();
        private SkillTreeNode selectedNode;

        public event EventHandler CloseClicked;

        public SkillTreeControl(GameState gameState)
        {
            player = gameState.Player;
            tree = player.SkillTree;
            BackColor = ColorTranslator.FromHtml("#F4F1E8");
            Font = new Font("Segoe UI", 10F);

            Label lblTitle = new Label
            {
                Text = "CÂY KỸ NĂNG",
                Location = new Point(40, 20),
                Size = new Size(400, 36),
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#3B2F2F")
            };
            lblPoints.Location = new Point(40, 58);
            lblPoints.Size = new Size(620, 24);
            lblPoints.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPoints.ForeColor = ColorTranslator.FromHtml("#C0392B");
            Controls.Add(lblTitle);
            Controls.Add(lblPoints);

            CreateTreePanel();
            CreateInfoPanel();

            Button btnBack = new Button
            {
                Text = "Quay lại bản đồ",
                Location = new Point(700, 520),
                Size = new Size(180, 40),
                BackColor = ColorTranslator.FromHtml("#FFFFFF")
            };
            btnBack.Click += (_, _) => CloseClicked?.Invoke(this, EventArgs.Empty);
            Controls.Add(btnBack);

            SelectNode(tree.Root);
        }

        // Nhánh quyết định cột, tầng quyết định hàng
        private static Point GetNodeCenter(SkillTreeNode node)
        {
            int x = node.Branch switch
            {
                SkillBranch.Health => 110,
                SkillBranch.Defense => 510,
                _ => 310
            };
            int y = node.Tier switch
            {
                0 => 430,
                1 => 320,
                2 => 210,
                _ => 100
            };
            return new Point(x, y);
        }

        private void CreateTreePanel()
        {
            pnlTree.Location = new Point(40, 90);
            pnlTree.Size = new Size(620, 470);
            pnlTree.BackColor = ColorTranslator.FromHtml("#FFFDF7");
            pnlTree.BorderStyle = BorderStyle.FixedSingle;
            pnlTree.Paint += PnlTree_Paint;
            Controls.Add(pnlTree);

            AddBranchTitle("MÁU", SkillBranch.Health, 110);
            AddBranchTitle("TẤN CÔNG", SkillBranch.Attack, 310);
            AddBranchTitle("PHÒNG THỦ", SkillBranch.Defense, 510);

            foreach (SkillTreeNode node in tree.Nodes)
            {
                Point center = GetNodeCenter(node);
                Button button = new Button
                {
                    Text = node.Name,
                    Size = NodeSize,
                    Location = new Point(center.X - NodeSize.Width / 2, center.Y - NodeSize.Height / 2),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                button.Click += (_, _) => SelectNode(node);
                nodeButtons.Add(node, button);
                pnlTree.Controls.Add(button);
            }
        }

        private void AddBranchTitle(string text, SkillBranch branch, int centerX)
        {
            pnlTree.Controls.Add(new Label
            {
                Text = text,
                Location = new Point(centerX - 70, 30),
                Size = new Size(140, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml(BranchColors[branch])
            });
        }

        private void CreateInfoPanel()
        {
            lblNodeName.Location = new Point(700, 100);
            lblNodeName.Size = new Size(400, 30);
            lblNodeName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            lblNodeInfo.Location = new Point(700, 135);
            lblNodeInfo.Size = new Size(400, 110);
            lblNodeInfo.ForeColor = ColorTranslator.FromHtml("#333333");

            lblNodeStatus.Location = new Point(700, 250);
            lblNodeStatus.Size = new Size(400, 24);
            lblNodeStatus.Font = new Font("Segoe UI", 10F, FontStyle.Italic);

            btnLearn.Text = "Học (1 điểm)";
            btnLearn.Location = new Point(700, 280);
            btnLearn.Size = new Size(180, 40);
            btnLearn.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            btnLearn.Click += BtnLearn_Click;

            AdminPanel adminPanel = new AdminPanel(player) { Location = new Point(700, 420) };
            adminPanel.Changed += (_, _) => RefreshTree();

            Controls.Add(lblNodeName);
            Controls.Add(lblNodeInfo);
            Controls.Add(lblNodeStatus);
            Controls.Add(btnLearn);
            Controls.Add(adminPanel);
        }

        private void SelectNode(SkillTreeNode node)
        {
            selectedNode = node;
            RefreshTree();
        }

        private void BtnLearn_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
                player.LearnSkillNode(selectedNode);
            RefreshTree();
        }

        private void RefreshTree()
        {
            lblPoints.Text = $"Điểm kỹ năng: {player.SkillPoints}   |   " +
                $"Nhánh đã chọn: {tree.CountStartedBranches()} / {SkillTree.MaxBranches}";

            foreach (KeyValuePair<SkillTreeNode, Button> pair in nodeButtons)
            {
                SkillTreeNode node = pair.Key;
                Button button = pair.Value;
                bool canLearn = tree.GetLockReason(node, player.SkillPoints) == null;

                // Đã học: màu nhánh, học được: trắng, bị khóa: xám
                if (node.IsLearned)
                {
                    button.BackColor = ColorTranslator.FromHtml(BranchColors[node.Branch]);
                    button.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                else if (canLearn)
                {
                    button.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    button.ForeColor = ColorTranslator.FromHtml("#222222");
                }
                else
                {
                    button.BackColor = ColorTranslator.FromHtml("#D5D5D5");
                    button.ForeColor = ColorTranslator.FromHtml("#7A7A7A");
                }

                button.FlatAppearance.BorderSize = node == selectedNode ? 3 : 1;
                button.FlatAppearance.BorderColor = ColorTranslator.FromHtml(
                    node == selectedNode ? "#222222" : "#999999");
            }

            if (selectedNode != null)
            {
                string reason = tree.GetLockReason(selectedNode, player.SkillPoints);
                lblNodeName.Text = selectedNode.Name;
                lblNodeName.ForeColor = ColorTranslator.FromHtml(BranchColors[selectedNode.Branch]);
                lblNodeInfo.Text = selectedNode.GetDetails();
                lblNodeStatus.Text = reason ?? "Có thể học.";
                lblNodeStatus.ForeColor = ColorTranslator.FromHtml(reason == null ? "#27AE60" : "#C0392B");
                btnLearn.Enabled = reason == null;
            }

            pnlTree.Invalidate();
        }

        // Vẽ đường nối giữa các ô
        private void PnlTree_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (SkillTreeNode node in tree.Nodes)
            {
                if (node.Branch == SkillBranch.Root)
                    continue;

                SkillTreeNode parent = node.Tier == 1 ? tree.Root : tree.GetNode(node.Branch, node.Tier - 1);
                string hexColor = node.IsLearned ? BranchColors[node.Branch] : "#B0B0B0";
                using Pen pen = new Pen(ColorTranslator.FromHtml(hexColor), node.IsLearned ? 4 : 2);
                e.Graphics.DrawLine(pen, GetNodeCenter(parent), GetNodeCenter(node));
            }
        }
    }
}
