using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG.Controls
{
    public partial class VictoryControl : UserControl
    {
        public event EventHandler BackToMainMenuClicked;
        public VictoryControl()
        {
            InitializeComponent();
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            BackToMainMenuClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
