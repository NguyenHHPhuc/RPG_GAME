namespace RPG.DoiTuong
{
    // Một ô lưu chứa trạng thái game hiện tại của một nhân vật.
    public class SaveSlot
    {
        public int SlotNumber { get; set; }
        public GameState State { get; set; } = GameState.CreateNewGame();
    }
}
