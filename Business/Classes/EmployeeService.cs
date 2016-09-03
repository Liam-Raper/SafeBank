using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.DatabaseModel;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class EmployeeService : IEmployeeService
    {

        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeBO> GetAllEmployeesAtABank(int bankId)
        {
            return
                _unitOfWork.EmployeeTable.GetAll()
                    .Where(x => x.EmployeeLocations.Any(y => y.BankId == bankId))
                    .Select(employeeInfo => new EmployeeBO
                    {
                        Id = employeeInfo.Id,
                        Code = employeeInfo.EmployeeDetail.Code,
                        GivenName = employeeInfo.EmployeeDetail.Given_name,
                        FamilyName = employeeInfo.EmployeeDetail.Family_name,
                        Email = employeeInfo.EmployeeDetail.Email,
                        Phone = employeeInfo.EmployeeDetail.Phone,
                        Username = employeeInfo.User.UserDetail.Username
                    });
        }

        public bool EmployeeExist(int bankId, int employeeCode)
        {
            return
                _unitOfWork.EmployeeTable
                    .GetAll()
                    .Where(x => x.EmployeeDetail.Code == employeeCode)
                    .Any(x => x.EmployeeLocations.Any(y => y.BankId == bankId));
        }

        public bool EmployeeExist(int employeeId)
        {
            return _unitOfWork.EmployeeTable.GetAll().Any(x => x.Id == employeeId);
        }

        public void AddEmployee(int bankId, EmployeeBO employee)
        {
            var employeeData = new Employee
            {
                EmployeeDetail = new EmployeeDetail
                {
                    Code = employee.Code,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    Family_name = employee.FamilyName,
                    Given_name = employee.GivenName
                },
                User = _unitOfWork.User.GetAll().Single(x => x.UserDetail.Username == employee.Username)
            };
            employeeData.EmployeeLocations.Add(new EmployeeLocation
            {
                BankDetail = _unitOfWork.BankTable.GetSingle(bankId)
            });
            _unitOfWork.EmployeeTable.AddSingle(employeeData);
            _unitOfWork.Commit();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeTable.GetSingle(id);
            _unitOfWork.EmployeeLocationTable.DeleteSingle(employee.EmployeeLocations.Select(x => x.Id).Single());
            _unitOfWork.EmployeeTable.DeleteSingle(id);
            _unitOfWork.Commit();
        }

        public int GetEmployeeId(int bankId, int employeeCode)
        {
            return
                _unitOfWork.EmployeeTable
                    .GetAll()
                    .Where(x => x.EmployeeDetail.Code == employeeCode)
                    .Single(x => x.EmployeeLocations.Any(y => y.BankId == bankId)).Id;
        }

        public int GetEmployeeId(string username)
        {
            return _unitOfWork.EmployeeTable.GetAll().Single(x => x.User.UserDetail.Username == username).Id;
        }

        public EmployeeBO GetEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeTable.GetSingle(id);
            return new EmployeeBO
            {
                Id = employee.Id,
                Username = employee.User.UserDetail.Username,
                FamilyName = employee.EmployeeDetail.Family_name,
                GivenName = employee.EmployeeDetail.Given_name,
                Phone = employee.EmployeeDetail.Phone,
                Email = employee.EmployeeDetail.Email,
                Code = employee.EmployeeDetail.Code
            };
        }

        public void UpdateEmployee(EmployeeBO employee)
        {
            var employeeData = _unitOfWork.EmployeeTable.GetSingle(employee.Id);
            employeeData.EmployeeDetail.Family_name = employee.FamilyName;
            employeeData.EmployeeDetail.Given_name = employee.GivenName;
            _unitOfWork.Commit();
        }

        public int GetBankId(int id)
        {
            return _unitOfWork.EmployeeTable.GetSingle(id).EmployeeLocations.Single().BankId;
        }
    }
}