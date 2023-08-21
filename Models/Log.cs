using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExcelUpload.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string Operation { get; set; }
        public string Message { get; set; }
        public DateTime dateTime { get; set; }
        public string EmployeeData { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}