using RPG.Controls;
using RPG.DoiTuong;
using RPG.Services;
using System.Linq;
namespace RPG
{
    public partial class Form1 : Form
    {
        
        GameState gameState;
        private UserControl currentControl;
        private MapService mapService = new MapService();
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            // Hiển thị menu chính khi Form1 được khởi tạo
            ShowMainMenu();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentControl is MapControl map)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        map.SetMoveUp(true);
                        break;

                    case Keys.S:
                        map.SetMoveDown(true);
                        break;

                    case Keys.A:
                        map.SetMoveLeft(true);
                        break;

                    case Keys.D:
                        map.SetMoveRight(true);
                        break;
                }
            }
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentControl is MapControl map)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        map.SetMoveUp(false);
                        break;

                    case Keys.S:
                        map.SetMoveDown(false);
                        break;

                    case Keys.A:
                        map.SetMoveLeft(false);
                        break;

                    case Keys.D:
                        map.SetMoveRight(false);
                        break;
                }
            }
        }


        private void ShowMainMenu()
        {
            MainMenuControl menu = new MainMenuControl();
            // Đăng ký các sự kiện từ menu để xử lý khi người dùng nhấn các nút
            // Khi người dùng nhấn nút "New Game", sự kiện NewGameClicked sẽ được kích hoạt và gọi phương thức Menu_NewGameClicked và tương tự cho các nút khác
            menu.NewGameClicked += Menu_NewGameClicked;
            menu.LoadGameClicked += Menu_LoadGameClicked;
            menu.ExitClicked += Menu_ExitClicked;


            // Hiển thị menu chính trong panelMain
            ShowControl(menu);
        }


        public void Menu_NewGameClicked(object sender, EventArgs e)
        {

            gameState = GameState.CreateNewGame();
            ShowMap();
        }
        public void Menu_LoadGameClicked(object sender, EventArgs e)
        {
            // Load game logic here
        }
        public void Menu_ExitClicked(object sender, EventArgs e)
        {
            Close();
        }
        private void ShowControl(UserControl control)
        {
            // Nếu có control hiện tại, hủy nó trước khi hiển thị control mới
            if (currentControl != null)
            {
                currentControl.Dispose();
                currentControl = null;
            }
            panelMain.Controls.Clear();
            currentControl = control;

            control.Dock = DockStyle.Fill;

            panelMain.Controls.Add(control);
        }
        public void Map_BattleRequested(object sender, EventArgs e)
        {
            // Khi sự kiện BattleRequested được kích hoạt từ MapControl, hiển thị BattleControl
            ShowBattle();
        }
        public void Map_InventoryRequested(object sender, EventArgs e)
        {
            // Khi sự kiện InventoryRequested được kích hoạt từ MapControl, hiển thị InventoryControl
            ShowInventory();
        }
        public void Map_HomeRequested(object sender, EventArgs e)
        {
            // Khi sự kiện HomeRequested được kích hoạt từ MapControl, hiển thị HomeControl
            ShowHome();
        }
        private void Map_MapExitRequested(object sender, EventArgs e)
        {
            // Lấy dữ liệu Map hiện tại
            MapData currentMap = mapService.GetMap(gameState.CurrentMapId);

            if (currentMap == null)
            {
                MessageBox.Show("Không tìm thấy Map hiện tại.");
                return;
            }

            // Từ Start Area: vào tầng cao nhất đã mở khóa
            if (!currentMap.IsDungeon)
            {
                int floor = gameState.HighestUnlockedFloor;

                if (floor < 1 || floor > 2)
                {
                    MessageBox.Show("Không có tầng Dungeon hợp lệ.");
                    return;
                }

                MapData nextMap = mapService.GetMap(
                    $"DungeonFloor{floor}"
                );

                if (nextMap == null)
                {
                    MessageBox.Show("Không tìm thấy tầng Dungeon.");
                    return;
                }

                gameState.CurrentDungeonFloor = floor;
                gameState.CurrentMapId = nextMap.Id;

                gameState.Player.X = nextMap.EntryX;
                gameState.Player.Y = nextMap.EntryY;

                ShowMap();
                return;
            }

            // Đang ở Dungeon: kiểm tra còn quái không
            bool hasRemainingEnemies = gameState.Enemies.Any(
                enemy => enemy.MapId == currentMap.Id
            );

            if (hasRemainingEnemies)
            {
                MessageBox.Show( "Hãy đánh bại tất cả quái vật trước khi rời tầng." );
                if(currentControl is MapControl map)
                {
                    map.ResumeMovement();
                }
                return;
            }

            int clearedFloor = currentMap.FloorNumber;

            if (!gameState.ClearedFloors.Contains(clearedFloor))
            {
                gameState.ClearedFloors.Add(clearedFloor);
            }

            // Mở khóa tầng kế tiếp, tối đa tầng 2
            if (clearedFloor < 2)
            {
                gameState.HighestUnlockedFloor = Math.Max(
                    gameState.HighestUnlockedFloor,
                    clearedFloor + 1
                );
            }

            // Sau khi hoàn thành tầng, quay về Start Area
            MapData startArea = mapService.GetMap("StartArea");

            if (startArea == null)
            {
                MessageBox.Show("Không tìm thấy Start Area.");
                return;
            }

            gameState.CurrentMapId = startArea.Id;

            gameState.Player.X = startArea.EntryX;
            gameState.Player.Y = startArea.EntryY;

            ShowMap();
        }

        private void ShowMap()
        {
            MapData mapData = mapService.GetMap(gameState.CurrentMapId);
            // Tạo một instance của MapControl và truyền gameState vào constructor
            MapControl map = new MapControl(gameState, mapData);
            
            // Đăng ký sự kiện BattleRequested từ MapControl để xử lý khi người chơi gặp kẻ thù
            map.BattleRequested += Map_BattleRequested;
            map.InventoryRequested += Map_InventoryRequested;
            map.HomeRequested += Map_HomeRequested;
            map.MapExitRequested += Map_MapExitRequested;
            map.ShopRequested += Map_ShopRequested;
            map.StatsRequested += (_, _) => ShowStats();
            map.SkillTreeRequested += (_, _) => ShowSkillTree();
            // Hiển thị MapControl trong panelMain
            ShowControl(map);
        }
        private void ShowBattle()
        {
            // Tạo một instance của BattleControl và truyền gameState vào constructor
            BattleControl battle = new BattleControl(gameState);
            battle.BattleWon += Battle_BattleWon;
            battle.BattleLost += Battle_BattleLost;
            battle.GameWon += Battle_GameWon;
            // Hiển thị BattleControl trong panelMain
            ShowControl(battle);
        }
        private void Battle_BattleWon(object sender, EventArgs e)
        {
            // Khi người chơi thắng trận đấu, hiển thị bản đồ trở lại
            ShowMap();
        }
        private void Battle_BattleLost(object sender, EventArgs e)
        {
            gameState = GameState.CreateNewGame();

            ShowMainMenu();
        }
        private void Battle_GameWon(object sender, EventArgs e)
        {
            ShowVictory();
        }
        private void ShowInventory()
        {
            InventoryControl inventory = new InventoryControl(gameState);
            inventory.CloseClicked += Inventory_CloseClicked;
            ShowControl(inventory);
        }
        private void Inventory_CloseClicked(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void ShowHome()
        {
            HomeControl home = new HomeControl(gameState);

            home.StartAdventureClicked += Home_StartAdventureClicked;
            home.InventoryClicked += Home_InventoryClicked;
            home.BackToMainMenuClicked += Home_BackToMainMenuClicked;

            ShowControl(home);
        }
        private void Home_StartAdventureClicked(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void Home_InventoryClicked(object sender, EventArgs e)
        {
            ShowInventory();
        }
        private void Home_BackToMainMenuClicked(object sender, EventArgs e)
        {
            ShowMainMenu();
        }
        private void ShowVictory()
        {
            VictoryControl victory = new VictoryControl();

            victory.BackToMainMenuClicked += Victory_BackToMainMenuClicked;

            ShowControl(victory);
        }
        private void Victory_BackToMainMenuClicked(object sender, EventArgs e)
        {
            ShowMainMenu();
        }
        private void Map_ShopRequested(object sender, EventArgs e)
        {
            ShowShop();
        }
        private void ShowShop()
        {
            ShopControl shop = new ShopControl(gameState);

            shop.CloseClicked += Shop_CloseClicked;

            ShowControl(shop);
        }
        private void Shop_CloseClicked(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void ShowStats()
        {
            StatsControl stats = new StatsControl(gameState);
            stats.CloseClicked += (_, _) => ShowMap();
            ShowControl(stats);
        }
        private void ShowSkillTree()
        {
            SkillTreeControl skillTree = new SkillTreeControl(gameState);
            skillTree.CloseClicked += (_, _) => ShowMap();
            ShowControl(skillTree);
        }
    }
}
