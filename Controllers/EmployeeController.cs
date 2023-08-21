using ExcelUpload.Services;
using System.Web;
using System.Web.Mvc;

namespace ExcelUpload.Controllers
{
    public class EmployeeController : Controller
    {
        private UploadService uploadService;
        public EmployeeController()
        {
            uploadService = new UploadService();
        }

        public ActionResult Index()
        {
            var items = uploadService.GetAllEmployees();
            return View(items);
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase file)
        {
            var result = uploadService.UploadExcel(file);
            if (result)
            {
                TempData["Message"] = "File Uploaded Successfully !!";
            }
            else
            {
                TempData["Message"] = "File Uploading Failed !!";
            }
            return RedirectToAction("Index");
        }
    }
}
