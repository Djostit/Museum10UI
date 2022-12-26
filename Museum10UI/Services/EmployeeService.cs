namespace Museum10UI.Services
{
    public class EmployeeService
    {
        private const string PATH = "employee.json";
        private static List<Employee> Employees = new();
        private static async Task ReadEmployes()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            else
            {
                Employees = JsonConvert.DeserializeObject<List<Employee>>(await File.ReadAllTextAsync(PATH));
            }
        }
        private static async Task SaveEmployee()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            await File.WriteAllTextAsync(PATH, JsonConvert.SerializeObject(Employees, Formatting.Indented));
        }
        public async Task AddEmployee(string Code, string FullName)
        {
            await ReadEmployes();

            Employees.Add(new Employee 
            {
                Code = Code,
                FullName = FullName,
            });

            await SaveEmployee();
        }
        public async Task DeleteEmployee()
        {
            await ReadEmployes();

            Employees.RemoveAt(Employees.FindIndex(e => e.Code.Equals(Global.CurrentEmployee.Code)));

            await SaveEmployee();
        }
        public List<Employee> GetEmployee()
        {
            ReadEmployes().GetAwaiter();
            return Employees;
        }
        public async Task UpdateCurrentEmployee(string Code, string FullName)
        {
            if (Global.CurrentEmployee == null
                || Global.CurrentEmployee.Code == null)
                return;
            Employees[Employees.FindIndex(e => e.Code.Equals(Global.CurrentEmployee.Code))] = new Employee
            {
                Code = Code,
                FullName = FullName,
            };
            await SaveEmployee();
        }
    }
}
