using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class TeacherController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();
        
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Save()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");

            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "TeacherId,TeacherName,TeacherAddress,TeacherEmail,TeacherContactNo,DesignationId,DepartmentId,CreditToBeTaken")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();

                ViewBag.Msg = "Teacher Saved Succesfully!";
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);

            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);

            return View(teacher);
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
        public JsonResult DoesTeacherEmailExist(string TeacherEmail)
        {
            return Json((!db.Teachers.Any(x => x.TeacherEmail == TeacherEmail)), JsonRequestBehavior.AllowGet);
        }
    }
}
