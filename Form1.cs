using RPG.Controls;
using RPG.DoiTuong;
using RPG.Services;

namespace RPG
{
    public partial class Form1 : Form
    {

        GameState gameState;
        private UserControl currentControl;
        private int activeSlotNumber = -1;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;

            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            FormClosing += (_, _) => SaveCurrentSlot();
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
            ShowSlotMenu(true);
        }
        public void Menu_LoadGameClicked(object sender, EventArgs e)
        {
            ShowSlotMenu(false);
        }

        private void ShowSlotMenu(bool creatingNewGame)
        {
            SaveSlotControl slots = new SaveSlotControl(creatingNewGame);
            slots.GameReady += SlotMenu_GameReady;
            slots.BackClicked += (_, _) => ShowMainMenu();
            ShowControl(slots);
        }

        private void SlotMenu_GameReady(object? sender, SaveSlotEventArgs e)
        {
            activeSlotNumber = e.SlotNumber;
            gameState = e.State;
            SaveCurrentSlot();
            ShowMap();
        }

        private void SaveCurrentSlot()
        {
            // Khi đang đánh nhau thì chờ xong trận mới ghi trạng thái ổn định.
            if (activeSlotNumber > 0 && gameState != null && gameState.CurrentEnemy == null)
                SaveSlotService.SaveSlot(activeSlotNumber, gameState);
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
                SaveCurrentSlot();
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
            switch (gameState.CurrentMapId)
            {
                case "Map01":
                    gameState.CurrentMapId = "Map02";

                    gameState.Player.X = 30;
                    gameState.Player.Y = 100;
                    break;

                case "Map02":
                    gameState.CurrentMapId = "Map03";

                    gameState.Player.X = 30;
                    gameState.Player.Y = 250;
                    break;

                case "Map03":
                    gameState.CurrentMapId = "Map01";

                    gameState.Player.X = 30;
                    gameState.Player.Y = 200;
                    break;
            }

            ShowMap();
        }

        private void ShowMap()
        {
            // Tạo một instance của MapControl và truyền gameState vào constructor
            MapControl map = new MapControl(gameState);

            // Đăng ký sự kiện BattleRequested từ MapControl để xử lý khi người chơi gặp kẻ thù
            map.BattleRequested += Map_BattleRequested;
            map.InventoryRequested += Map_InventoryRequested;
            map.HomeRequested += Map_HomeRequested;
            map.MapExitRequested += Map_MapExitRequested;
            map.ResetMapRequested += Map_ResetMapRequested;
            // Hiển thị MapControl trong panelMain
            ShowControl(map);
        }

        private void Map_ResetMapRequested(object? sender, EventArgs e)
        {
            gameState.ResetCurrentMapEnemies();
            ShowMap();
        }
        private void ShowBattle()
        {
            // Tạo một instance của BattleControl và truyền gameState vào constructor
            BattleControl battle = new BattleControl(gameState);
            battle.BattleWon += Battle_BattleWon;
            battle.BattleLost += Battle_BattleLost;
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
            // Giữ nguyên ô lưu đã được ghi an toàn trước trận thua.
            activeSlotNumber = -1;
            ShowMainMenu();
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
    }
}
