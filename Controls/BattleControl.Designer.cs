namespace RPG.Controls
{
    partial class BattleControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                animationTimer?.Dispose();
                floatingTextTimer?.Dispose();
                picBattlePlayer?.Image?.Dispose();
                picBattleEnemy?.Image?.Dispose();
                arrowImage?.Dispose();
                tintedImage?.Dispose();
                if (!ReferenceEquals(damagedOriginalImage, picBattlePlayer?.Image)
                    && !ReferenceEquals(damagedOriginalImage, picBattleEnemy?.Image))
                    damagedOriginalImage?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPlayerName = new Label();
            lblEnemyName = new Label();
            pnlPlayerHP = new Panel();
            pnlPlayerHPFill = new Panel();
            pnlEnemyHP = new Panel();
            pnlEnemyHPFill = new Panel();
            lblPlayerHP = new Label();
            lblEnemyHP = new Label();
            btnAttack = new Button();
            btnSkill = new Button();
            pnlPlayerMP = new Panel();
            pnlPlayerMPFill = new Panel();
            lblPlayerMP = new Label();
            btnItem = new Button();
            lblBattleLogTitle = new Label();
            lstBattleLog = new ListBox();
            btnContinue = new Button();
            pnlEncounterScene = new Panel();
            picBattlePlayer = new PictureBox();
            picBattleEnemy = new PictureBox();
            pnlPlayerXP = new Panel();
            pnlPlayerXPFill = new Panel();
            lblPlayerXP = new Label();
            grpSkillSelection = new GroupBox();
            lstSkills = new ListBox();
            lblSkillInfo = new Label();
            btnUseSelectedSkill = new Button();
            btnCancelSkill = new Button();
            grpItemSelection = new GroupBox();
            lstBattleItems = new ListBox();
            lblBattleItemInfo = new Label();
            btnUseSelectedItem = new Button();
            btnCancelItem = new Button();
            pnlPlayerHP.SuspendLayout();
            pnlEnemyHP.SuspendLayout();
            pnlPlayerMP.SuspendLayout();
            pnlEncounterScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBattlePlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBattleEnemy).BeginInit();
            pnlPlayerXP.SuspendLayout();
            grpSkillSelection.SuspendLayout();
            grpItemSelection.SuspendLayout();
            SuspendLayout();
            // 
            // lblPlayerName
            // 
            lblPlayerName.Anchor = AnchorStyles.None;
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(251, 23);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(89, 20);
            lblPlayerName.TabIndex = 0;
            lblPlayerName.Text = "PlayerName";
            // 
            // lblEnemyName
            // 
            lblEnemyName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEnemyName.AutoSize = true;
            lblEnemyName.Location = new Point(811, 23);
            lblEnemyName.Name = "lblEnemyName";
            lblEnemyName.Size = new Size(93, 20);
            lblEnemyName.TabIndex = 1;
            lblEnemyName.Text = "EnemyName";
            lblEnemyName.Click += lblEnemyName_Click;
            // 
            // pnlPlayerHP
            // 
            pnlPlayerHP.BackColor = Color.Gainsboro;
            pnlPlayerHP.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayerHP.Controls.Add(pnlPlayerHPFill);
            pnlPlayerHP.Location = new Point(91, 291);
            pnlPlayerHP.Margin = new Padding(3, 4, 3, 4);
            pnlPlayerHP.Name = "pnlPlayerHP";
            pnlPlayerHP.Size = new Size(400, 31);
            pnlPlayerHP.TabIndex = 2;
            // 
            // pnlPlayerHPFill
            // 
            pnlPlayerHPFill.BackColor = Color.FromArgb(220, 53, 69);
            pnlPlayerHPFill.Location = new Point(0, 0);
            pnlPlayerHPFill.Margin = new Padding(3, 4, 3, 4);
            pnlPlayerHPFill.Name = "pnlPlayerHPFill";
            pnlPlayerHPFill.Size = new Size(398, 29);
            pnlPlayerHPFill.TabIndex = 0;
            // 
            // pnlEnemyHP
            // 
            pnlEnemyHP.BackColor = Color.Gainsboro;
            pnlEnemyHP.BorderStyle = BorderStyle.FixedSingle;
            pnlEnemyHP.Controls.Add(pnlEnemyHPFill);
            pnlEnemyHP.Location = new Point(651, 291);
            pnlEnemyHP.Margin = new Padding(3, 4, 3, 4);
            pnlEnemyHP.Name = "pnlEnemyHP";
            pnlEnemyHP.Size = new Size(400, 31);
            pnlEnemyHP.TabIndex = 3;
            // 
            // pnlEnemyHPFill
            // 
            pnlEnemyHPFill.BackColor = Color.FromArgb(220, 53, 69);
            pnlEnemyHPFill.Location = new Point(0, 0);
            pnlEnemyHPFill.Margin = new Padding(3, 4, 3, 4);
            pnlEnemyHPFill.Name = "pnlEnemyHPFill";
            pnlEnemyHPFill.Size = new Size(398, 29);
            pnlEnemyHPFill.TabIndex = 0;
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.AutoSize = true;
            lblPlayerHP.Location = new Point(91, 328);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(38, 20);
            lblPlayerHP.TabIndex = 4;
            lblPlayerHP.Text = "Máu";
            // 
            // lblEnemyHP
            // 
            lblEnemyHP.AutoSize = true;
            lblEnemyHP.Location = new Point(651, 328);
            lblEnemyHP.Name = "lblEnemyHP";
            lblEnemyHP.Size = new Size(38, 20);
            lblEnemyHP.TabIndex = 5;
            lblEnemyHP.Text = "Máu";
            // 
            // btnAttack
            // 
            btnAttack.Anchor = AnchorStyles.Bottom;
            btnAttack.Location = new Point(294, 728);
            btnAttack.Margin = new Padding(3, 4, 3, 4);
            btnAttack.Name = "btnAttack";
            btnAttack.Size = new Size(171, 59);
            btnAttack.TabIndex = 6;
            btnAttack.Text = "Đánh";
            btnAttack.UseVisualStyleBackColor = true;
            btnAttack.Click += btnAttack_Click;
            // 
            // btnSkill
            // 
            btnSkill.Anchor = AnchorStyles.Bottom;
            btnSkill.Location = new Point(486, 728);
            btnSkill.Margin = new Padding(3, 4, 3, 4);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(171, 59);
            btnSkill.TabIndex = 7;
            btnSkill.Text = "Kỹ năng";
            btnSkill.UseVisualStyleBackColor = true;
            btnSkill.Click += btnSkill_Click;
            // 
            // pnlPlayerMP
            // 
            pnlPlayerMP.BackColor = Color.Gainsboro;
            pnlPlayerMP.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayerMP.Controls.Add(pnlPlayerMPFill);
            pnlPlayerMP.Location = new Point(91, 356);
            pnlPlayerMP.Margin = new Padding(3, 4, 3, 4);
            pnlPlayerMP.Name = "pnlPlayerMP";
            pnlPlayerMP.Size = new Size(400, 31);
            pnlPlayerMP.TabIndex = 8;
            // 
            // pnlPlayerMPFill
            // 
            pnlPlayerMPFill.BackColor = Color.FromArgb(13, 110, 253);
            pnlPlayerMPFill.Location = new Point(0, 0);
            pnlPlayerMPFill.Margin = new Padding(3, 4, 3, 4);
            pnlPlayerMPFill.Name = "pnlPlayerMPFill";
            pnlPlayerMPFill.Size = new Size(398, 29);
            pnlPlayerMPFill.TabIndex = 0;
            // 
            // lblPlayerMP
            // 
            lblPlayerMP.AutoSize = true;
            lblPlayerMP.Location = new Point(91, 393);
            lblPlayerMP.Name = "lblPlayerMP";
            lblPlayerMP.Size = new Size(88, 20);
            lblPlayerMP.TabIndex = 9;
            lblPlayerMP.Text = "Năng lượng";
            // 
            // btnItem
            // 
            btnItem.Anchor = AnchorStyles.Bottom;
            btnItem.Location = new Point(678, 728);
            btnItem.Margin = new Padding(3, 4, 3, 4);
            btnItem.Name = "btnItem";
            btnItem.Size = new Size(171, 59);
            btnItem.TabIndex = 10;
            btnItem.Text = "Vật phẩm";
            btnItem.UseVisualStyleBackColor = true;
            btnItem.Click += btnItem_Click;
            // 
            // lblBattleLogTitle
            // 
            lblBattleLogTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblBattleLogTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBattleLogTitle.Location = new Point(137, 461);
            lblBattleLogTitle.Name = "lblBattleLogTitle";
            lblBattleLogTitle.Size = new Size(869, 29);
            lblBattleLogTitle.TabIndex = 11;
            lblBattleLogTitle.Text = "NHẬT KÝ TRẬN ĐẤU";
            // 
            // lstBattleLog
            // 
            lstBattleLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstBattleLog.Font = new Font("Segoe UI", 10F);
            lstBattleLog.FormattingEnabled = true;
            lstBattleLog.HorizontalScrollbar = true;
            lstBattleLog.IntegralHeight = false;
            lstBattleLog.Location = new Point(137, 496);
            lstBattleLog.Margin = new Padding(3, 4, 3, 4);
            lstBattleLog.Name = "lstBattleLog";
            lstBattleLog.Size = new Size(868, 159);
            lstBattleLog.TabIndex = 12;
            // 
            // btnContinue
            // 
            btnContinue.Anchor = AnchorStyles.Bottom;
            btnContinue.Location = new Point(486, 728);
            btnContinue.Margin = new Padding(3, 4, 3, 4);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(171, 59);
            btnContinue.TabIndex = 13;
            btnContinue.Text = "Tiếp tục";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Visible = false;
            btnContinue.Click += btnContinue_Click;
            // 
            // pnlEncounterScene
            // 
            pnlEncounterScene.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlEncounterScene.BackColor = Color.FromArgb(222, 230, 216);
            pnlEncounterScene.BorderStyle = BorderStyle.FixedSingle;
            pnlEncounterScene.Controls.Add(picBattlePlayer);
            pnlEncounterScene.Controls.Add(picBattleEnemy);
            pnlEncounterScene.Location = new Point(57, 56);
            pnlEncounterScene.Margin = new Padding(3, 4, 3, 4);
            pnlEncounterScene.Name = "pnlEncounterScene";
            pnlEncounterScene.Size = new Size(1028, 159);
            pnlEncounterScene.TabIndex = 14;
            pnlEncounterScene.Paint += pnlEncounterScene_Paint;
            // 
            // picBattlePlayer
            // 
            picBattlePlayer.BackColor = Color.Transparent;
            picBattlePlayer.Location = new Point(215, 53);
            picBattlePlayer.Margin = new Padding(3, 4, 3, 4);
            picBattlePlayer.Name = "picBattlePlayer";
            picBattlePlayer.Size = new Size(39, 80);
            picBattlePlayer.SizeMode = PictureBoxSizeMode.Zoom;
            picBattlePlayer.TabIndex = 1;
            picBattlePlayer.TabStop = false;
            // 
            // picBattleEnemy
            // 
            picBattleEnemy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picBattleEnemy.BackColor = Color.Transparent;
            picBattleEnemy.Location = new Point(1573, 45);
            picBattleEnemy.Margin = new Padding(3, 4, 3, 4);
            picBattleEnemy.Name = "picBattleEnemy";
            picBattleEnemy.Size = new Size(39, 80);
            picBattleEnemy.SizeMode = PictureBoxSizeMode.Zoom;
            picBattleEnemy.TabIndex = 2;
            picBattleEnemy.TabStop = false;
            // 
            // pnlPlayerXP
            // 
            pnlPlayerXP.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlPlayerXP.BackColor = Color.Gainsboro;
            pnlPlayerXP.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayerXP.Controls.Add(pnlPlayerXPFill);
            pnlPlayerXP.Location = new Point(57, 240);
            pnlPlayerXP.Margin = new Padding(3, 4, 3, 4);
            pnlPlayerXP.Name = "pnlPlayerXP";
            pnlPlayerXP.Size = new Size(1028, 13);
            pnlPlayerXP.TabIndex = 15;
            // 
            // pnlPlayerXPFill
            // 
            pnlPlayerXPFill.BackColor = Color.FromArgb(156, 39, 176);
            pnlPlayerXPFill.Location = new Point(0, 0);
            pnlPlayerXPFill.Margin = new Padding(3, 4, 3, 4);
            pnlPlayerXPFill.Name = "pnlPlayerXPFill";
            pnlPlayerXPFill.Size = new Size(0, 11);
            pnlPlayerXPFill.TabIndex = 0;
            // 
            // lblPlayerXP
            // 
            lblPlayerXP.AutoSize = true;
            lblPlayerXP.Location = new Point(57, 259);
            lblPlayerXP.Name = "lblPlayerXP";
            lblPlayerXP.Size = new Size(145, 20);
            lblPlayerXP.TabIndex = 16;
            lblPlayerXP.Text = "Kinh nghiệm: 0 / 100";
            // 
            // grpSkillSelection
            // 
            grpSkillSelection.Anchor = AnchorStyles.None;
            grpSkillSelection.Controls.Add(lstSkills);
            grpSkillSelection.Controls.Add(lblSkillInfo);
            grpSkillSelection.Controls.Add(btnUseSelectedSkill);
            grpSkillSelection.Controls.Add(btnCancelSkill);
            grpSkillSelection.Location = new Point(366, 313);
            grpSkillSelection.Margin = new Padding(3, 4, 3, 4);
            grpSkillSelection.Name = "grpSkillSelection";
            grpSkillSelection.Padding = new Padding(3, 4, 3, 4);
            grpSkillSelection.Size = new Size(411, 373);
            grpSkillSelection.TabIndex = 17;
            grpSkillSelection.TabStop = false;
            grpSkillSelection.Text = "Chọn kỹ năng";
            grpSkillSelection.Visible = false;
            // 
            // lstSkills
            // 
            lstSkills.FormattingEnabled = true;
            lstSkills.Location = new Point(18, 40);
            lstSkills.Margin = new Padding(3, 4, 3, 4);
            lstSkills.Name = "lstSkills";
            lstSkills.Size = new Size(165, 204);
            lstSkills.TabIndex = 0;
            // 
            // lblSkillInfo
            // 
            lblSkillInfo.Location = new Point(200, 40);
            lblSkillInfo.Name = "lblSkillInfo";
            lblSkillInfo.Size = new Size(192, 219);
            lblSkillInfo.TabIndex = 1;
            lblSkillInfo.Text = "Chọn một kỹ năng.";
            // 
            // btnUseSelectedSkill
            // 
            btnUseSelectedSkill.Location = new Point(51, 293);
            btnUseSelectedSkill.Margin = new Padding(3, 4, 3, 4);
            btnUseSelectedSkill.Name = "btnUseSelectedSkill";
            btnUseSelectedSkill.Size = new Size(137, 47);
            btnUseSelectedSkill.TabIndex = 2;
            btnUseSelectedSkill.Text = "Sử dụng";
            btnUseSelectedSkill.UseVisualStyleBackColor = true;
            // 
            // btnCancelSkill
            // 
            btnCancelSkill.Location = new Point(217, 293);
            btnCancelSkill.Margin = new Padding(3, 4, 3, 4);
            btnCancelSkill.Name = "btnCancelSkill";
            btnCancelSkill.Size = new Size(137, 47);
            btnCancelSkill.TabIndex = 3;
            btnCancelSkill.Text = "Hủy";
            btnCancelSkill.UseVisualStyleBackColor = true;
            // 
            // grpItemSelection
            // 
            grpItemSelection.Anchor = AnchorStyles.None;
            grpItemSelection.Controls.Add(lstBattleItems);
            grpItemSelection.Controls.Add(lblBattleItemInfo);
            grpItemSelection.Controls.Add(btnUseSelectedItem);
            grpItemSelection.Controls.Add(btnCancelItem);
            grpItemSelection.Location = new Point(366, 313);
            grpItemSelection.Margin = new Padding(3, 4, 3, 4);
            grpItemSelection.Name = "grpItemSelection";
            grpItemSelection.Padding = new Padding(3, 4, 3, 4);
            grpItemSelection.Size = new Size(411, 373);
            grpItemSelection.TabIndex = 18;
            grpItemSelection.TabStop = false;
            grpItemSelection.Text = "Chọn vật phẩm";
            grpItemSelection.Visible = false;
            // 
            // lstBattleItems
            // 
            lstBattleItems.FormattingEnabled = true;
            lstBattleItems.Location = new Point(18, 40);
            lstBattleItems.Margin = new Padding(3, 4, 3, 4);
            lstBattleItems.Name = "lstBattleItems";
            lstBattleItems.Size = new Size(165, 204);
            lstBattleItems.TabIndex = 0;
            // 
            // lblBattleItemInfo
            // 
            lblBattleItemInfo.Location = new Point(200, 40);
            lblBattleItemInfo.Name = "lblBattleItemInfo";
            lblBattleItemInfo.Size = new Size(192, 219);
            lblBattleItemInfo.TabIndex = 1;
            lblBattleItemInfo.Text = "Chọn một vật phẩm.";
            // 
            // btnUseSelectedItem
            // 
            btnUseSelectedItem.Location = new Point(51, 293);
            btnUseSelectedItem.Margin = new Padding(3, 4, 3, 4);
            btnUseSelectedItem.Name = "btnUseSelectedItem";
            btnUseSelectedItem.Size = new Size(137, 47);
            btnUseSelectedItem.TabIndex = 2;
            btnUseSelectedItem.Text = "Sử dụng";
            btnUseSelectedItem.UseVisualStyleBackColor = true;
            // 
            // btnCancelItem
            // 
            btnCancelItem.Location = new Point(217, 293);
            btnCancelItem.Margin = new Padding(3, 4, 3, 4);
            btnCancelItem.Name = "btnCancelItem";
            btnCancelItem.Size = new Size(137, 47);
            btnCancelItem.TabIndex = 3;
            btnCancelItem.Text = "Hủy";
            btnCancelItem.UseVisualStyleBackColor = true;
            // 
            // BattleControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnItem);
            Controls.Add(lblPlayerMP);
            Controls.Add(pnlPlayerMP);
            Controls.Add(btnSkill);
            Controls.Add(btnAttack);
            Controls.Add(lblEnemyHP);
            Controls.Add(lblPlayerHP);
            Controls.Add(pnlEnemyHP);
            Controls.Add(pnlPlayerHP);
            Controls.Add(pnlEncounterScene);
            Controls.Add(lblPlayerXP);
            Controls.Add(pnlPlayerXP);
            Controls.Add(lblEnemyName);
            Controls.Add(lblPlayerName);
            Controls.Add(lstBattleLog);
            Controls.Add(lblBattleLogTitle);
            Controls.Add(btnContinue);
            Controls.Add(grpSkillSelection);
            Controls.Add(grpItemSelection);
            Margin = new Padding(3, 4, 3, 4);
            Name = "BattleControl";
            Size = new Size(1143, 867);
            pnlPlayerHP.ResumeLayout(false);
            pnlEnemyHP.ResumeLayout(false);
            pnlPlayerMP.ResumeLayout(false);
            pnlEncounterScene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBattlePlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBattleEnemy).EndInit();
            pnlPlayerXP.ResumeLayout(false);
            grpSkillSelection.ResumeLayout(false);
            grpItemSelection.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayerName;
        private Label lblEnemyName;
        private Panel pnlPlayerHP;
        private Panel pnlPlayerHPFill;
        private Panel pnlEnemyHP;
        private Panel pnlEnemyHPFill;
        private Label lblPlayerHP;
        private Label lblEnemyHP;
        private Button btnAttack;
        private Button btnSkill;
        private Panel pnlPlayerMP;
        private Panel pnlPlayerMPFill;
        private Label lblPlayerMP;
        private Button btnItem;
        private Label lblBattleLogTitle;
        private ListBox lstBattleLog;
        private Button btnContinue;
        private Panel pnlEncounterScene;
        private PictureBox picBattlePlayer;
        private PictureBox picBattleEnemy;
        private Panel pnlPlayerXP;
        private Panel pnlPlayerXPFill;
        private Label lblPlayerXP;
        private GroupBox grpSkillSelection;
        private ListBox lstSkills;
        private Label lblSkillInfo;
        private Button btnUseSelectedSkill;
        private Button btnCancelSkill;
        private GroupBox grpItemSelection;
        private ListBox lstBattleItems;
        private Label lblBattleItemInfo;
        private Button btnUseSelectedItem;
        private Button btnCancelItem;
    }
}
