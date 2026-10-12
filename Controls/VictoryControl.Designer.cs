namespace RPG.Controls
{
    partial class VictoryControl
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
            lblVictoryTitle = new Label();
            btnBackToMenu = new Button();
            SuspendLayout();
            // 
            // lblVictoryTitle
            // 
            lblVictoryTitle.AutoSize = true;
            lblVictoryTitle.Font = new Font("Segoe UI", 26F);
            lblVictoryTitle.Location = new Point(312, 179);
            lblVictoryTitle.Name = "lblVictoryTitle";
            lblVictoryTitle.Size = new Size(128, 47);
            lblVictoryTitle.TabIndex = 0;
            lblVictoryTitle.Text = "Victory";
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.Location = new Point(333, 335);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(75, 23);
            btnBackToMenu.TabIndex = 1;
            btnBackToMenu.Text = "Về Menu";
            btnBackToMenu.UseVisualStyleBackColor = true;
            btnBackToMenu.Click += btnBackToMenu_Click;
            // 
            // VictoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnBackToMenu);
            Controls.Add(lblVictoryTitle);
            Name = "VictoryControl";
            Size = new Size(820, 550);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblVictoryTitle;
        private Button btnBackToMenu;
    }
}
