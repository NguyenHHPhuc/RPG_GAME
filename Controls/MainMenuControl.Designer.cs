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
            lblTitle = new Label();
            btnNewGame = new Button();
            btnLoadGame = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F);
            lblTitle.Location = new Point(69, 26);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(176, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "RPG GAME";
            // 
            // btnNewGame
            // 
            btnNewGame.Anchor = AnchorStyles.None;
            btnNewGame.Location = new Point(114, 108);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(75, 23);
            btnNewGame.TabIndex = 1;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // btnLoadGame
            // 
            btnLoadGame.Anchor = AnchorStyles.None;
            btnLoadGame.Location = new Point(114, 149);
            btnLoadGame.Name = "btnLoadGame";
            btnLoadGame.Size = new Size(75, 23);
            btnLoadGame.TabIndex = 2;
            btnLoadGame.Text = "Load Game";
            btnLoadGame.UseVisualStyleBackColor = true;
            btnLoadGame.Click += btnLoadGame_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.None;
            btnExit.Location = new Point(114, 191);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // MainMenuControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnExit);
            Controls.Add(btnLoadGame);
            Controls.Add(btnNewGame);
            Controls.Add(lblTitle);
            Name = "MainMenuControl";
            Size = new Size(304, 250);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnNewGame;
        private Button btnLoadGame;
        private Button btnExit;
    }
}
