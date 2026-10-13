using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using RPG.DoiTuong;
namespace RPG.Controls
{
    public partial class MapControl : UserControl
    {

        private GameState gameState;
        private readonly List<(Enemy Enemy, PictureBox Icon)> mapEnemies = new();
        public event EventHandler BattleRequested;
        public event EventHandler InventoryRequested;
        public event EventHandler HomeRequested;
        public event EventHandler MapExitRequested;
        public event EventHandler ResetMapRequested;
        private int playerSpeed = 10;

        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private List<Control> walls;
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.Focus();
        }
        public MapControl(GameState gameState)
        {
            InitializeComponent();

            this.gameState = gameState;

            LoadPlayer();
            walls = new List<Control> { pnlWall1, pnlWall2 };
            LoadMapEnemies();
            ConfigureMap();
            gameTimer.Start();

        }
        private void ConfigureMap()
        {
            int exitTop;

            switch (gameState.CurrentMapId)
            {
                case "Map01":
                    lblMapName.Text = "Bản đồ 01 - Rừng";
                    BackColor = Color.DarkSeaGreen;
                    lblMapName.ForeColor = Color.Black;

                    exitTop = (ClientSize.Height - pnlMapExit.Height) / 2;
                    break;

                case "Map02":
                    lblMapName.Text = "Bản đồ 02 - Núi";
                    BackColor = Color.LightSteelBlue;
                    lblMapName.ForeColor = Color.Black;

                    exitTop = 80;
                    break;

                case "Map03":
                    lblMapName.Text = "Bản đồ 03 - Hang động";
                    BackColor = Color.DimGray;
                    lblMapName.ForeColor = Color.White;

                    exitTop = ClientSize.Height - pnlMapExit.Height - 50;
                    break;

                default:
                    lblMapName.Text = gameState.CurrentMapId;
                    BackColor = Color.DarkSeaGreen;
                    lblMapName.ForeColor = Color.Black;

                    exitTop = ClientSize.Height / 2;
                    break;
            }

            // Đặt cửa gần cạnh phải
            pnlMapExit.Left = Math.Max(
                0,
                ClientSize.Width - pnlMapExit.Width - 5
            );

            // Đảm bảo cửa nằm trong bản đồ
            int maxTop = Math.Max(
                0,
                ClientSize.Height - pnlMapExit.Height
            );

            exitTop = Math.Max(0, Math.Min(exitTop, maxTop));

            pnlMapExit.Top = exitTop;
            // Chỉ hiện cổng khi không còn quái nào trên bản đồ hiện tại.
            pnlMapExit.Visible = !gameState.Enemies.Any(enemy => enemy.MapId == gameState.CurrentMapId);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (gameState != null && pnlMapExit != null)
            {
                ConfigureMap();
            }
        }
        private void LoadPlayer()
        {
            lblMapName.Text = gameState.CurrentMapId;

            picPlayer.Left = gameState.Player.X;
            picPlayer.Top = gameState.Player.Y;
            string playerImagePath = Path.Combine(AppContext.BaseDirectory, "Assets", "Steve_Dwarf.png");
            if (File.Exists(playerImagePath))
            {
                using Image source = Image.FromFile(playerImagePath);
                picPlayer.Image = new Bitmap(source);
            }
        }

        private void LoadMapEnemies()
        {
            foreach (Enemy enemy in gameState.Enemies.Where(enemy => enemy.MapId == gameState.CurrentMapId))
            {
                PictureBox icon = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Location = new Point(enemy.X, enemy.Y),
                    Size = new Size(30, 36),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Name = "mob_" + enemy.Id
                };

                string fileName = enemy.MobType == "Skeleton"
                    ? "Skeleton_Left_Handed.png"
                    : "Zombie_Adult.png";
                string imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", fileName);
                if (File.Exists(imagePath))
                {
                    using Image source = Image.FromFile(imagePath);
                    icon.Image = new Bitmap(source);
                }

                mapEnemies.Add((enemy, icon));
                Controls.Add(icon);
                icon.BringToFront();
            }
        }

        public void SetMoveUp(bool value)
        {
            moveUp = value;
        }

        public void SetMoveDown(bool value)
        {
            moveDown = value;
        }

        public void SetMoveLeft(bool value)
        {
            moveLeft = value;
        }

        public void SetMoveRight(bool value)
        {
            moveRight = value;
        }
        private void MovePlayer(int deltaX, int deltaY)
        {
            // Tính toán vị trí mới của người chơi dựa trên deltaX và deltaY
            int newX = picPlayer.Left + deltaX;
            int newY = picPlayer.Top + deltaY;

            // quá mép trái sẽ bị dừng
            if (newX < 0)
                newX = 0;
            // quá mép trên sẽ bị dừng
            if (newY < 0)
                newY = 0;
            // quá mép phải sẽ bị dừng
            if (newX + picPlayer.Width > this.ClientSize.Width)
                newX = this.ClientSize.Width - picPlayer.Width;
            // quá mép dưới sẽ bị dừng
            if (newY + picPlayer.Height > this.ClientSize.Height)
                newY = this.ClientSize.Height - picPlayer.Height;

            // Tạo một Rectangle mới đại diện cho vị trí mới của người chơi và kiểm tra va chạm với các vật cản (ví dụ: tường)
            Rectangle newPlayerRect = new Rectangle(newX, newY, picPlayer.Width, picPlayer.Height);

            if (IsCollidingWithWall(newPlayerRect))
            {
                // Nếu va chạm với tường, không cập nhật vị trí của người chơi
                return;
            }

            // Kiểm tra va chạm với kẻ thù
            Enemy enemy = GetCollidingEnemy(newPlayerRect);

            if (enemy != null)
            {
                gameState.CurrentEnemy = enemy;

                gameTimer.Stop();

                BattleRequested?.Invoke(this, EventArgs.Empty);

                return;
            }
            if (pnlMapExit.Visible && newPlayerRect.IntersectsWith(pnlMapExit.Bounds))
            {
                gameTimer.Stop();

                MapExitRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            // Cập nhật vị trí của người chơi trong gameState
            gameState.Player.X = newX;
            gameState.Player.Y = newY;
            // Cập nhật vị trí của picPlayer trên MapControl
            picPlayer.Left = newX;
            picPlayer.Top = newY;


        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int deltaX = 0;
            int deltaY = 0;

            if (moveUp)
                deltaY -= playerSpeed;
            if (moveDown)
                deltaY += playerSpeed;
            if (moveLeft)
                deltaX -= playerSpeed;
            if (moveRight)
                deltaX += playerSpeed;

            MovePlayer(deltaX, deltaY);
        }
        private bool IsCollidingWithWall(Rectangle playerRect)
        {
            foreach (Control wall in walls)
            {
                if (playerRect.IntersectsWith(wall.Bounds))
                {
                    return true;
                }
            }

            return false;
        }
        private Enemy GetCollidingEnemy(Rectangle playerRect)
        {
            foreach ((Enemy enemy, PictureBox icon) in mapEnemies)
            {
                if (icon.Visible && playerRect.IntersectsWith(icon.Bounds))
                    return enemy;
            }

            return null;
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            InventoryRequested?.Invoke(this, EventArgs.Empty);
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            HomeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnResetMap_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            ResetMapRequested?.Invoke(this, EventArgs.Empty);
        }
    }

}
