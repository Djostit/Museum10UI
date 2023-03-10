namespace Museum10UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;
        public Page PageSource { get; set; }
        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;

            _pageService.ChangePage(new MainPage());
        }
    }
}
