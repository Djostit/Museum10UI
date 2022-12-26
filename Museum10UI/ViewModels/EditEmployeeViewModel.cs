namespace Museum10UI.ViewModels
{
    public class EditEmployeeViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly EmployeeService _employeeService;
        public Visibility Visibility { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        private string ErrorMessage { get;set; }
        public EditEmployeeViewModel(PageService pageService, EmployeeService employeeService)
        {
            _pageService = pageService;
            _employeeService = employeeService;
            if(Global.CurrentEmployee == null)
                Visibility = Visibility.Collapsed;
            else
               Visibility = Visibility.Visible;

            Code = Global.CurrentEmployee?.Code;
            FullName = Global.CurrentEmployee?.FullName;
        }
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            Global.CurrentEmployee = null;
            _pageService.ChangePage(new MainPage());
        });
        public AsyncCommand SaveCommand => new(async () =>
        {
            if (Global.CurrentEmployee != null)
            {
                await _employeeService.UpdateCurrentEmployee(Code, FullName);
                Global.CurrentEmployee = null;
            }
            else
                await _employeeService.AddEmployee(Code, FullName);
            _pageService.ChangePage(new MainPage());
        }, bool () => 
        {
            if (string.IsNullOrWhiteSpace(Code))
                ErrorMessage = "Пусто";
            else if (string.IsNullOrWhiteSpace(FullName))
                ErrorMessage = "Пусто";
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true;return false;
        });
        public AsyncCommand DeleteCommand => new(async () =>
        {
            await _employeeService.DeleteEmployee();
            Global.CurrentEmployee = null;
            _pageService.ChangePage(new MainPage());
        });
    }
}
