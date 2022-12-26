namespace Museum10UI.Services
{
    public class ExhibitsService
    {
        private const string PATH = "exhibits.json";
        private static List<Exhibits> Exhibits = new();
        private static async Task ReadExhibits()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            else
            {
                Exhibits = JsonConvert.DeserializeObject<List<Exhibits>>(await File.ReadAllTextAsync(PATH));
            }
        }
        private static async Task SaveExhibits()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            await File.WriteAllTextAsync(PATH, JsonConvert.SerializeObject(Exhibits, Formatting.Indented));
        }
        public async Task AddExhibits(string Id, string DateReceipt, string Name, string Place, string Status)
        {
            await ReadExhibits();

            Exhibits.Add(new Exhibits
            {
                Id = int.Parse(Id),
                DateReceipt = DateReceipt,
                Name = Name,
                Place = Place,
                Status = Status,
            });

            await SaveExhibits();
        }
        public async Task DeleteExhibits()
        {
            await ReadExhibits();

            Exhibits.RemoveAt(Exhibits.FindIndex(e => e.Id.Equals(Global.CurrentExhibits.Id)));

            await SaveExhibits();
        }
        public List<Exhibits> GetExhibits()
        {
            ReadExhibits().GetAwaiter();
            return Exhibits;
        }
        public async Task UpdateCurrentExhibits(string Id, string DateReceipt, string Name, string Place, string Status)
        {
            if (Global.CurrentExhibits == null
                || Global.CurrentExhibits.Id == null)
                return;
            Exhibits[Exhibits.FindIndex(e => e.Id.Equals(Global.CurrentExhibits.Id))] = new Exhibits
            {
                Id = int.Parse(Id),
                DateReceipt = DateReceipt,
                Name = Name,
                Place = Place,
                Status = Status,
            };
            await SaveExhibits();
        }
    }
}
