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
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.Font = new Font("Segoe UI", 15F);
            lblTitle.Location = new Point(442, 38);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Túi đồ";
            // 
            // lstItems
            // 
            lstItems.FormattingEnabled = true;
            lstItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstItems.Location = new Point(180, 130);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(280, 360);
            lstItems.TabIndex = 1;
            lstItems.SelectedIndexChanged += lstItems_SelectedIndexChanged;
            // 
            // btnUse
            // 
            btnUse.Location = new Point(325, 560);
            btnUse.Anchor = AnchorStyles.Bottom;
            btnUse.Name = "btnUse";
            btnUse.Size = new Size(160, 44);
            btnUse.TabIndex = 2;
            btnUse.Text = "Dùng";
            btnUse.UseVisualStyleBackColor = true;
            btnUse.Click += btnUse_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(515, 560);
            btnClose.Anchor = AnchorStyles.Bottom;
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(160, 44);
            btnClose.TabIndex = 3;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblItemInfo
            // 
            lblItemInfo.AutoSize = true;
            lblItemInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblItemInfo.AutoSize = false;
            lblItemInfo.Location = new Point(490, 130);
            lblItemInfo.Name = "lblItemInfo";
            lblItemInfo.Size = new Size(340, 120);
            lblItemInfo.TabIndex = 4;
            lblItemInfo.Text = "Chọn một vật phẩm để xem thông tin.";
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
            Size = new Size(1000, 650);
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
