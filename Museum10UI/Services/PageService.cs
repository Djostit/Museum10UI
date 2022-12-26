namespace Museum10UI.Services
{
    public class PageService
    {
        public Action<Page> OnPageChanged;

        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
