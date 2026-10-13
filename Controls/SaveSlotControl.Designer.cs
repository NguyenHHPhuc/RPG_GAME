namespace RPG.Controls
{
    partial class SaveSlotControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnSlot1 = new Button();
            btnSlot2 = new Button();
            btnSlot3 = new Button();
            pnlDetails = new Panel();
            lblDetailsTitle = new Label();
            lblPlayerName = new Label();
            txtPlayerName = new TextBox();
            lblPlayerClass = new Label();
            cmbPlayerClass = new ComboBox();
            lblClassDescription = new Label();
            btnAction = new Button();
            btnDelete = new Button();
            btnBack = new Button();
            pnlDetails.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.Location = new Point(72, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(282, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Tạo nhân vật mới";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.Location = new Point(76, 82);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(329, 19);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Chọn một ô trống để tạo nhân vật hoặc tải game đã lưu.";
            //
            // btnSlot1
            //
            btnSlot1.FlatStyle = FlatStyle.Flat;
            btnSlot1.Location = new Point(78, 145);
            btnSlot1.Name = "btnSlot1";
            btnSlot1.Size = new Size(350, 82);
            btnSlot1.TabIndex = 2;
            btnSlot1.Text = "Ô lưu 1";
            btnSlot1.TextAlign = ContentAlignment.MiddleLeft;
            btnSlot1.UseVisualStyleBackColor = false;
            btnSlot1.Click += btnSlot_Click;
            //
            // btnSlot2
            //
            btnSlot2.FlatStyle = FlatStyle.Flat;
            btnSlot2.Location = new Point(78, 242);
            btnSlot2.Name = "btnSlot2";
            btnSlot2.Size = new Size(350, 82);
            btnSlot2.TabIndex = 3;
            btnSlot2.Text = "Ô lưu 2";
            btnSlot2.TextAlign = ContentAlignment.MiddleLeft;
            btnSlot2.UseVisualStyleBackColor = false;
            btnSlot2.Click += btnSlot_Click;
            //
            // btnSlot3
            //
            btnSlot3.FlatStyle = FlatStyle.Flat;
            btnSlot3.Location = new Point(78, 339);
            btnSlot3.Name = "btnSlot3";
            btnSlot3.Size = new Size(350, 82);
            btnSlot3.TabIndex = 4;
            btnSlot3.Text = "Ô lưu 3";
            btnSlot3.TextAlign = ContentAlignment.MiddleLeft;
            btnSlot3.UseVisualStyleBackColor = false;
            btnSlot3.Click += btnSlot_Click;
            //
            // pnlDetails
            //
            pnlDetails.BackColor = Color.White;
            pnlDetails.BorderStyle = BorderStyle.FixedSingle;
            pnlDetails.Controls.Add(lblDetailsTitle);
            pnlDetails.Controls.Add(lblPlayerName);
            pnlDetails.Controls.Add(txtPlayerName);
            pnlDetails.Controls.Add(lblPlayerClass);
            pnlDetails.Controls.Add(cmbPlayerClass);
            pnlDetails.Controls.Add(lblClassDescription);
            pnlDetails.Controls.Add(btnAction);
            pnlDetails.Location = new Point(455, 145);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new Size(430, 276);
            pnlDetails.TabIndex = 5;
            //
            // lblDetailsTitle
            //
            lblDetailsTitle.AutoSize = true;
            lblDetailsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDetailsTitle.Location = new Point(22, 18);
            lblDetailsTitle.Name = "lblDetailsTitle";
            lblDetailsTitle.Size = new Size(121, 21);
            lblDetailsTitle.TabIndex = 0;
            lblDetailsTitle.Text = "Thông tin nhân vật";
            //
            // lblPlayerName
            //
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(24, 61);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(78, 15);
            lblPlayerName.TabIndex = 1;
            lblPlayerName.Text = "Tên nhân vật";
            //
            // txtPlayerName
            //
            txtPlayerName.Location = new Point(24, 82);
            txtPlayerName.MaxLength = 20;
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.Size = new Size(396, 23);
            txtPlayerName.TabIndex = 2;
            //
            // lblPlayerClass
            //
            lblPlayerClass.AutoSize = true;
            lblPlayerClass.Location = new Point(24, 119);
            lblPlayerClass.Name = "lblPlayerClass";
            lblPlayerClass.Size = new Size(36, 15);
            lblPlayerClass.TabIndex = 3;
            lblPlayerClass.Text = "Lớp nhân vật";
            //
            // cmbPlayerClass
            //
            cmbPlayerClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlayerClass.FormattingEnabled = true;
            cmbPlayerClass.Items.AddRange(new object[] { "Chiến binh", "Cung thủ", "Đỡ đòn" });
            cmbPlayerClass.Location = new Point(24, 140);
            cmbPlayerClass.Name = "cmbPlayerClass";
            cmbPlayerClass.Size = new Size(396, 23);
            cmbPlayerClass.TabIndex = 4;
            cmbPlayerClass.SelectedIndexChanged += cmbPlayerClass_SelectedIndexChanged;
            //
            // lblClassDescription
            //
            lblClassDescription.Location = new Point(24, 177);
            lblClassDescription.Name = "lblClassDescription";
            lblClassDescription.Size = new Size(396, 38);
            lblClassDescription.TabIndex = 5;
            lblClassDescription.Text = "";
            //
            // btnAction
            //
            btnAction.Location = new Point(260, 220);
            btnAction.Name = "btnAction";
            btnAction.Size = new Size(160, 38);
            btnAction.TabIndex = 6;
            btnAction.Text = "Tạo nhân vật";
            btnAction.UseVisualStyleBackColor = true;
            btnAction.Click += btnAction_Click;
            //
            // btnDelete
            //
            btnDelete.Location = new Point(475, 445);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(160, 38);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Xóa ô lưu";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            //
            // btnBack
            //
            btnBack.Location = new Point(700, 520);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(160, 38);
            btnBack.TabIndex = 7;
            btnBack.Text = "Quay lại";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            //
            // SaveSlotControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(btnBack);
            Controls.Add(btnDelete);
            Controls.Add(pnlDetails);
            Controls.Add(btnSlot3);
            Controls.Add(btnSlot2);
            Controls.Add(btnSlot1);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Name = "SaveSlotControl";
            Size = new Size(1000, 650);
            pnlDetails.ResumeLayout(false);
            pnlDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnSlot1;
        private Button btnSlot2;
        private Button btnSlot3;
        private Panel pnlDetails;
        private Label lblDetailsTitle;
        private Label lblPlayerName;
        private TextBox txtPlayerName;
        private Label lblPlayerClass;
        private ComboBox cmbPlayerClass;
        private Label lblClassDescription;
        private Button btnAction;
        private Button btnDelete;
        private Button btnBack;
    }
}
