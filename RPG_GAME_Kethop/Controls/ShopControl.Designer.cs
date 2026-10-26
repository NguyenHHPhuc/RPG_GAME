namespace RPG.Controls
{
    partial class ShopControl
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
            lblShopGold = new Label();
            lblShopTitle = new Label();
            btnCloseShop = new Button();
            lstShopItems = new ListBox();
            lblItemDescription = new Label();
            lblItemPrice = new Label();
            btnBuyItem = new Button();
            SuspendLayout();
            // 
            // lblShopGold
            // 
            lblShopGold.AutoSize = true;
            lblShopGold.Location = new Point(423, 455);
            lblShopGold.Name = "lblShopGold";
            lblShopGold.Size = new Size(32, 15);
            lblShopGold.TabIndex = 0;
            lblShopGold.Text = "Gold";
            // 
            // lblShopTitle
            // 
            lblShopTitle.AutoSize = true;
            lblShopTitle.Font = new Font("Segoe UI", 28F);
            lblShopTitle.Location = new Point(501, 62);
            lblShopTitle.Name = "lblShopTitle";
            lblShopTitle.Size = new Size(214, 51);
            lblShopTitle.TabIndex = 1;
            lblShopTitle.Text = "CỬA HÀNG";
            // 
            // btnCloseShop
            // 
            btnCloseShop.Location = new Point(513, 588);
            btnCloseShop.Name = "btnCloseShop";
            btnCloseShop.Size = new Size(75, 23);
            btnCloseShop.TabIndex = 2;
            btnCloseShop.Text = "Đóng";
            btnCloseShop.UseVisualStyleBackColor = true;
            btnCloseShop.Click += btnCloseShop_Click;
            // 
            // lstShopItems
            // 
            lstShopItems.FormattingEnabled = true;
            lstShopItems.Location = new Point(423, 131);
            lstShopItems.Name = "lstShopItems";
            lstShopItems.Size = new Size(363, 289);
            lstShopItems.TabIndex = 3;
            // 
            // lblItemDescription
            // 
            lblItemDescription.AutoSize = true;
            lblItemDescription.Location = new Point(792, 187);
            lblItemDescription.Name = "lblItemDescription";
            lblItemDescription.Size = new Size(38, 15);
            lblItemDescription.TabIndex = 4;
            lblItemDescription.Text = "Mô tả";
            // 
            // lblItemPrice
            // 
            lblItemPrice.AutoSize = true;
            lblItemPrice.Location = new Point(792, 131);
            lblItemPrice.Name = "lblItemPrice";
            lblItemPrice.Size = new Size(24, 15);
            lblItemPrice.TabIndex = 5;
            lblItemPrice.Text = "Giá";
            // 
            // btnBuyItem
            // 
            btnBuyItem.Location = new Point(874, 397);
            btnBuyItem.Name = "btnBuyItem";
            btnBuyItem.Size = new Size(75, 23);
            btnBuyItem.TabIndex = 6;
            btnBuyItem.Text = "Mua";
            btnBuyItem.UseVisualStyleBackColor = true;
            // 
            // ShopControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnBuyItem);
            Controls.Add(lblItemPrice);
            Controls.Add(lblItemDescription);
            Controls.Add(lstShopItems);
            Controls.Add(btnCloseShop);
            Controls.Add(lblShopTitle);
            Controls.Add(lblShopGold);
            Name = "ShopControl";
            Size = new Size(1280, 720);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblShopGold;
        private Label lblShopTitle;
        private Button btnCloseShop;
        private ListBox lstShopItems;
        private Label lblItemDescription;
        private Label lblItemPrice;
        private Button btnBuyItem;
    }
}
