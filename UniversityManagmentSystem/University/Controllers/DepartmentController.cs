using System.Linq;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class DepartmentController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "DepartmentId,DepartmentCode,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();

                ViewBag.Msg = "Department Saved Succesfully!";
            }

            return View(department);
        }

        public ActionResult Show()
        {
            return View(db.Departments.ToList());
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult DoesDepartmentCodeExist(string DepartmentCode)
        {
            return Json((!db.Departments.Any(x => x.DepartmentCode == DepartmentCode)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DoesDepartmentNameExist(string DepartmentName)
        {
            return Json((!db.Departments.Any(x => x.DepartmentName == DepartmentName)), JsonRequestBehavior.AllowGet);
        }
    }
}
