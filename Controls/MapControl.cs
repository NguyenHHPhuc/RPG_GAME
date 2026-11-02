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
        private MapData mapData;
        private Image mapBackgroundImage;
        // Ba khung hình hoạt ảnh đứng yên của nhân vật
        private Bitmap[] playerIdleFrames;

        // Khung hình đang hiển thị
        private int playerIdleFrameIndex = 0;

        // Bộ đếm số lần Timer chạy
        private int idleAnimationTick = 0;

        // Cứ 6 lần Timer chạy thì chuyển khung hình
        private const int IdleFrameDelay = 6;

        // Các khung hình animation di chuyển
        private Bitmap[] playerWalkFrames;
        // Animation di chuyển nhìn sang trái
        private Bitmap[] playerWalkLeftFrames;


        // Xác định hướng nhân vật đang nhìn
        // false = phải, true = trái
        private bool playerFacingLeft = false;

        // Lưu hướng ở lần cập nhật animation trước
        private bool lastWalkFacingLeft = false;

        // Khung hình di chuyển đang hiển thị
        private int playerWalkFrameIndex = 0;

        // Bộ đếm tốc độ animation di chuyển
        private int walkAnimationTick = 0;

        // Xác định trạng thái animation ở lần cập nhật trước
        private bool wasPlayerMoving = false;

        // Cứ 3 lần Timer chạy thì chuyển khung hình di chuyển
        private const int WalkFrameDelay = 3;
        private Panel shopPanel;
        // Lưu mỗi quái vật cùng Panel hiển thị tương ứng trên bản đồ
        private readonly Dictionary<Enemy, Panel> enemyPanels = new Dictionary<Enemy, Panel>();
        private readonly List<Panel> obstaclePanels = new List<Panel>();

        private const bool ShowObstacleDebug = false; // Hiển thị các vật cản (tường) để debug
        public event EventHandler BattleRequested;
        public event EventHandler InventoryRequested;
        public event EventHandler HomeRequested;
        public event EventHandler MapExitRequested;
        public event EventHandler ShopRequested;
        public event EventHandler StatsRequested;
        public event EventHandler SkillTreeRequested;
        public event EventHandler SaveRequested;

        private Button btnStats;
        private Button btnSkillTree;

        private int playerSpeed = 5;

        private bool moveUp;
        private bool moveDown;
        private bool moveLeft;
        private bool moveRight;

        private void CreateProgressionButtons()
        {
            btnSkillTree = new Button
            {
                Name = "btnSkillTree",
                Text = "CÂY KỸ NĂNG",
                Size = new Size(170, 32),
                Location = new Point(ClientSize.Width - 15 - 170, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                UseVisualStyleBackColor = true
            };

            btnStats = new Button
            {
                Name = "btnStats",
                Text = "CHỈ SỐ",
                Size = new Size(120, 32),
                Location = new Point(btnSkillTree.Left - 8 - 120, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                UseVisualStyleBackColor = true
            };

            btnStats.Click += btnStats_Click;
            btnSkillTree.Click += btnSkillTree_Click;

            Controls.Add(btnStats);
            Controls.Add(btnSkillTree);
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            StopMapMovement();
            StatsRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnSkillTree_Click(object sender, EventArgs e)
        {
            StopMapMovement();
            SkillTreeRequested?.Invoke(this, EventArgs.Empty);
        }
        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            StopMapMovement();

            try
            {
                SaveRequested?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                ResumeMovement();
            }
        }

        private void StopMapMovement()
        {
            gameTimer.Stop();
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }

        private void LoadPlayerIdleFrames()
        {
            // Lấy ảnh sprite từ Resources
            Bitmap spriteSheet =
                global::RPG.Properties.Resources.Cat_idle_1;

            // File hiện tại có 3 khung hình nằm ngang
            int frameCount = 3;

            int frameWidth = spriteSheet.Width / frameCount;
            int frameHeight = spriteSheet.Height;

            playerIdleFrames = new Bitmap[frameCount];

            // Cắt từng khung hình từ ảnh gốc
            for (int i = 0; i < frameCount; i++)
            {
                Rectangle frameRect = new Rectangle(
                    i * frameWidth,
                    0,
                    frameWidth,
                    frameHeight
                );

                playerIdleFrames[i] = spriteSheet.Clone(
                    frameRect,
                    spriteSheet.PixelFormat
                );
            }

            // Hiển thị khung hình đầu tiên
            playerIdleFrameIndex = 0;
            pnlPlayer.BackgroundImage = playerIdleFrames[0];

            // Giữ tỷ lệ hình ảnh khi hiển thị
            pnlPlayer.BackgroundImageLayout = ImageLayout.Zoom;
            pnlPlayer.BackColor = Color.Transparent;
        }
        private void UpdatePlayerAnimation(bool isMoving, bool isFacingLeft)
        {
            // Chọn bộ animation đi bộ theo hướng nhìn
            Bitmap[] currentWalkFrames = isFacingLeft
                ? playerWalkLeftFrames
                : playerWalkFrames;

            // =================================
            // 1. NHÂN VẬT ĐANG DI CHUYỂN
            // =================================
            if (isMoving &&
                currentWalkFrames != null &&
                currentWalkFrames.Length > 0)
            {
                // Dừng bộ đếm Idle
                idleAnimationTick = 0;

                // Vừa bắt đầu di chuyển
                if (!wasPlayerMoving)
                {
                    wasPlayerMoving = true;

                    walkAnimationTick = 0;
                    playerWalkFrameIndex = 0;
                    lastWalkFacingLeft = isFacingLeft;

                    pnlPlayer.BackgroundImage =
                        currentWalkFrames[playerWalkFrameIndex];

                    return;
                }

                // Nếu đổi hướng trái/phải
                if (lastWalkFacingLeft != isFacingLeft)
                {
                    lastWalkFacingLeft = isFacingLeft;

                    walkAnimationTick = 0;

                    pnlPlayer.BackgroundImage =
                        currentWalkFrames[playerWalkFrameIndex];
                }

                // Chuyển frame đi bộ
                walkAnimationTick++;

                if (walkAnimationTick >= WalkFrameDelay)
                {
                    walkAnimationTick = 0;

                    playerWalkFrameIndex++;

                    if (playerWalkFrameIndex >= currentWalkFrames.Length)
                    {
                        playerWalkFrameIndex = 0;
                    }

                    pnlPlayer.BackgroundImage =
                        currentWalkFrames[playerWalkFrameIndex];
                }

                return;
            }

            // =================================
            // 2. NHÂN VẬT VỪA DỪNG DI CHUYỂN
            // =================================
            if (wasPlayerMoving)
            {
                wasPlayerMoving = false;

                // Reset bộ đếm đi bộ
                walkAnimationTick = 0;
                playerWalkFrameIndex = 0;

                // Reset animation Idle
                idleAnimationTick = 0;
                playerIdleFrameIndex = 0;

                // Chuyển ngay về frame đầu tiên của Idle
                if (playerIdleFrames != null &&
                    playerIdleFrames.Length > 0)
                {
                    pnlPlayer.BackgroundImage =
                        playerIdleFrames[0];
                }

                return;
            }

            // =================================
            // 3. NHÂN VẬT ĐANG ĐỨNG YÊN
            // =================================
            if (playerIdleFrames == null ||
                playerIdleFrames.Length == 0)
            {
                return;
            }

            idleAnimationTick++;

            if (idleAnimationTick >= IdleFrameDelay)
            {
                idleAnimationTick = 0;

                playerIdleFrameIndex++;

                // Hết frame Idle thì quay lại frame đầu
                if (playerIdleFrameIndex >= playerIdleFrames.Length)
                {
                    playerIdleFrameIndex = 0;
                }

                // Hiển thị frame Idle
                pnlPlayer.BackgroundImage =
                    playerIdleFrames[playerIdleFrameIndex];
            }
        }
        private void LoadPlayerWalkFrames()
        {
            // Lấy sprite sheet từ Resources
            Bitmap spriteSheet =
                global::RPG.Properties.Resources.Cat_walk_1;

            // Ảnh có 3 khung hình nằm ngang
            int frameCount = 3;

            int frameWidth = spriteSheet.Width / frameCount;
            int frameHeight = spriteSheet.Height;

            // Tạo hai bộ animation: phải và trái
            playerWalkFrames = new Bitmap[frameCount];
            playerWalkLeftFrames = new Bitmap[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                Rectangle frameRect = new Rectangle(
                    i * frameWidth,
                    0,
                    frameWidth,
                    frameHeight
                );

                // Cắt khung hình gốc nhìn sang phải
                playerWalkFrames[i] = spriteSheet.Clone(
                    frameRect,
                    spriteSheet.PixelFormat
                );

                // Sao chép khung hình gốc
                playerWalkLeftFrames[i] =
                    (Bitmap)playerWalkFrames[i].Clone();

                // Lật ngang bản sao để nhân vật nhìn sang trái
                playerWalkLeftFrames[i].RotateFlip(
                    RotateFlipType.RotateNoneFlipX
                );
            }

            playerWalkFrameIndex = 0;
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.Focus();
        }
        public void ResumeMovement()
        {
            // Xóa trạng thái phím đang giữ
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;

            // Khởi động lại Timer
            gameTimer.Start();
        }
        public MapControl(GameState gameState, MapData mapData)
        {

            InitializeComponent();
            CreateProgressionButtons();

            this.gameState = gameState;
            this.mapData = mapData;

            LoadMapBackground();

            LoadObstacles();
            LoadPlayer();
            LoadEnemies();
            LoadShop();
            ConfigureMap();

            LoadPlayerIdleFrames();
            LoadPlayerWalkFrames();

            UpdatePlayerProgress();

            // Các nút luôn nằm trên background, vật cản và enemy.
            btnStats.BringToFront();
            btnSkillTree.BringToFront();
            gameTimer.Start();

        }
        private void LoadMapBackground()
        {
            string fileName = mapData.Id switch
            {
                "StartArea" => "StartArea_BG.png",
                "DungeonFloor1" => "DungeonFloor1_BG.png",
                "DungeonFloor2" => "DungeonFloor2_BG.png",

                _ => null
            };

            // Không có background tương ứng.
            // Bản đồ sẽ tiếp tục sử dụng màu nền mặc định.
            if (string.IsNullOrEmpty(fileName))
            {
                BackgroundImage = null;
                return;
            }

            string imagePath = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                fileName
            );

            // Nếu chưa tìm thấy file, dùng màu nền hiện tại.
            if (!File.Exists(imagePath))
            {
                BackgroundImage = null;
                return;
            }

            // Tạo bản sao để không khóa file PNG.
            using (Image source = Image.FromFile(imagePath))
            {
                mapBackgroundImage = new Bitmap(source);
            }

            BackgroundImage = mapBackgroundImage;

            // Để ảnh phủ kín vùng chơi.
            BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void ConfigureMap()
        {
            // Cập nhật tên và màu chữ của bản đồ
            lblMapName.Text = $"{mapData.Id} - {mapData.Name}";
            pnlMapExit.Visible = mapData.Id != "DungeonFloor2";
            BackColor = mapData.BackgroundColor;
            lblMapName.ForeColor = mapData.TextColor;

            int maxTop = Math.Max(
                0,
                ClientSize.Height - pnlMapExit.Height
            );

            if (mapData.Id == "StartArea")
            {
                // Căn vùng tương tác vào cổng ở phía trên bên phải
                pnlMapExit.Left =
                    (int)(ClientSize.Width * 0.82)
                    - pnlMapExit.Width / 2;

                pnlMapExit.Top =
                    (int)(ClientSize.Height * 0.12)
                    - pnlMapExit.Height / 2;

                // Giữ hình cổng trong background nhìn thấy được
                pnlMapExit.BackColor = Color.Transparent;
                pnlMapExit.BorderStyle = BorderStyle.None;
            }
            else if (mapData.Id == "DungeonFloor1")
            {
                // Tỷ lệ vị trí cổng trên background
                double exitXRatio = 0.95;
                double exitYRatio = mapData.ExitVerticalRatio;

                // Đặt tâm vùng tương tác tại tọa độ mong muốn
                pnlMapExit.Left = (int)(
                    ClientSize.Width * exitXRatio
                    - pnlMapExit.Width / 2.0
                );

                pnlMapExit.Top = (int)(
                    ClientSize.Height * exitYRatio
                    - pnlMapExit.Height / 2.0
                );

                // Đảm bảo Panel không vượt khỏi vùng bản đồ
                pnlMapExit.Left = Math.Max(
                    0,
                    Math.Min(
                        pnlMapExit.Left,
                        ClientSize.Width - pnlMapExit.Width
                    )
                );

                pnlMapExit.Top = Math.Max(
                    0,
                    Math.Min(
                        pnlMapExit.Top,
                        ClientSize.Height - pnlMapExit.Height
                    )
                );
                // Che cổng exit map, xóa đi ba dòng để hiển thị
                pnlMapExit.BackColor = Color.Transparent;
                pnlMapExit.BackgroundImage = null;
                pnlMapExit.BorderStyle = BorderStyle.None;
            }
            else
            {
                // Giữ cách đặt lối ra cũ cho các bản đồ khác
                int exitTop = (int)(
                    maxTop * mapData.ExitVerticalRatio
                );

                pnlMapExit.Left = Math.Max(
                    0,
                    ClientSize.Width - pnlMapExit.Width - 5
                );

                pnlMapExit.Top = Math.Max(
                    0,
                    Math.Min(exitTop, maxTop)
                );
            }

            // Đặt vùng tương tác cửa hàng vào vị trí cửa hàng trên ảnh
            if (shopPanel != null)
            {
                shopPanel.Left =
                    (int)(ClientSize.Width * 0.19)
                    - shopPanel.Width / 2;

                shopPanel.Top =
                    (int)(ClientSize.Height * 0.52)
                    - shopPanel.Height / 2;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (gameState != null && pnlMapExit != null)
            {
                ConfigureMap();
            }
        }
        private void UpdatePlayerProgress()
        {
            Player player = gameState.Player;

            // Hiển thị cấp độ và điểm kỹ năng
            lblPlayerLevel.Text = $"Level: {player.Level}";
            lblSkillPoints.Text = $"Điểm kỹ năng: {player.SkillPoints}";
            btnStats.Text = $"CHỈ SỐ ({player.StatPoints})";
            btnSkillTree.Text = $"CÂY KỸ NĂNG ({player.SkillPoints})";

            // Cập nhật thanh tiến trình EXP
            int expRequired = Math.Max(1, player.ExperienceToNextLevel
            );

            progressPlayerEXP.Minimum = 0;
            progressPlayerEXP.Maximum = expRequired;

            // Đảm bảo Value không vượt quá Maximum
            progressPlayerEXP.Value = Math.Max(0, Math.Min(player.Experience, expRequired)
            );

            // Hiển thị lượng EXP hiện tại
            lblPlayerEXP.Text = $"EXP: {player.Experience} / {expRequired}";
        }
        private void LoadPlayer()
        {
            lblMapName.Text = gameState.CurrentMapId;

            pnlPlayer.Left = gameState.Player.X;
            pnlPlayer.Top = gameState.Player.Y;
        }
        private Image LoadEnemySprite(Enemy enemy)
        {
            string fileName = enemy.Name?.Trim().ToLowerInvariant() switch
            {
                "slime" => "Slime.png",
                "skeleton" => "Skeleton_Left_Handed.png",
                "zombie" => "Zombie_Adult.png",

                // Enemy ở DungeonFloor2
                "demon" => "Demon.png",
                "yeti" => "Yeti.png",
                "boss" => "Boss.png",

                // Ảnh mặc định nếu tên enemy không khớp
                _ => "Zombie_Adult.png"
            };

            string imagePath = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                fileName
            );

            // Nếu chưa có ảnh, trả về null để dùng màu thay thế
            if (!File.Exists(imagePath))
                return null;

            // Tạo bản sao để không khóa file PNG
            using (Image source = Image.FromFile(imagePath))
            {
                return new Bitmap(source);
            }
        }
        private void LoadEnemies()
        {
            // Xóa những Panel quái vật được tạo động từ lần tải trước
            foreach (Panel panel in enemyPanels.Values)
            {
                // Giải phóng ảnh sprite cũ
                if (panel.BackgroundImage != null)
                {
                    panel.BackgroundImage.Dispose();
                    panel.BackgroundImage = null;
                }

                if (panel != pnlEnemy1)
                {
                    Controls.Remove(panel);
                    panel.Dispose();
                }
            }

            enemyPanels.Clear();

            // Lấy tất cả quái vật thuộc bản đồ hiện tại
            List<Enemy> mapEnemies = gameState.Enemies
                .Where(enemy => enemy.MapId == gameState.CurrentMapId)
                .ToList();

            // Ẩn Panel mặc định nếu không có quái
            pnlEnemy1.Visible = false;

            for (int i = 0; i < mapEnemies.Count; i++)
            {
                Enemy enemy = mapEnemies[i];
                Panel enemyPanel;

                if (i == 0)
                {
                    // Quái đầu tiên dùng Panel có sẵn trong Designer
                    enemyPanel = pnlEnemy1;
                }
                else
                {
                    // Các quái tiếp theo được tạo riêng
                    enemyPanel = new Panel
                    {
                        Name = $"pnlEnemy_{i + 1}",
                        Size = pnlEnemy1.Size
                    };

                    Controls.Add(enemyPanel);
                }

                // Chọn kích thước hiển thị theo từng loại enemy
                string enemyName = enemy.Name?.Trim().ToLowerInvariant() ?? "";

                Size enemySize = enemyName switch
                {
                    "slime" => new Size(44, 44),
                    "skeleton" => new Size(52, 60),
                    "zombie" => new Size(52, 60),
                    "demon" => new Size(52, 60),
                    "yeti" => new Size(52, 60),
                    "boss" => new Size(110, 110),

                    _ => pnlEnemy1.Size
                };

                // Gán kích thước và vị trí cho Panel
                enemyPanel.Size = enemySize;
                enemyPanel.Left = enemy.X;
                enemyPanel.Top = enemy.Y;

                // Giữ tỷ lệ hình ảnh
                enemyPanel.BackgroundImageLayout = ImageLayout.Zoom;

                // Tải hình ảnh tương ứng với loại quái
                Image enemyImage = LoadEnemySprite(enemy);

                if (enemyImage != null)
                {
                    enemyPanel.BackgroundImage = enemyImage;
                    enemyPanel.BackgroundImageLayout =
                        ImageLayout.Zoom;

                    enemyPanel.BackColor = Color.Transparent;
                    enemyPanel.BorderStyle = BorderStyle.None;
                }
                else
                {
                    // Hình thay thế khi chưa có sprite
                    enemyPanel.BackgroundImage = null;

                    if (enemy.Name.IndexOf(
                        "Slime",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        enemyPanel.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        enemyPanel.BackColor = Color.Firebrick;
                    }

                    enemyPanel.BorderStyle = BorderStyle.FixedSingle;
                }

                enemyPanel.Visible = true;

                // Liên kết quái với Panel tương ứng
                enemyPanels.Add(enemy, enemyPanel);
            }
            foreach (Panel enemyPanel in enemyPanels.Values)
            {
                enemyPanel.BringToFront();
            }

            // Đưa người chơi và những thành phần giao diện quan trọng lên phía trước
            pnlPlayer.BringToFront();
            pnlMapExit.BringToFront();
            lblMapName.BringToFront();
            btnHome.BringToFront();
            btnInventory.BringToFront();
        }
        private void LoadObstacles()
        {
            // Xóa các Panel vật cản đã tạo trước đó
            foreach (Panel panel in obstaclePanels)
            {
                Controls.Remove(panel);
                panel.Dispose();
            }

            obstaclePanels.Clear();

            // Tạo các Panel theo dữ liệu của Map hiện tại
            foreach (MapObstacle obstacle in mapData.Obstacles)
            {
                Panel wall = new Panel
                {
                    Name = $"pnl_{obstacle.Id}",

                    Left = obstacle.X,
                    Top = obstacle.Y,

                    Width = obstacle.Width,
                    Height = obstacle.Height,

                    BackColor = ShowObstacleDebug
                        ? Color.Magenta
                        : Color.Transparent,

                    BorderStyle = ShowObstacleDebug
                        ? BorderStyle.FixedSingle
                        : BorderStyle.None
                };

                Controls.Add(wall);
                // hiển thì tên các vật cản để debug
                if (ShowObstacleDebug)
                {
                    Label lblObstacleName = new Label
                    {
                        Name = $"lbl_{obstacle.Id}",

                        // Hiện tên vật cản
                        Text = obstacle.Id,

                        Dock = DockStyle.Fill,
                        AutoSize = false,

                        TextAlign = ContentAlignment.MiddleCenter,

                        ForeColor = Color.White,
                        BackColor = Color.Transparent,

                        Font = new Font(
                            "Arial",
                            8,
                            FontStyle.Bold
                        )
                    };

                    wall.Controls.Add(lblObstacleName);
                    lblObstacleName.BringToFront();
                }

                obstaclePanels.Add(wall);

                // Đưa vật cản xuống dưới Player và Enemy
                wall.SendToBack();
            }

            // Đảm bảo các đối tượng game nằm trên vật cản
            pnlPlayer.BringToFront();
            pnlEnemy1.BringToFront();
            pnlMapExit.BringToFront();
            lblMapName.BringToFront();

            // Nếu MapControl có hai nút này, giữ chúng ở phía trước
            btnHome.BringToFront();
            btnInventory.BringToFront();
        }
        private void LoadShop()
        {
            // Cửa hàng chỉ xuất hiện tại StartArea
            if (mapData.Id != "StartArea")
                return;

            // Panel này chỉ dùng để phát hiện va chạm,
            // hình cửa hàng đã có sẵn trong background.
            shopPanel = new Panel
            {
                Name = "pnlShop",
                Size = new Size(80, 65),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };

            // Thêm vùng tương tác vào bản đồ
            Controls.Add(shopPanel);

            // Căn vị trí dựa trên background
            shopPanel.Left =
                (int)(ClientSize.Width * 0.19)
                - shopPanel.Width / 2;

            shopPanel.Top =
                (int)(ClientSize.Height * 0.52)
                - shopPanel.Height / 2;

            // Đảm bảo nhân vật và giao diện vẫn hiển thị phía trước
            shopPanel.BringToFront();

            foreach (Panel enemyPanel in enemyPanels.Values)
                enemyPanel.BringToFront();

            pnlPlayer.BringToFront();
            pnlMapExit.BringToFront();
            lblMapName.BringToFront();
            btnHome.BringToFront();
            btnInventory.BringToFront();
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
            if (shopPanel != null && newPlayerRect.IntersectsWith(shopPanel.Bounds))
            {
                // Dừng di chuyển khi mở cửa hàng
                gameTimer.Stop();

                moveUp = false;
                moveDown = false;
                moveLeft = false;
                moveRight = false;

                // Thông báo cho Form1 mở ShopControl
                ShopRequested?.Invoke(this, EventArgs.Empty);

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
            // Cập nhật vị trí của pnlPlayer trên MapControl
            pnlPlayer.Left = newX;
            pnlPlayer.Top = newY;


        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Khai báo lượng di chuyển theo chiều ngang và dọc
            int deltaX = 0;
            int deltaY = 0;

            // Xác định hướng di chuyển
            if (moveUp)
                deltaY -= playerSpeed;

            if (moveDown)
                deltaY += playerSpeed;

            if (moveLeft)
                deltaX -= playerSpeed;

            if (moveRight)
                deltaX += playerSpeed;

            // Lưu vị trí cũ
            int oldX = pnlPlayer.Left;
            int oldY = pnlPlayer.Top;

            // Di chuyển nhân vật
            MovePlayer(deltaX, deltaY);

            // Xác định hướng nhìn trái hoặc phải
            if (pnlPlayer.Left < oldX)
            {
                playerFacingLeft = true;
            }
            else if (pnlPlayer.Left > oldX)
            {
                playerFacingLeft = false;
            }

            // Kiểm tra nhân vật có thực sự di chuyển không
            bool isMoving =
                pnlPlayer.Left != oldX ||
                pnlPlayer.Top != oldY;

            // Cập nhật animation
            UpdatePlayerAnimation(isMoving, playerFacingLeft);
        }
        private bool IsCollidingWithWall(Rectangle playerRect)
        {
            foreach (Panel wall in obstaclePanels)
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
            foreach (KeyValuePair<Enemy, Panel> pair in enemyPanels)
            {
                Panel enemyPanel = pair.Value;

                if (enemyPanel.Visible &&
                    playerRect.IntersectsWith(enemyPanel.Bounds))
                {
                    // Trả về đúng quái vật mà người chơi vừa chạm vào
                    return pair.Key;
                }
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

        private void lblMapName_Click(object sender, EventArgs e)
        {

        }
    }

}
