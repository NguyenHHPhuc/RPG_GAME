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
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.Font = new Font("Segoe UI", 22F);
            lblTitle.Location = new Point(448, 48);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(104, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Nhà";
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(200, 205);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(71, 15);
            lblPlayerName.TabIndex = 1;
            lblPlayerName.Text = "PlayerName";
            // 
            // lblPlayerLevel
            // 
            lblPlayerLevel.AutoSize = true;
            lblPlayerLevel.Location = new Point(200, 255);
            lblPlayerLevel.Name = "lblPlayerLevel";
            lblPlayerLevel.Size = new Size(19, 15);
            lblPlayerLevel.TabIndex = 2;
            lblPlayerLevel.Text = "Cấp";
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.AutoSize = true;
            lblPlayerHP.Location = new Point(200, 305);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(23, 15);
            lblPlayerHP.TabIndex = 3;
            lblPlayerHP.Text = "Máu";
            // 
            // lblPlayerMP
            // 
            lblPlayerMP.AutoSize = true;
            lblPlayerMP.Location = new Point(200, 355);
            lblPlayerMP.Name = "lblPlayerMP";
            lblPlayerMP.Size = new Size(25, 15);
            lblPlayerMP.TabIndex = 4;
            lblPlayerMP.Text = "Năng lượng";
            // 
            // btnStartAdventure
            // 
            btnStartAdventure.Location = new Point(580, 195);
            btnStartAdventure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStartAdventure.Name = "btnStartAdventure";
            btnStartAdventure.Size = new Size(220, 46);
            btnStartAdventure.TabIndex = 5;
            btnStartAdventure.Text = "Tiếp tục";
            btnStartAdventure.UseVisualStyleBackColor = true;
            btnStartAdventure.Click += btnStartAdventure_Click;
            // 
            // btnInventory
            // 
            btnInventory.Location = new Point(580, 265);
            btnInventory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(220, 46);
            btnInventory.TabIndex = 6;
            btnInventory.Text = "Túi đồ";
            btnInventory.UseVisualStyleBackColor = true;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnExitHome
            // 
            btnExitHome.Location = new Point(580, 335);
            btnExitHome.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExitHome.Name = "btnExitHome";
            btnExitHome.Size = new Size(220, 46);
            btnExitHome.TabIndex = 7;
            btnExitHome.Text = "Quay lại";
            btnExitHome.UseVisualStyleBackColor = true;
            btnExitHome.Click += btnExitHome_Click;
            // 
            // HomeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnExitHome);
            Controls.Add(btnInventory);
            Controls.Add(btnStartAdventure);
            Controls.Add(lblPlayerMP);
            Controls.Add(lblPlayerHP);
            Controls.Add(lblPlayerLevel);
            Controls.Add(lblPlayerName);
            Controls.Add(lblTitle);
            Name = "HomeControl";
            Size = new Size(1000, 650);
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
    }
}
