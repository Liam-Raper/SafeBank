using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeBO> GetAllEmployeesAtABank(int bankId);
        bool EmployeeExist(int bankId, int employeeCode);
        bool EmployeeExist(int employeeId);
        void AddEmployee(int bankId, EmployeeBO employee);
        void DeleteEmployee(int id);
        int GetEmployeeId(int bankId, int employeeCode);
        int GetEmployeeId(string username);
        EmployeeBO GetEmployee(int id);
        void UpdateEmployee(EmployeeBO employee);
        int GetBankId(int id);
    }
}