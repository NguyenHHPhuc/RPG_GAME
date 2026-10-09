namespace RPG.Controls
{
    partial class BattleControl
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
            lblPlayerName = new Label();
            lblEnemyName = new Label();
            progressPlayerHP = new ProgressBar();
            progressEnemyHP = new ProgressBar();
            lblPlayerHP = new Label();
            lblEnemyHP = new Label();
            btnAttack = new Button();
            btnSkill = new Button();
            progressPlayerMP = new ProgressBar();
            lblPlayerMP = new Label();
            btnItem = new Button();
            SuspendLayout();
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(82, 45);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(71, 15);
            lblPlayerName.TabIndex = 0;
            lblPlayerName.Text = "PlayerName";
            // 
            // lblEnemyName
            // 
            lblEnemyName.AutoSize = true;
            lblEnemyName.Location = new Point(472, 45);
            lblEnemyName.Name = "lblEnemyName";
            lblEnemyName.Size = new Size(75, 15);
            lblEnemyName.TabIndex = 1;
            lblEnemyName.Text = "EnemyName";
            lblEnemyName.Click += lblEnemyName_Click;
            // 
            // progressPlayerHP
            // 
            progressPlayerHP.Location = new Point(67, 217);
            progressPlayerHP.Name = "progressPlayerHP";
            progressPlayerHP.Size = new Size(100, 23);
            progressPlayerHP.TabIndex = 2;
            // 
            // progressEnemyHP
            // 
            progressEnemyHP.Location = new Point(458, 217);
            progressEnemyHP.Name = "progressEnemyHP";
            progressEnemyHP.Size = new Size(100, 23);
            progressEnemyHP.TabIndex = 3;
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.AutoSize = true;
            lblPlayerHP.Location = new Point(173, 225);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(55, 15);
            lblPlayerHP.TabIndex = 4;
            lblPlayerHP.Text = "PlayerHP";
            // 
            // lblEnemyHP
            // 
            lblEnemyHP.AutoSize = true;
            lblEnemyHP.Location = new Point(564, 225);
            lblEnemyHP.Name = "lblEnemyHP";
            lblEnemyHP.Size = new Size(59, 15);
            lblEnemyHP.TabIndex = 5;
            lblEnemyHP.Text = "EnemyHP";
            // 
            // btnAttack
            // 
            btnAttack.Location = new Point(92, 323);
            btnAttack.Name = "btnAttack";
            btnAttack.Size = new Size(75, 23);
            btnAttack.TabIndex = 6;
            btnAttack.Text = "ATTACK";
            btnAttack.UseVisualStyleBackColor = true;
            btnAttack.Click += btnAttack_Click;
            // 
            // btnSkill
            // 
            btnSkill.Location = new Point(232, 323);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(75, 23);
            btnSkill.TabIndex = 7;
            btnSkill.Text = "SKILL";
            btnSkill.UseVisualStyleBackColor = true;
            btnSkill.Click += btnSkill_Click;
            // 
            // progressPlayerMP
            // 
            progressPlayerMP.Location = new Point(67, 246);
            progressPlayerMP.Name = "progressPlayerMP";
            progressPlayerMP.Size = new Size(100, 23);
            progressPlayerMP.TabIndex = 8;
            // 
            // lblPlayerMP
            // 
            lblPlayerMP.AutoSize = true;
            lblPlayerMP.Location = new Point(173, 254);
            lblPlayerMP.Name = "lblPlayerMP";
            lblPlayerMP.Size = new Size(53, 15);
            lblPlayerMP.TabIndex = 9;
            lblPlayerMP.Text = "PlayeMP";
            // 
            // btnItem
            // 
            btnItem.Location = new Point(92, 373);
            btnItem.Name = "btnItem";
            btnItem.Size = new Size(75, 23);
            btnItem.TabIndex = 10;
            btnItem.Text = "ITEM";
            btnItem.UseVisualStyleBackColor = true;
            btnItem.Click += btnItem_Click;
            // 
            // BattleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnItem);
            Controls.Add(lblPlayerMP);
            Controls.Add(progressPlayerMP);
            Controls.Add(btnSkill);
            Controls.Add(btnAttack);
            Controls.Add(lblEnemyHP);
            Controls.Add(lblPlayerHP);
            Controls.Add(progressEnemyHP);
            Controls.Add(progressPlayerHP);
            Controls.Add(lblEnemyName);
            Controls.Add(lblPlayerName);
            Name = "BattleControl";
            Size = new Size(650, 500);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayerName;
        private Label lblEnemyName;
        private ProgressBar progressPlayerHP;
        private ProgressBar progressEnemyHP;
        private Label lblPlayerHP;
        private Label lblEnemyHP;
        private Button btnAttack;
        private Button btnSkill;
        private ProgressBar progressPlayerMP;
        private Label lblPlayerMP;
        private Button btnItem;
    }
}
