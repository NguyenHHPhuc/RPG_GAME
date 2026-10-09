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
            pnlWall1 = new Panel();
            pnlWall2 = new Panel();
            pnlEnemy1 = new Panel();
            btnInventory = new Button();
            btnHome = new Button();
            pnlMapExit = new Panel();
            SuspendLayout();
            // 
            // lblMapName
            // 
            lblMapName.AutoSize = true;
            lblMapName.Location = new Point(641, 12);
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
            // pnlWall1
            // 
            pnlWall1.BackColor = SystemColors.ActiveCaptionText;
            pnlWall1.Location = new Point(280, 280);
            pnlWall1.Name = "pnlWall1";
            pnlWall1.Size = new Size(200, 40);
            pnlWall1.TabIndex = 2;
            // 
            // pnlWall2
            // 
            pnlWall2.BackColor = SystemColors.ActiveCaptionText;
            pnlWall2.Location = new Point(280, 200);
            pnlWall2.Name = "pnlWall2";
            pnlWall2.Size = new Size(200, 40);
            pnlWall2.TabIndex = 3;
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
            // MapControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMapExit);
            Controls.Add(btnHome);
            Controls.Add(btnInventory);
            Controls.Add(pnlEnemy1);
            Controls.Add(pnlWall2);
            Controls.Add(pnlWall1);
            Controls.Add(pnlPlayer);
            Controls.Add(lblMapName);
            Name = "MapControl";
            Size = new Size(820, 550);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMapName;
        private Panel pnlPlayer;
        private System.Windows.Forms.Timer gameTimer;
        private Panel pnlWall1;
        private Panel pnlWall2;
        private Panel pnlEnemy1;
        private Button btnInventory;
        private Button btnHome;
        private Panel pnlMapExit;
    }
}
