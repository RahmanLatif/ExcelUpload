using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelUpload.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOH { get; set; }
        public string Department { get; set; }
        public double BasicSalary { get; set; }
        public double TotalSalary { get; set; }
        public bool MFIG { get; set; }
        public virtual ICollection<Log> Logs { get; set; }

        public override string ToString()
        {
            return $"{{'id':{Id},'name':'{Name}','gender':'{Gender}','DOB':'{DOB.ToString()}',"
                + $"'DOH':{DOH.ToString()},'department':'{Department}','basicSalary':'{BasicSalary}',"
                +$"'totalSalary':'{TotalSalary}','MFIG':'{MFIG}'}}";
        }

    }
}