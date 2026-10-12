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
            progressPlayerHP = new ProgressBar();
            progressEnemyHP = new ProgressBar();
            lblPlayerHP = new Label();
            lblEnemyHP = new Label();
            btnAttack = new Button();
            btnSkill = new Button();
            progressPlayerMP = new ProgressBar();
            lblPlayerMP = new Label();
            btnItem = new Button();
            grpSkillSelection = new GroupBox();
            btnCancelSkill = new Button();
            btnUseSelectedSkill = new Button();
            lblSkillInfo = new Label();
            lstSkills = new ListBox();
            grpItemSelection = new GroupBox();
            btnUseSelectedItem = new Button();
            btnCancelItem = new Button();
            lblBattleItemInfo = new Label();
            lstBattleItems = new ListBox();
            rtbBattleLog = new RichTextBox();
            btnContinue = new Button();
            grpSkillSelection.SuspendLayout();
            grpItemSelection.SuspendLayout();
            SuspendLayout();
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(266, 45);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(71, 15);
            lblPlayerName.TabIndex = 0;
            lblPlayerName.Text = "PlayerName";
            // 
            // lblEnemyName
            // 
            lblEnemyName.AutoSize = true;
            lblEnemyName.Location = new Point(703, 45);
            lblEnemyName.Name = "lblEnemyName";
            lblEnemyName.Size = new Size(75, 15);
            lblEnemyName.TabIndex = 1;
            lblEnemyName.Text = "EnemyName";
            lblEnemyName.Click += lblEnemyName_Click;
            // 
            // progressPlayerHP
            // 
            progressPlayerHP.Location = new Point(251, 217);
            progressPlayerHP.Name = "progressPlayerHP";
            progressPlayerHP.Size = new Size(100, 23);
            progressPlayerHP.TabIndex = 2;
            // 
            // progressEnemyHP
            // 
            progressEnemyHP.Location = new Point(689, 217);
            progressEnemyHP.Name = "progressEnemyHP";
            progressEnemyHP.Size = new Size(100, 23);
            progressEnemyHP.TabIndex = 3;
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.AutoSize = true;
            lblPlayerHP.Location = new Point(357, 225);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(55, 15);
            lblPlayerHP.TabIndex = 4;
            lblPlayerHP.Text = "PlayerHP";
            // 
            // lblEnemyHP
            // 
            lblEnemyHP.AutoSize = true;
            lblEnemyHP.Location = new Point(795, 225);
            lblEnemyHP.Name = "lblEnemyHP";
            lblEnemyHP.Size = new Size(59, 15);
            lblEnemyHP.TabIndex = 5;
            lblEnemyHP.Text = "EnemyHP";
            // 
            // btnAttack
            // 
            btnAttack.Location = new Point(266, 312);
            btnAttack.Name = "btnAttack";
            btnAttack.Size = new Size(75, 23);
            btnAttack.TabIndex = 6;
            btnAttack.Text = "ATTACK";
            btnAttack.UseVisualStyleBackColor = true;
            btnAttack.Click += btnAttack_Click;
            // 
            // btnSkill
            // 
            btnSkill.Location = new Point(266, 414);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(75, 23);
            btnSkill.TabIndex = 7;
            btnSkill.Text = "SKILL";
            btnSkill.UseVisualStyleBackColor = true;
            btnSkill.Click += btnSkill_Click;
            // 
            // progressPlayerMP
            // 
            progressPlayerMP.Location = new Point(251, 246);
            progressPlayerMP.Name = "progressPlayerMP";
            progressPlayerMP.Size = new Size(100, 23);
            progressPlayerMP.TabIndex = 8;
            // 
            // lblPlayerMP
            // 
            lblPlayerMP.AutoSize = true;
            lblPlayerMP.Location = new Point(357, 254);
            lblPlayerMP.Name = "lblPlayerMP";
            lblPlayerMP.Size = new Size(53, 15);
            lblPlayerMP.TabIndex = 9;
            lblPlayerMP.Text = "PlayeMP";
            // 
            // btnItem
            // 
            btnItem.Location = new Point(266, 362);
            btnItem.Name = "btnItem";
            btnItem.Size = new Size(75, 23);
            btnItem.TabIndex = 10;
            btnItem.Text = "ITEM";
            btnItem.UseVisualStyleBackColor = true;
            btnItem.Click += btnItem_Click;
            // 
            // grpSkillSelection
            // 
            grpSkillSelection.Controls.Add(btnCancelSkill);
            grpSkillSelection.Controls.Add(btnUseSelectedSkill);
            grpSkillSelection.Controls.Add(lblSkillInfo);
            grpSkillSelection.Controls.Add(lstSkills);
            grpSkillSelection.Location = new Point(155, 462);
            grpSkillSelection.Name = "grpSkillSelection";
            grpSkillSelection.Size = new Size(300, 200);
            grpSkillSelection.TabIndex = 11;
            grpSkillSelection.TabStop = false;
            grpSkillSelection.Text = "SkillBox";
            grpSkillSelection.Visible = false;
            // 
            // btnCancelSkill
            // 
            btnCancelSkill.Location = new Point(36, 171);
            btnCancelSkill.Name = "btnCancelSkill";
            btnCancelSkill.Size = new Size(75, 23);
            btnCancelSkill.TabIndex = 3;
            btnCancelSkill.Text = "Hủy";
            btnCancelSkill.UseVisualStyleBackColor = true;
            // 
            // btnUseSelectedSkill
            // 
            btnUseSelectedSkill.Location = new Point(172, 171);
            btnUseSelectedSkill.Name = "btnUseSelectedSkill";
            btnUseSelectedSkill.Size = new Size(75, 23);
            btnUseSelectedSkill.TabIndex = 2;
            btnUseSelectedSkill.Text = "Sử dụng";
            btnUseSelectedSkill.UseVisualStyleBackColor = true;
            // 
            // lblSkillInfo
            // 
            lblSkillInfo.AutoSize = true;
            lblSkillInfo.Location = new Point(172, 22);
            lblSkillInfo.Name = "lblSkillInfo";
            lblSkillInfo.Size = new Size(49, 15);
            lblSkillInfo.TabIndex = 1;
            lblSkillInfo.Text = "InfoSkill";
            // 
            // lstSkills
            // 
            lstSkills.FormattingEnabled = true;
            lstSkills.Location = new Point(6, 22);
            lstSkills.Name = "lstSkills";
            lstSkills.Size = new Size(160, 124);
            lstSkills.TabIndex = 0;
            // 
            // grpItemSelection
            // 
            grpItemSelection.Controls.Add(btnUseSelectedItem);
            grpItemSelection.Controls.Add(btnCancelItem);
            grpItemSelection.Controls.Add(lblBattleItemInfo);
            grpItemSelection.Controls.Add(lstBattleItems);
            grpItemSelection.Location = new Point(499, 462);
            grpItemSelection.Name = "grpItemSelection";
            grpItemSelection.Size = new Size(300, 200);
            grpItemSelection.TabIndex = 12;
            grpItemSelection.TabStop = false;
            grpItemSelection.Text = "groupBox1";
            grpItemSelection.Visible = false;
            // 
            // btnUseSelectedItem
            // 
            btnUseSelectedItem.Location = new Point(178, 171);
            btnUseSelectedItem.Name = "btnUseSelectedItem";
            btnUseSelectedItem.Size = new Size(75, 23);
            btnUseSelectedItem.TabIndex = 3;
            btnUseSelectedItem.Text = "Sử dụng";
            btnUseSelectedItem.UseVisualStyleBackColor = true;
            // 
            // btnCancelItem
            // 
            btnCancelItem.Location = new Point(47, 171);
            btnCancelItem.Name = "btnCancelItem";
            btnCancelItem.Size = new Size(75, 23);
            btnCancelItem.TabIndex = 2;
            btnCancelItem.Text = "Hủy";
            btnCancelItem.UseVisualStyleBackColor = true;
            // 
            // lblBattleItemInfo
            // 
            lblBattleItemInfo.AutoSize = true;
            lblBattleItemInfo.Location = new Point(178, 25);
            lblBattleItemInfo.Name = "lblBattleItemInfo";
            lblBattleItemInfo.Size = new Size(57, 15);
            lblBattleItemInfo.TabIndex = 1;
            lblBattleItemInfo.Text = "InfoItems";
            // 
            // lstBattleItems
            // 
            lstBattleItems.FormattingEnabled = true;
            lstBattleItems.Location = new Point(6, 22);
            lstBattleItems.Name = "lstBattleItems";
            lstBattleItems.Size = new Size(160, 124);
            lstBattleItems.TabIndex = 0;
            // 
            // rtbBattleLog
            // 
            rtbBattleLog.BackColor = SystemColors.ButtonHighlight;
            rtbBattleLog.DetectUrls = false;
            rtbBattleLog.Location = new Point(476, 269);
            rtbBattleLog.Name = "rtbBattleLog";
            rtbBattleLog.ReadOnly = true;
            rtbBattleLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbBattleLog.Size = new Size(500, 180);
            rtbBattleLog.TabIndex = 13;
            rtbBattleLog.Text = "";
            // 
            // btnContinue
            // 
            btnContinue.Location = new Point(866, 476);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(75, 23);
            btnContinue.TabIndex = 14;
            btnContinue.Text = "Tiếp tục";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Visible = false;
            // 
            // BattleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnContinue);
            Controls.Add(rtbBattleLog);
            Controls.Add(grpItemSelection);
            Controls.Add(grpSkillSelection);
            Controls.Add(btnItem);
            Controls.Add(lblPlayerMP);
            Controls.Add(progressPlayerMP);
            Controls.Add(btnSkill);
            Controls.Add(btnAttack);
            Controls.Add(lblEnemyHP);
            Controls.Add(lblPlayerHP);
            Controls.Add(progressEnemyHP);
            Controls.Add(progressPlayerHP);
            Controls.Add(lblEnemyName);
            Controls.Add(lblPlayerName);
            Name = "BattleControl";
            Size = new Size(1280, 720);
            grpSkillSelection.ResumeLayout(false);
            grpSkillSelection.PerformLayout();
            grpItemSelection.ResumeLayout(false);
            grpItemSelection.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayerName;
        private Label lblEnemyName;
        private ProgressBar progressPlayerHP;
        private ProgressBar progressEnemyHP;
        private Label lblPlayerHP;
        private Label lblEnemyHP;
        private Button btnAttack;
        private Button btnSkill;
        private ProgressBar progressPlayerMP;
        private Label lblPlayerMP;
        private Button btnItem;
        private GroupBox grpSkillSelection;
        private Button btnCancelSkill;
        private Button btnUseSelectedSkill;
        private Label lblSkillInfo;
        private ListBox lstSkills;
        private GroupBox grpItemSelection;
        private Label lblBattleItemInfo;
        private ListBox lstBattleItems;
        private Button btnUseSelectedItem;
        private Button btnCancelItem;
        private RichTextBox rtbBattleLog;
        private Button btnContinue;
    }
}
