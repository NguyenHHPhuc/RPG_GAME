namespace RPG.Controls
{
    partial class InventoryControl
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
            lstItems = new ListBox();
            btnUse = new Button();
            btnClose = new Button();
            lblItemInfo = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F);
            lblTitle.Location = new Point(153, 38);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "INVENTORY";
            // 
            // lstItems
            // 
            lstItems.FormattingEnabled = true;
            lstItems.Location = new Point(149, 102);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(120, 94);
            lstItems.TabIndex = 1;
            lstItems.SelectedIndexChanged += lstItems_SelectedIndexChanged;
            // 
            // btnUse
            // 
            btnUse.Location = new Point(76, 332);
            btnUse.Name = "btnUse";
            btnUse.Size = new Size(75, 23);
            btnUse.TabIndex = 2;
            btnUse.Text = "USE";
            btnUse.UseVisualStyleBackColor = true;
            btnUse.Click += btnUse_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(224, 332);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 3;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblItemInfo
            // 
            lblItemInfo.AutoSize = true;
            lblItemInfo.Location = new Point(330, 109);
            lblItemInfo.Name = "lblItemInfo";
            lblItemInfo.Size = new Size(28, 15);
            lblItemInfo.TabIndex = 4;
            lblItemInfo.Text = "Info";
            // 
            // InventoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblItemInfo);
            Controls.Add(btnClose);
            Controls.Add(btnUse);
            Controls.Add(lstItems);
            Controls.Add(lblTitle);
            Name = "InventoryControl";
            Size = new Size(511, 513);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private ListBox lstItems;
        private Button btnUse;
        private Button btnClose;
        private Label lblItemInfo;
    }
}
