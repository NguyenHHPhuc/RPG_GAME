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
            grpItemSelection.SuspendLayout();
            SuspendLayout();
            // 
            // lblPlayerName
            // 
            lblPlayerName.Anchor = AnchorStyles.None;
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(220, 17);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(71, 15);
            lblPlayerName.TabIndex = 0;
            lblPlayerName.Text = "PlayerName";
            // 
            // lblEnemyName
            // 
            lblEnemyName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEnemyName.AutoSize = true;
            lblEnemyName.Location = new Point(710, 17);
            lblEnemyName.Name = "lblEnemyName";
            lblEnemyName.Size = new Size(75, 15);
            lblEnemyName.TabIndex = 1;
            lblEnemyName.Text = "EnemyName";
            lblEnemyName.Click += lblEnemyName_Click;
            // 
            // pnlPlayerHP
            // 
            pnlPlayerHP.BackColor = Color.Gainsboro;
            pnlPlayerHP.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayerHP.Controls.Add(pnlPlayerHPFill);
            pnlPlayerHP.Location = new Point(80, 218);
            pnlPlayerHP.Name = "pnlPlayerHP";
            pnlPlayerHP.Size = new Size(350, 24);
            pnlPlayerHP.TabIndex = 2;
            // 
            // pnlPlayerHPFill
            // 
            pnlPlayerHPFill.BackColor = Color.FromArgb(220, 53, 69);
            pnlPlayerHPFill.Location = new Point(0, 0);
            pnlPlayerHPFill.Name = "pnlPlayerHPFill";
            pnlPlayerHPFill.Size = new Size(348, 22);
            pnlPlayerHPFill.TabIndex = 0;
            // 
            // pnlEnemyHP
            // 
            pnlEnemyHP.BackColor = Color.Gainsboro;
            pnlEnemyHP.BorderStyle = BorderStyle.FixedSingle;
            pnlEnemyHP.Controls.Add(pnlEnemyHPFill);
            pnlEnemyHP.Location = new Point(570, 218);
            pnlEnemyHP.Name = "pnlEnemyHP";
            pnlEnemyHP.Size = new Size(350, 24);
            pnlEnemyHP.TabIndex = 3;
            // 
            // pnlEnemyHPFill
            // 
            pnlEnemyHPFill.BackColor = Color.FromArgb(220, 53, 69);
            pnlEnemyHPFill.Location = new Point(0, 0);
            pnlEnemyHPFill.Name = "pnlEnemyHPFill";
            pnlEnemyHPFill.Size = new Size(348, 22);
            pnlEnemyHPFill.TabIndex = 0;
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.AutoSize = true;
            lblPlayerHP.Location = new Point(80, 246);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(31, 15);
            lblPlayerHP.TabIndex = 4;
            lblPlayerHP.Text = "Máu";
            // 
            // lblEnemyHP
            // 
            lblEnemyHP.AutoSize = true;
            lblEnemyHP.Location = new Point(570, 246);
            lblEnemyHP.Name = "lblEnemyHP";
            lblEnemyHP.Size = new Size(31, 15);
            lblEnemyHP.TabIndex = 5;
            lblEnemyHP.Text = "Máu";
            // 
            // btnAttack
            // 
            btnAttack.Anchor = AnchorStyles.Bottom;
            btnAttack.Location = new Point(257, 546);
            btnAttack.Name = "btnAttack";
            btnAttack.Size = new Size(150, 44);
            btnAttack.TabIndex = 6;
            btnAttack.Text = "Đánh";
            btnAttack.UseVisualStyleBackColor = true;
            btnAttack.Click += btnAttack_Click;
            // 
            // btnSkill
            // 
            btnSkill.Anchor = AnchorStyles.Bottom;
            btnSkill.Location = new Point(425, 546);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(150, 44);
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
            pnlPlayerMP.Location = new Point(80, 267);
            pnlPlayerMP.Name = "pnlPlayerMP";
            pnlPlayerMP.Size = new Size(350, 24);
            pnlPlayerMP.TabIndex = 8;
            // 
            // pnlPlayerMPFill
            // 
            pnlPlayerMPFill.BackColor = Color.FromArgb(13, 110, 253);
            pnlPlayerMPFill.Location = new Point(0, 0);
            pnlPlayerMPFill.Name = "pnlPlayerMPFill";
            pnlPlayerMPFill.Size = new Size(348, 22);
            pnlPlayerMPFill.TabIndex = 0;
            // 
            // lblPlayerMP
            // 
            lblPlayerMP.AutoSize = true;
            lblPlayerMP.Location = new Point(80, 295);
            lblPlayerMP.Name = "lblPlayerMP";
            lblPlayerMP.Size = new Size(70, 15);
            lblPlayerMP.TabIndex = 9;
            lblPlayerMP.Text = "Năng lượng";
            // 
            // btnItem
            // 
            btnItem.Anchor = AnchorStyles.Bottom;
            btnItem.Location = new Point(593, 546);
            btnItem.Name = "btnItem";
            btnItem.Size = new Size(150, 44);
            btnItem.TabIndex = 10;
            btnItem.Text = "Vật phẩm";
            btnItem.UseVisualStyleBackColor = true;
            btnItem.Click += btnItem_Click;
            // 
            // lblBattleLogTitle
            // 
            lblBattleLogTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblBattleLogTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBattleLogTitle.Location = new Point(120, 346);
            lblBattleLogTitle.Name = "lblBattleLogTitle";
            lblBattleLogTitle.Size = new Size(760, 22);
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
            lstBattleLog.Location = new Point(120, 372);
            lstBattleLog.Name = "lstBattleLog";
            lstBattleLog.Size = new Size(760, 120);
            lstBattleLog.TabIndex = 12;
            // 
            // btnContinue
            // 
            btnContinue.Anchor = AnchorStyles.Bottom;
            btnContinue.Location = new Point(425, 546);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(150, 44);
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
            pnlEncounterScene.Location = new Point(50, 42);
            pnlEncounterScene.Name = "pnlEncounterScene";
            pnlEncounterScene.Size = new Size(900, 120);
            pnlEncounterScene.TabIndex = 14;
            pnlEncounterScene.Paint += pnlEncounterScene_Paint;
            // 
            // picBattlePlayer
            // 
            picBattlePlayer.BackColor = Color.Transparent;
            picBattlePlayer.Location = new Point(188, 40);
            picBattlePlayer.Name = "picBattlePlayer";
            picBattlePlayer.Size = new Size(34, 60);
            picBattlePlayer.SizeMode = PictureBoxSizeMode.Zoom;
            picBattlePlayer.TabIndex = 1;
            picBattlePlayer.TabStop = false;
            // 
            // picBattleEnemy
            // 
            picBattleEnemy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picBattleEnemy.BackColor = Color.Transparent;
            picBattleEnemy.Location = new Point(1376, 34);
            picBattleEnemy.Name = "picBattleEnemy";
            picBattleEnemy.Size = new Size(34, 60);
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
            pnlPlayerXP.Location = new Point(50, 180);
            pnlPlayerXP.Name = "pnlPlayerXP";
            pnlPlayerXP.Size = new Size(900, 10);
            pnlPlayerXP.TabIndex = 15;
            // 
            // pnlPlayerXPFill
            // 
            pnlPlayerXPFill.BackColor = Color.FromArgb(156, 39, 176);
            pnlPlayerXPFill.Location = new Point(0, 0);
            pnlPlayerXPFill.Name = "pnlPlayerXPFill";
            pnlPlayerXPFill.Size = new Size(0, 8);
            pnlPlayerXPFill.TabIndex = 0;
            // 
            // lblPlayerXP
            // 
            lblPlayerXP.AutoSize = true;
            lblPlayerXP.Location = new Point(50, 194);
            lblPlayerXP.Name = "lblPlayerXP";
            lblPlayerXP.Size = new Size(116, 15);
            lblPlayerXP.TabIndex = 16;
            lblPlayerXP.Text = "Kinh nghiệm: 0 / 100";
            //
            // grpItemSelection
            //
            grpItemSelection.Anchor = AnchorStyles.None;
            grpItemSelection.Controls.Add(lstBattleItems);
            grpItemSelection.Controls.Add(lblBattleItemInfo);
            grpItemSelection.Controls.Add(btnUseSelectedItem);
            grpItemSelection.Controls.Add(btnCancelItem);
            grpItemSelection.Location = new Point(320, 235);
            grpItemSelection.Name = "grpItemSelection";
            grpItemSelection.Size = new Size(360, 280);
            grpItemSelection.TabIndex = 18;
            grpItemSelection.TabStop = false;
            grpItemSelection.Text = "Chọn vật phẩm";
            grpItemSelection.Visible = false;
            //
            // lstBattleItems
            //
            lstBattleItems.FormattingEnabled = true;
            lstBattleItems.ItemHeight = 20;
            lstBattleItems.Location = new Point(16, 30);
            lstBattleItems.Name = "lstBattleItems";
            lstBattleItems.Size = new Size(145, 164);
            lstBattleItems.TabIndex = 0;
            //
            // lblBattleItemInfo
            //
            lblBattleItemInfo.Location = new Point(175, 30);
            lblBattleItemInfo.Name = "lblBattleItemInfo";
            lblBattleItemInfo.Size = new Size(168, 164);
            lblBattleItemInfo.TabIndex = 1;
            lblBattleItemInfo.Text = "Chọn một vật phẩm.";
            //
            // btnUseSelectedItem
            //
            btnUseSelectedItem.Location = new Point(45, 220);
            btnUseSelectedItem.Name = "btnUseSelectedItem";
            btnUseSelectedItem.Size = new Size(120, 35);
            btnUseSelectedItem.TabIndex = 2;
            btnUseSelectedItem.Text = "Sử dụng";
            btnUseSelectedItem.UseVisualStyleBackColor = true;
            //
            // btnCancelItem
            //
            btnCancelItem.Location = new Point(190, 220);
            btnCancelItem.Name = "btnCancelItem";
            btnCancelItem.Size = new Size(120, 35);
            btnCancelItem.TabIndex = 3;
            btnCancelItem.Text = "Hủy";
            btnCancelItem.UseVisualStyleBackColor = true;
            //
            // BattleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
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
            Controls.Add(grpItemSelection);
            Name = "BattleControl";
            Size = new Size(1000, 650);
            pnlPlayerHP.ResumeLayout(false);
            pnlEnemyHP.ResumeLayout(false);
            pnlPlayerMP.ResumeLayout(false);
            pnlEncounterScene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBattlePlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBattleEnemy).EndInit();
            pnlPlayerXP.ResumeLayout(false);
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
        private GroupBox grpItemSelection;
        private ListBox lstBattleItems;
        private Label lblBattleItemInfo;
        private Button btnUseSelectedItem;
        private Button btnCancelItem;
    }
}
