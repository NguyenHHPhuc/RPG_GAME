namespace RPG.Controls
{
    partial class MainMenuControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuControl));
            lblTitle = new Label();
            btnNewGame = new Button();
            btnLoadGame = new Button();
            btnExit = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Copperplate Gothic Bold", 24F);
            lblTitle.Location = new Point(655, 140);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(208, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "RPG GAME";
            // 
            // btnNewGame
            // 
            btnNewGame.Anchor = AnchorStyles.None;
            btnNewGame.Location = new Point(620, 355);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(260, 44);
            btnNewGame.TabIndex = 1;
            btnNewGame.Text = "Chơi mới";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // btnLoadGame
            // 
            btnLoadGame.Anchor = AnchorStyles.None;
            btnLoadGame.Location = new Point(620, 415);
            btnLoadGame.Name = "btnLoadGame";
            btnLoadGame.Size = new Size(260, 44);
            btnLoadGame.TabIndex = 2;
            btnLoadGame.Text = "Tải game";
            btnLoadGame.UseVisualStyleBackColor = true;
            btnLoadGame.Click += btnLoadGame_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.None;
            btnExit.Location = new Point(620, 475);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(260, 44);
            btnExit.TabIndex = 3;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.Cat_jump_1;
            pictureBox2.Location = new Point(503, 30);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(101, 110);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(890, 30);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(101, 110);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // MainMenuControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(btnExit);
            Controls.Add(btnLoadGame);
            Controls.Add(btnNewGame);
            Controls.Add(lblTitle);
            Name = "MainMenuControl";
            Size = new Size(1500, 900);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnNewGame;
        private Button btnLoadGame;
        private Button btnExit;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}
