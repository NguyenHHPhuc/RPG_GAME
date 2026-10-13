using System.Text.Json;
using RPG.DoiTuong;

namespace RPG.Services
{
    public static class SaveSlotService
    {
        public const int MaximumSlots = 3;

        private static string SaveFilePath
        {
            get
            {
                // Khi chạy trong Visual Studio, ba cấp thư mục phía trên bin là thư mục project.
                string projectFolder = Path.GetFullPath(Path.Combine(
                    AppContext.BaseDirectory, "..", "..", ".."));
                return Path.Combine(projectFolder, "SaveData", "save-slots.json");
            }
        }

        public static List<SaveSlot?> LoadSlots()
        {
            try
            {
                if (File.Exists(SaveFilePath))
                {
                    List<SaveSlot?>? savedSlots = JsonSerializer.Deserialize<List<SaveSlot?>>(File.ReadAllText(SaveFilePath));
                    return Enumerable.Range(1, MaximumSlots)
                        .Select(number => savedSlots?.FirstOrDefault(slot => slot?.SlotNumber == number))
                        .ToList();
                }
            }
            catch (JsonException)
            {
                // File JSON lỗi thì trả về các ô trống để game vẫn mở được.
            }
            catch (IOException)
            {
                // Nếu file đang bận, menu vẫn mở để người chơi thử lại sau.
            }

            return Enumerable.Repeat<SaveSlot?>(null, MaximumSlots).ToList();
        }

        public static void SaveSlot(int slotNumber, GameState state)
        {
            List<SaveSlot?> slots = LoadSlots();
            slots[slotNumber - 1] = new SaveSlot
            {
                SlotNumber = slotNumber,
                State = state
            };

            Directory.CreateDirectory(Path.GetDirectoryName(SaveFilePath)!);
            File.WriteAllText(SaveFilePath, JsonSerializer.Serialize(slots, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }

        public static void DeleteSlot(int slotNumber)
        {
            List<SaveSlot?> slots = LoadSlots();
            slots[slotNumber - 1] = null;
            Directory.CreateDirectory(Path.GetDirectoryName(SaveFilePath)!);
            File.WriteAllText(SaveFilePath, JsonSerializer.Serialize(slots, new JsonSerializerOptions
            {
                WriteIndented = true
            }));
        }
    }
}
