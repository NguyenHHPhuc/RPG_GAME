using System;
using System.Collections.Generic;
using System.Text;


namespace RPG.DoiTuong
{
    public class GameSave
    {
        // Khóa chính của bản lưu.
        public int Id { get; set; }

        // Tên bản lưu do người chơi đặt.
        public string SaveName { get; set; } = "Save 1";

        // Bản đồ tại thời điểm lưu.
        public string CurrentMapId { get; set; } = "StartArea";

        // Đánh dấu người chơi đã thắng Boss cuối hay chưa.
        public bool IsGameWon { get; set; }

        // Toàn bộ dữ liệu chi tiết của GameState dạng JSON.
        public string SaveData { get; set; } = "{}";

        // Thời điểm lưu dữ liệu.
        public DateTime SavedAt { get; set; } = DateTime.Now;
    }
}