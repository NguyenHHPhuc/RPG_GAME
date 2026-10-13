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
            picPlayer = new PictureBox();
            gameTimer = new System.Windows.Forms.Timer(components);
            pnlWall1 = new Panel();
            pnlWall2 = new Panel();
            btnInventory = new Button();
            btnHome = new Button();
            btnResetMap = new Button();
            pnlMapExit = new Panel();
            SuspendLayout();
            // 
            // lblMapName
            // 
            lblMapName.AutoSize = true;
            lblMapName.Anchor = AnchorStyles.None;
            lblMapName.Location = new Point(390, 16);
            lblMapName.Name = "lblMapName";
            lblMapName.Size = new Size(90, 15);
            lblMapName.TabIndex = 0;
            lblMapName.Text = "Home (Map 01)";
            // 
            // picPlayer
            // 
            picPlayer.BackColor = Color.Transparent;
            picPlayer.Location = new Point(590, 132);
            picPlayer.Name = "picPlayer";
            picPlayer.Size = new Size(30, 36);
            picPlayer.SizeMode = PictureBoxSizeMode.Zoom;
            picPlayer.TabIndex = 1;
            picPlayer.TabStop = false;
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
            // 
            // btnInventory
            // 
            btnInventory.Location = new Point(23, 12);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(88, 23);
            btnInventory.TabIndex = 5;
            btnInventory.Text = "Túi đồ";
            btnInventory.UseVisualStyleBackColor = true;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnHome
            // 
            btnHome.Location = new Point(128, 12);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(75, 23);
            btnHome.TabIndex = 6;
            btnHome.Text = "Về nhà";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // Nút tạo lại quái để có thể chơi thử bản đồ nhiều lần.
            btnResetMap.Location = new Point(218, 12);
            btnResetMap.Name = "btnResetMap";
            btnResetMap.Size = new Size(100, 23);
            btnResetMap.TabIndex = 8;
            btnResetMap.Text = "Đặt lại map";
            btnResetMap.UseVisualStyleBackColor = true;
            btnResetMap.Click += btnResetMap_Click;
            // 
            // pnlMapExit
            // 
            pnlMapExit.BackColor = Color.Lime;
            pnlMapExit.Location = new Point(960, 275);
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
            Controls.Add(btnResetMap);
            Controls.Add(btnInventory);
            Controls.Add(pnlWall2);
            Controls.Add(pnlWall1);
            Controls.Add(picPlayer);
            Controls.Add(lblMapName);
            Name = "MapControl";
            Size = new Size(1000, 650);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMapName;
        private PictureBox picPlayer;
        private System.Windows.Forms.Timer gameTimer;
        private Panel pnlWall1;
        private Panel pnlWall2;
        private Button btnInventory;
        private Button btnHome;
        private Button btnResetMap;
        private Panel pnlMapExit;
    }
}
