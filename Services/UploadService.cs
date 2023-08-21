using ExcelDataReader;
using ExcelUpload.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExcelUpload.Services
{
    public class UploadService
    {

        public EmployeeService employeeService;
        public LogService logService;

        public UploadService()
        {
            employeeService = new EmployeeService();
            logService = new LogService();
        }
        public bool UploadExcel(HttpPostedFileBase file)
        {
            var data = ReadFile(file);
            if (data == null)
            {
                return false;
            }

            bool skipHeader=false;
            foreach (DataRow row in data.Rows)
            {
                if (!skipHeader)
                {
                    skipHeader=true;
                    continue;
                }
                var employee = ExtractItem(row.ItemArray);
                if (employee != null)
                {
                    employeeService.InsertEmployee(employee);
                }
            }
            return true;
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return employeeService.GetAllEmployees();
        }

        public DataTable ReadFile(HttpPostedFileBase file)
        {
            Stream stream = file.InputStream;

            IExcelDataReader reader = null;

            if (file.FileName.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (file.FileName.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                return null;
            }

            DataSet excelRecords = reader.AsDataSet();
            reader.Close();

            return excelRecords.Tables?[0];
        }

        public Employee ExtractItem(object[] itemData)
        {
            var x = itemData.ToString();
            var employee = new Employee();
            var logMessage = "";

            //Validating Id
            int id;
            bool validId = int.TryParse(itemData?[0].ToString(), out id);
            if (validId)
            {
                employee.Id = id;
            }
            else
            {
                logMessage += "Id is not valid !!;";
            }

            employee.Name = itemData?[1].ToString();

            //Validating Gender
            Gender gender;
            bool validGender = Gender.TryParse(itemData?[2].ToString(), out gender);
            if (validGender)
            {
                employee.Gender = gender;
            }
            else
            {
                logMessage += "Gender is not valid !!;";
            }

            //Validating DOB
            DateTime DOB;
            bool validDOB = DateTime.TryParse(itemData?[3].ToString(), out DOB);
            if (validDOB)
            {
                employee.DOB = DOB;
            }
            else
            {
                logMessage += "DOB is not valid !!;";
            }

            //Validating DOH
            DateTime DOH;
            bool validDOH = DateTime.TryParse(itemData?[4].ToString(), out DOH);
            if (validDOH)
            {
                employee.DOH = DOH;
            }
            else
            {
                logMessage += "DOH is not valid !!;";
            }

            employee.Department = itemData?[5].ToString();

            //Validating BasicSalary
            double basicSalary;
            bool validBasicSalary = double.TryParse(itemData?[6].ToString(), out basicSalary);
            if (validBasicSalary)
            {
                employee.BasicSalary = basicSalary;
            }
            else
            {
                logMessage += "Basic Salary is not valid !!;";
            }

            //Validating TotalSalary
            double totalSalary;
            bool validTotalSalary = double.TryParse(itemData?[7].ToString(), out totalSalary);
            if (validTotalSalary)
            {
                employee.TotalSalary = totalSalary;
            }
            else
            {
                logMessage += "Total Salary is not valid !!;";
            }

            //Validating MFIG
            int MFIG;
            bool validMFIG = int.TryParse(itemData?[8].ToString(), out MFIG);
            if (validMFIG)
            {
                if (MFIG == 0)
                {
                    employee.MFIG = false;
                }
                else if (MFIG == 1)
                {
                    employee.MFIG = true;
                }
                else
                {
                    logMessage += "MFIG is not valid !!;";
                }
            }
            else
            {
                logMessage += "MFIG is not valid !!;";
            }

            if (logMessage != "")
            {
                var log = new Log
                {
                    EmployeeData = employee.ToString(),
                    Operation = "Insert",
                    Message = logMessage,
                    dateTime = DateTime.Now,
                };
                logService.Log(log);
                return null;
            }
            return employee;
        }
    }
}