namespace Museum10UI.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly EmployeeService _employeeService;
        private readonly ExhibitsService _exhibitsService;
        private readonly PageService _pageService;
        public List<Employee> ItemSourse { get; set; } = new();
        public Employee SelectedEmployee
        {
            get { return GetValue<Employee>(); }
            set { SetValue(value, changedCallback: OnEmployeeChanged); }
        }
        private void OnEmployeeChanged()
        {
            Global.CurrentEmployee = SelectedEmployee;
            _pageService.ChangePage(new EditEmployeePage());
        }
        public DelegateCommand EditEmployeeCommand => new(() =>
        {
            _pageService.ChangePage(new EditEmployeePage());
        });
        public MainPageViewModel(EmployeeService employeeService, PageService pageService, ExhibitsService exhibitsService)
        {
            _employeeService = employeeService;
            _pageService = pageService;
            _exhibitsService = exhibitsService;
            ItemSourse = _employeeService.GetEmployee(); 
            ItemExhibitsSourse = _exhibitsService.GetExhibits();
        }
        public List<Exhibits> ItemExhibitsSourse { get; set; } = new();
        public Exhibits SelectedExhibits
        {
            get { return GetValue<Exhibits>(); }
            set { SetValue(value, changedCallback: OnExhibitsChanged); }
        }
        private void OnExhibitsChanged()
        {
            Global.CurrentExhibits = SelectedExhibits;
            _pageService.ChangePage(new EditExhibitsPage());
        }
        public DelegateCommand EditExhibitsCommand => new(() =>
        {
            _pageService.ChangePage(new EditExhibitsPage());
        });
    }
}
