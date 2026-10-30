using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RPG.DoiTuong;

namespace RPG.Services
{
    public sealed class SaveGameService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // Lưu mới hoặc cập nhật bản lưu có cùng tên.
        // Trả về Id của bản lưu trong SQL Server.
        public int SaveGame(GameState gameState, string saveName)
        {
            if (gameState == null)
                throw new ArgumentNullException(nameof(gameState));

            if (gameState.Player == null)
                throw new InvalidOperationException(
                    "GameState chưa có dữ liệu Player.");

            if (string.IsNullOrWhiteSpace(saveName))
                throw new ArgumentException(
                    "Tên bản lưu không được để trống.",
                    nameof(saveName));

            saveName = saveName.Trim();

            // 1. Chuyển trạng thái game thành snapshot.
            GameSaveSnapshot snapshot =
                GameSaveSnapshot.FromGameState(gameState);

            // 2. Chuyển snapshot thành chuỗi JSON.
            string json = JsonSerializer.Serialize(
                snapshot,
                JsonOptions);

            // 3. Kết nối database để lưu.
            using GameDbContext db = new GameDbContext();

            // Nếu tên bản lưu đã tồn tại, cập nhật bản lưu đó.
            // Nếu chưa có, tạo một bản lưu mới.
            GameSave? save = db.GameSaves
                .FirstOrDefault(item => item.SaveName == saveName);

            if (save == null)
            {
                save = new GameSave
                {
                    SaveName = saveName
                };

                db.GameSaves.Add(save);
            }

            save.SaveName = saveName;
            save.CurrentMapId = gameState.CurrentMapId ?? "StartArea";
            save.IsGameWon = gameState.IsGameWon;
            save.SaveData = json;
            save.SavedAt = DateTime.Now;

            // 4. Ghi thay đổi xuống SQL Server.
            db.SaveChanges();

            return save.Id;
        }

        // Lấy danh sách các bản lưu để hiển thị ở giao diện Load Game.
        public List<GameSave> GetAllSaves()
        {
            using GameDbContext db = new GameDbContext();

            return db.GameSaves
                .AsNoTracking()
                .OrderByDescending(item => item.SavedAt)
                .ToList();
        }

        // Tải một bản lưu theo khóa chính Id.
        public GameState LoadGame(int saveId)
        {
            using GameDbContext db = new GameDbContext();

            GameSave? save = db.GameSaves
                .AsNoTracking()
                .FirstOrDefault(item => item.Id == saveId);

            if (save == null)
            {
                throw new InvalidOperationException(
                    $"Không tìm thấy bản lưu có Id = {saveId}.");
            }

            // Chuyển JSON trở lại thành snapshot.
            GameSaveSnapshot? snapshot =
                JsonSerializer.Deserialize<GameSaveSnapshot>(
                    save.SaveData,
                    JsonOptions);

            if (snapshot == null)
            {
                throw new InvalidOperationException(
                    "Dữ liệu bản lưu không hợp lệ hoặc đang bị trống.");
            }

            // Tái tạo GameState từ snapshot.
            GameState gameState = snapshot.ToGameState();

            // Đồng bộ thêm thông tin từ các cột SQL.
            gameState.CurrentMapId = save.CurrentMapId;
            gameState.IsGameWon = save.IsGameWon;

            return gameState;
        }
        public bool DeleteSave(int saveId)
        {
            using var context = new GameDbContext();

            GameSave save = context.GameSaves
                .FirstOrDefault(s => s.Id == saveId);

            if (save == null)
            {
                return false;
            }

            context.GameSaves.Remove(save);
            context.SaveChanges();

            return true;
        }
    }
}