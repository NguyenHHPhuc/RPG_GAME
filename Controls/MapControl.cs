using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using RPG.DoiTuong;
namespace RPG.Controls
{
    public partial class MapControl : UserControl
    {

        private GameState gameState;
        private Enemy currentMapEnemy;
        public event EventHandler BattleRequested;
        public event EventHandler InventoryRequested;
        public event EventHandler HomeRequested;
        public event EventHandler MapExitRequested;
        private int playerSpeed = 10;

        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;
        private List<Control> walls;
        private List<Control> enemies;
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
            ConfigureMap();
            walls = new List<Control> { pnlWall1, pnlWall2 };
            enemies = new List<Control> { pnlEnemy1 };
            gameTimer.Start();

        }
        private void ConfigureMap()
        {
            int exitTop;

            switch (gameState.CurrentMapId)
            {
                case "Map01":
                    lblMapName.Text = "MAP 01 - FOREST";
                    BackColor = Color.DarkSeaGreen;
                    lblMapName.ForeColor = Color.Black;

                    exitTop = (ClientSize.Height - pnlMapExit.Height) / 2;
                    break;

                case "Map02":
                    lblMapName.Text = "MAP 02 - MOUNTAIN";
                    BackColor = Color.LightSteelBlue;
                    lblMapName.ForeColor = Color.Black;

                    exitTop = 80;
                    break;

                case "Map03":
                    lblMapName.Text = "MAP 03 - CAVE";
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

            pnlPlayer.Left = gameState.Player.X;
            pnlPlayer.Top = gameState.Player.Y;
            currentMapEnemy = gameState.Enemies.FirstOrDefault(e => e.MapId == gameState.CurrentMapId);

            if (currentMapEnemy != null)
            {
                pnlEnemy1.Visible = true;

                pnlEnemy1.Left = currentMapEnemy.X;
                pnlEnemy1.Top = currentMapEnemy.Y;
            }
            else
            {
                pnlEnemy1.Visible = false;
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
            int newX = pnlPlayer.Left + deltaX;
            int newY = pnlPlayer.Top + deltaY;

            // quá mép trái sẽ bị dừng
            if (newX < 0)
                newX = 0;
            // quá mép trên sẽ bị dừng
            if (newY < 0)
                newY = 0;
            // quá mép phải sẽ bị dừng
            if (newX + pnlPlayer.Width > this.ClientSize.Width)
                newX = this.ClientSize.Width - pnlPlayer.Width;
            // quá mép dưới sẽ bị dừng
            if (newY + pnlPlayer.Height > this.ClientSize.Height)
                newY = this.ClientSize.Height - pnlPlayer.Height;

            // Tạo một Rectangle mới đại diện cho vị trí mới của người chơi và kiểm tra va chạm với các vật cản (ví dụ: tường)
            Rectangle newPlayerRect = new Rectangle(newX, newY, pnlPlayer.Width, pnlPlayer.Height);

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
            if (newPlayerRect.IntersectsWith(pnlMapExit.Bounds))
            {
                gameTimer.Stop();

                MapExitRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            // Cập nhật vị trí của người chơi trong gameState
            gameState.Player.X = newX;
            gameState.Player.Y = newY;
            // Cập nhật vị trí của pnlPlayer trên MapControl
            pnlPlayer.Left = newX;
            pnlPlayer.Top = newY;


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
        private bool IsCollidingWithEnemy(Rectangle playerRect)
        {
            foreach (Enemy enemy in gameState.Enemies)
            {
                Rectangle enmyRect = new Rectangle(
                    enemy.X,
                    enemy.Y,
                    pnlEnemy1.Width,
                    pnlEnemy1.Height);
                if (playerRect.IntersectsWith(enmyRect))
                {
                    return true;
                }
            }
            return false;
        }
        private Enemy GetCollidingEnemy(Rectangle playerRect)
        {
            if (currentMapEnemy == null)
                return null;

            Rectangle enemyRect = new Rectangle(
                currentMapEnemy.X,
                currentMapEnemy.Y,
                pnlEnemy1.Width,
                pnlEnemy1.Height
            );

            if (playerRect.IntersectsWith(enemyRect))
                return currentMapEnemy;

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
    }

}
