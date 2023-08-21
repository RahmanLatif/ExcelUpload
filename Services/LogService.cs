using ExcelUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelUpload.Services
{
    public class LogService
    {
        private ApplicationContext context { get; set; }
        public LogService()
        {
            context = new ApplicationContext();
        }
        public void Log(Log log)
        {
            context.Logs.Add(log);
            context.SaveChanges();
        }
    }
}