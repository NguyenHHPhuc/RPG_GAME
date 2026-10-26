using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RPG.Controls
{
    public partial class MainMenuControl : UserControl
    {
        // tao ra một sự kiện để thông báo cho Form1 khi người dùng nhấn nút "New Game", "Load Game" hoặc "Exit"
        public event EventHandler NewGameClicked;
        public event EventHandler LoadGameClicked;
        public event EventHandler ExitClicked;
        public MainMenuControl()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            // Gọi sự kiện NewGameClicked khi người dùng nhấn nút "New Game"
            // Sử dụng EventArgs.Empty để truyền vào sự kiện mà không cần dữ liệu bổ sung
            // Cú pháp ?.Invoke giúp tránh lỗi nếu không có người đăng ký sự kiện
            // this là người gửi sự kiện
            NewGameClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            LoadGameClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
