using Museum10UI.ViewModels;

namespace Museum10UI
{
    internal class ViewModelLocator
    {
        private static ServiceProvider _provider;
        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<EditEmployeeViewModel>();
            services.AddTransient<EditExhibitsViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<EmployeeService>();
            services.AddSingleton<ExhibitsService>();


            _provider = services.BuildServiceProvider();
            foreach(var service in services) 
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }
        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public MainPageViewModel MainPageViewModel => _provider.GetRequiredService<MainPageViewModel>();
        public EditEmployeeViewModel EditEmployeeViewModel => _provider.GetRequiredService<EditEmployeeViewModel>();
        public EditExhibitsViewModel EditExhibitsViewModel => _provider.GetRequiredService<EditExhibitsViewModel>();
    }
}
