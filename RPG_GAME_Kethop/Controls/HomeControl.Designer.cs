namespace RPG.Controls
{
    partial class HomeControl
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
            lblTitle = new Label();
            lblPlayerName = new Label();
            lblPlayerLevel = new Label();
            lblPlayerHP = new Label();
            lblPlayerMP = new Label();
            btnStartAdventure = new Button();
            btnInventory = new Button();
            btnExitHome = new Button();
            lblPlayerEXP = new Label();
            lblPlayerGold = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F);
            lblTitle.Location = new Point(330, 46);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(104, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HOME";
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(176, 133);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(71, 15);
            lblPlayerName.TabIndex = 1;
            lblPlayerName.Text = "PlayerName";
            // 
            // lblPlayerLevel
            // 
            lblPlayerLevel.AutoSize = true;
            lblPlayerLevel.Location = new Point(330, 178);
            lblPlayerLevel.Name = "lblPlayerLevel";
            lblPlayerLevel.Size = new Size(19, 15);
            lblPlayerLevel.TabIndex = 2;
            lblPlayerLevel.Text = "LV";
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.AutoSize = true;
            lblPlayerHP.Location = new Point(176, 227);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(23, 15);
            lblPlayerHP.TabIndex = 3;
            lblPlayerHP.Text = "HP";
            // 
            // lblPlayerMP
            // 
            lblPlayerMP.AutoSize = true;
            lblPlayerMP.Location = new Point(176, 274);
            lblPlayerMP.Name = "lblPlayerMP";
            lblPlayerMP.Size = new Size(25, 15);
            lblPlayerMP.TabIndex = 4;
            lblPlayerMP.Text = "MP";
            // 
            // btnStartAdventure
            // 
            btnStartAdventure.Location = new Point(489, 133);
            btnStartAdventure.Name = "btnStartAdventure";
            btnStartAdventure.Size = new Size(75, 23);
            btnStartAdventure.TabIndex = 5;
            btnStartAdventure.Text = "Resume";
            btnStartAdventure.UseVisualStyleBackColor = true;
            btnStartAdventure.Click += btnStartAdventure_Click;
            // 
            // btnInventory
            // 
            btnInventory.Location = new Point(489, 188);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(75, 23);
            btnInventory.TabIndex = 6;
            btnInventory.Text = "Inventory";
            btnInventory.UseVisualStyleBackColor = true;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnExitHome
            // 
            btnExitHome.Location = new Point(489, 245);
            btnExitHome.Name = "btnExitHome";
            btnExitHome.Size = new Size(75, 23);
            btnExitHome.TabIndex = 7;
            btnExitHome.Text = "Exit";
            btnExitHome.UseVisualStyleBackColor = true;
            btnExitHome.Click += btnExitHome_Click;
            // 
            // lblPlayerEXP
            // 
            lblPlayerEXP.AutoSize = true;
            lblPlayerEXP.Location = new Point(176, 178);
            lblPlayerEXP.Name = "lblPlayerEXP";
            lblPlayerEXP.Size = new Size(26, 15);
            lblPlayerEXP.TabIndex = 8;
            lblPlayerEXP.Text = "Exp";
            // 
            // lblPlayerGold
            // 
            lblPlayerGold.AutoSize = true;
            lblPlayerGold.Location = new Point(237, 338);
            lblPlayerGold.Name = "lblPlayerGold";
            lblPlayerGold.Size = new Size(32, 15);
            lblPlayerGold.TabIndex = 9;
            lblPlayerGold.Text = "Gold";
            // 
            // HomeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblPlayerGold);
            Controls.Add(lblPlayerEXP);
            Controls.Add(btnExitHome);
            Controls.Add(btnInventory);
            Controls.Add(btnStartAdventure);
            Controls.Add(lblPlayerMP);
            Controls.Add(lblPlayerHP);
            Controls.Add(lblPlayerLevel);
            Controls.Add(lblPlayerName);
            Controls.Add(lblTitle);
            Name = "HomeControl";
            Size = new Size(741, 416);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblPlayerName;
        private Label lblPlayerLevel;
        private Label lblPlayerHP;
        private Label lblPlayerMP;
        private Button btnStartAdventure;
        private Button btnInventory;
        private Button btnExitHome;
        private Label lblPlayerEXP;
        private Label lblPlayerGold;
    }
}
