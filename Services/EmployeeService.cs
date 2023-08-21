using ExcelUpload.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ExcelUpload.Services
{
    public class EmployeeService
    {
        private ApplicationContext context { get; set; }
        public LogService logService;
        public EmployeeService()
        {
            this.context = new ApplicationContext();
            logService = new LogService();
        }
        public int InsertEmployee(Employee employee)
        {
            var employeeEntity = context.Employees.FirstOrDefault(em => em.Id == employee.Id);
            if (employeeEntity == null)
            {
                try
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    var log = new Log
                    {
                        EmployeeId = employee.Id,
                        EmployeeData = employee.ToString(),
                        Operation = "Insert",
                        Message = "Employee Inserted Successfully",
                        dateTime = DateTime.Now,
                    };
                    logService.Log(log);
                }
                catch(Exception ex)
                {
                    var log = new Log
                    {
                        EmployeeData = employee.ToString(),
                        Operation = "Insert",
                        Message = ex.Message,
                        dateTime = DateTime.Now,
                    };
                   logService.Log(log);
                    return 0;
                }
            }
            else
            {
                try
                {
                    context.Entry(employeeEntity).State = EntityState.Detached;
                    context.Entry(employee).State = EntityState.Modified;
                    context.SaveChanges();
                    var log = new Log
                    {
                        EmployeeId = employee.Id,
                        EmployeeData = employee.ToString(),
                        Operation = "Update",
                        Message = "Employee Updated Successfully",
                        dateTime = DateTime.Now,
                    };
                    logService.Log(log);
                }
                catch (Exception ex)
                {
                    var log = new Log
                    {
                        EmployeeId = employee.Id,
                        EmployeeData = employee.ToString(),
                        Operation = "Update",
                        Message = ex.Message,
                        dateTime = DateTime.Now,
                    };
                    logService.Log(log);
                    return 0;
                }
            }
            return employee.Id;
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }
    }
}