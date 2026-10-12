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
            btnEquip = new Button();
            btnUnequip = new Button();
            lblEquippedWeapon = new Label();
            lblEquippedArmor = new Label();
            lblPlayerDamage = new Label();
            lblPlayerDefense = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F);
            lblTitle.Location = new Point(505, 76);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "INVENTORY";
            // 
            // lstItems
            // 
            lstItems.FormattingEnabled = true;
            lstItems.Location = new Point(268, 160);
            lstItems.Name = "lstItems";
            lstItems.Size = new Size(215, 139);
            lstItems.TabIndex = 1;
            lstItems.SelectedIndexChanged += lstItems_SelectedIndexChanged;
            // 
            // btnUse
            // 
            btnUse.Location = new Point(268, 355);
            btnUse.Name = "btnUse";
            btnUse.Size = new Size(75, 23);
            btnUse.TabIndex = 2;
            btnUse.Text = "USE";
            btnUse.UseVisualStyleBackColor = true;
            btnUse.Click += btnUse_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(416, 355);
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
            lblItemInfo.Location = new Point(505, 160);
            lblItemInfo.Name = "lblItemInfo";
            lblItemInfo.Size = new Size(28, 15);
            lblItemInfo.TabIndex = 4;
            lblItemInfo.Text = "Info";
            // 
            // btnEquip
            // 
            btnEquip.Location = new Point(268, 408);
            btnEquip.Name = "btnEquip";
            btnEquip.Size = new Size(75, 23);
            btnEquip.TabIndex = 5;
            btnEquip.Text = "Trang bị";
            btnEquip.UseVisualStyleBackColor = true;
            btnEquip.Click += btnEquip_Click;
            // 
            // btnUnequip
            // 
            btnUnequip.Location = new Point(414, 408);
            btnUnequip.Name = "btnUnequip";
            btnUnequip.Size = new Size(75, 23);
            btnUnequip.TabIndex = 6;
            btnUnequip.Text = "Tháo trang bị";
            btnUnequip.UseVisualStyleBackColor = true;
            btnUnequip.Click += btnUnequip_Click;
            // 
            // lblEquippedWeapon
            // 
            lblEquippedWeapon.AutoSize = true;
            lblEquippedWeapon.Location = new Point(787, 160);
            lblEquippedWeapon.Name = "lblEquippedWeapon";
            lblEquippedWeapon.Size = new Size(118, 15);
            lblEquippedWeapon.TabIndex = 7;
            lblEquippedWeapon.Text = "Vũ khí: Chưa trang bị";
            // 
            // lblEquippedArmor
            // 
            lblEquippedArmor.AutoSize = true;
            lblEquippedArmor.Location = new Point(787, 210);
            lblEquippedArmor.Name = "lblEquippedArmor";
            lblEquippedArmor.Size = new Size(109, 15);
            lblEquippedArmor.TabIndex = 8;
            lblEquippedArmor.Text = "Giáp: Chưa trang bị";
            // 
            // lblPlayerDamage
            // 
            lblPlayerDamage.AutoSize = true;
            lblPlayerDamage.Location = new Point(961, 160);
            lblPlayerDamage.Name = "lblPlayerDamage";
            lblPlayerDamage.Size = new Size(33, 15);
            lblPlayerDamage.TabIndex = 9;
            lblPlayerDamage.Text = "Dmg";
            // 
            // lblPlayerDefense
            // 
            lblPlayerDefense.AutoSize = true;
            lblPlayerDefense.Location = new Point(961, 210);
            lblPlayerDefense.Name = "lblPlayerDefense";
            lblPlayerDefense.Size = new Size(31, 15);
            lblPlayerDefense.TabIndex = 10;
            lblPlayerDefense.Text = "Giáp";
            // 
            // InventoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblPlayerDefense);
            Controls.Add(lblPlayerDamage);
            Controls.Add(lblEquippedArmor);
            Controls.Add(lblEquippedWeapon);
            Controls.Add(btnUnequip);
            Controls.Add(btnEquip);
            Controls.Add(lblItemInfo);
            Controls.Add(btnClose);
            Controls.Add(btnUse);
            Controls.Add(lstItems);
            Controls.Add(lblTitle);
            Name = "InventoryControl";
            Size = new Size(1280, 720);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private ListBox lstItems;
        private Button btnUse;
        private Button btnClose;
        private Label lblItemInfo;
        private Button btnEquip;
        private Button btnUnequip;
        private Label lblEquippedWeapon;
        private Label lblEquippedArmor;
        private Label lblPlayerDamage;
        private Label lblPlayerDefense;
    }
}
