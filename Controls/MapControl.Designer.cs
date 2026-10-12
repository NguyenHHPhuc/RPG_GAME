namespace RPG.Controls
{
    partial class MapControl
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
            components = new System.ComponentModel.Container();
            lblMapName = new Label();
            pnlPlayer = new Panel();
            gameTimer = new System.Windows.Forms.Timer(components);
            pnlEnemy1 = new Panel();
            btnInventory = new Button();
            btnHome = new Button();
            pnlMapExit = new Panel();
            lblPlayerLevel = new Label();
            lblPlayerEXP = new Label();
            lblSkillPoints = new Label();
            progressPlayerEXP = new ProgressBar();
            SuspendLayout();
            // 
            // lblMapName
            // 
            lblMapName.AutoSize = true;
            lblMapName.Location = new Point(12, 516);
            lblMapName.Name = "lblMapName";
            lblMapName.Size = new Size(90, 15);
            lblMapName.TabIndex = 0;
            lblMapName.Text = "Home (Map 01)";
            // 
            // pnlPlayer
            // 
            pnlPlayer.BackColor = SystemColors.Highlight;
            pnlPlayer.Location = new Point(590, 132);
            pnlPlayer.Name = "pnlPlayer";
            pnlPlayer.Size = new Size(30, 30);
            pnlPlayer.TabIndex = 1;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 20;
            gameTimer.Tick += gameTimer_Tick;
            // 
            // pnlEnemy1
            // 
            pnlEnemy1.BackColor = Color.Firebrick;
            pnlEnemy1.ForeColor = SystemColors.ControlText;
            pnlEnemy1.Location = new Point(400, 250);
            pnlEnemy1.Name = "pnlEnemy1";
            pnlEnemy1.Size = new Size(30, 30);
            pnlEnemy1.TabIndex = 4;
            // 
            // btnInventory
            // 
            btnInventory.Location = new Point(23, 12);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(88, 23);
            btnInventory.TabIndex = 5;
            btnInventory.Text = "INVENTORY";
            btnInventory.UseVisualStyleBackColor = true;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnHome
            // 
            btnHome.Location = new Point(128, 12);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(75, 23);
            btnHome.TabIndex = 6;
            btnHome.Text = "HOME";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // pnlMapExit
            // 
            pnlMapExit.BackColor = Color.Lime;
            pnlMapExit.Location = new Point(732, 431);
            pnlMapExit.Name = "pnlMapExit";
            pnlMapExit.Size = new Size(35, 100);
            pnlMapExit.TabIndex = 7;
            // 
            // lblPlayerLevel
            // 
            lblPlayerLevel.AutoSize = true;
            lblPlayerLevel.Location = new Point(422, 11);
            lblPlayerLevel.Name = "lblPlayerLevel";
            lblPlayerLevel.Size = new Size(18, 15);
            lblPlayerLevel.TabIndex = 8;
            lblPlayerLevel.Text = "Lv";
            // 
            // lblPlayerEXP
            // 
            lblPlayerEXP.AutoSize = true;
            lblPlayerEXP.Location = new Point(270, 37);
            lblPlayerEXP.Name = "lblPlayerEXP";
            lblPlayerEXP.Size = new Size(27, 15);
            lblPlayerEXP.TabIndex = 9;
            lblPlayerEXP.Text = "EXP";
            // 
            // lblSkillPoints
            // 
            lblSkillPoints.AutoSize = true;
            lblSkillPoints.Location = new Point(487, 11);
            lblSkillPoints.Name = "lblSkillPoints";
            lblSkillPoints.Size = new Size(59, 15);
            lblSkillPoints.TabIndex = 10;
            lblSkillPoints.Text = "Skill Point";
            // 
            // progressPlayerEXP
            // 
            progressPlayerEXP.Location = new Point(270, 12);
            progressPlayerEXP.Name = "progressPlayerEXP";
            progressPlayerEXP.Size = new Size(146, 14);
            progressPlayerEXP.TabIndex = 11;
            // 
            // MapControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(progressPlayerEXP);
            Controls.Add(lblSkillPoints);
            Controls.Add(lblPlayerEXP);
            Controls.Add(lblPlayerLevel);
            Controls.Add(pnlMapExit);
            Controls.Add(btnHome);
            Controls.Add(btnInventory);
            Controls.Add(pnlEnemy1);
            Controls.Add(pnlPlayer);
            Controls.Add(lblMapName);
            Name = "MapControl";
            Size = new Size(1500, 700);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMapName;
        private Panel pnlPlayer;
        private System.Windows.Forms.Timer gameTimer;
        private Panel pnlEnemy1;
        private Button btnInventory;
        private Button btnHome;
        private Panel pnlMapExit;
        private Label lblPlayerLevel;
        private Label lblPlayerEXP;
        private Label lblSkillPoints;
        private ProgressBar progressPlayerEXP;
    }
}
