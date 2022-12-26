namespace Museum10UI.ViewModels
{
    public class EditExhibitsViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly ExhibitsService _exhibitsService;
        public Visibility Visibility { get; set; }
        public string Id { get; set; }
        public string DateReceipt { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Status { get; set; }
        private string ErrorMessage { get; set; }
        public EditExhibitsViewModel(PageService pageService, ExhibitsService exhibitsService)
        {
            _pageService = pageService;
            _exhibitsService = exhibitsService;
            if (Global.CurrentExhibits == null)
                Visibility = Visibility.Collapsed;
            else
                Visibility = Visibility.Visible;

            Id = Global.CurrentExhibits?.Id.ToString();
            DateReceipt = Global.CurrentExhibits?.DateReceipt;
            Name = Global.CurrentExhibits?.Name;
            Place = Global.CurrentExhibits?.Place;
            Status = Global.CurrentExhibits?.Status;
        }
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            Global.CurrentExhibits = null;
            _pageService.ChangePage(new MainPage());
        });
        public AsyncCommand SaveCommand => new(async () =>
        {
            if (Global.CurrentExhibits != null)
            {
                await _exhibitsService.UpdateCurrentExhibits(Id, DateReceipt, Name, Place, Status);
                Global.CurrentExhibits = null;
            }
            else
                await _exhibitsService.AddExhibits(Id, DateReceipt, Name, Place, Status);
            _pageService.ChangePage(new MainPage());
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(Id)
                || !Id.All(char.IsDigit))
                ErrorMessage = "Пусто";
            else if (string.IsNullOrWhiteSpace(DateReceipt))
                ErrorMessage = "Пусто";
            else if (string.IsNullOrWhiteSpace(Name))
                ErrorMessage = "Пусто";
            else if (string.IsNullOrWhiteSpace(Place))
                ErrorMessage = "Пусто";
            else if (string.IsNullOrWhiteSpace(Status))
                ErrorMessage = "Пусто";
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;
        });
        public AsyncCommand DeleteCommand => new(async () =>
        {
            await _exhibitsService.DeleteExhibits();
            Global.CurrentExhibits = null;
            _pageService.ChangePage(new MainPage());
        });
    }
}
