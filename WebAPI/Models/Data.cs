using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Data
    {
        private static Data _dataAccess = null;

        private static readonly object syncRoot = new object();


        public static Data dataAccess{

            get
            {
                if (_dataAccess == null)
                {
                    lock (syncRoot)
                    {
                        if (_dataAccess == null)
                            _dataAccess = new Data();
                    }
                }
                return _dataAccess;
            }
            }

        List<Employee> employeeList;
        private Data()
        {
            employeeList = new List<Employee>();
            employeeList.Add(new Employee() { Position = "Manager",  EmployeeId = 1, EmployeeName = "John Scratchy Doe", PhotoFileName = "1.jpg" });
            employeeList.Add(new Employee() { Position = "IT", EmployeeId = 2, EmployeeName = "Mike Kitty Lock", PhotoFileName = "2.jpg" });
            employeeList.Add(new Employee() { Position = "Worker", EmployeeId = 3, EmployeeName = "Greg Mew Stone", PhotoFileName = "3.jpg" });
            employeeList.Add(new Employee() { Position = "Director", EmployeeId = 4, EmployeeName = "Bread Squeak Milk", PhotoFileName = "4.jpg" });
            employeeList.Add(new Employee() { Position = "Cleaner", EmployeeId = 5, EmployeeName = "Geremy Pass Riot", PhotoFileName = "5.jpg" });
            employeeList.Add(new Employee() { Position = "Coder", EmployeeId = 6, EmployeeName = "Silver Play Spoon", PhotoFileName = "6.jpg" });
         
        }

        public void AddEmployee(Employee emp)
        {
            if (emp.EmployeeId == 0)
                emp.EmployeeId = employeeList.Last().EmployeeId + 1;
            employeeList.Add(emp);
        }


        public List<Employee> GetAllEmployees()
        {
            return employeeList;
        }




    }
}
