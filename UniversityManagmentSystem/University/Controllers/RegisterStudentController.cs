using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class RegisterStudentController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StudentId,StudentName,StudentEmail,StudentContactNo,Date,StudentAddress,RegistrationId,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.RegistrationId = CreateStudentRegistrationNo(student);
                
                db.Students.Add(student);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = student.StudentId });
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);

            return View(student);
        }

        private string CreateStudentRegistrationNo(Student student)
        {
            int id = db.Students.Count(s => (s.DepartmentId == student.DepartmentId) && (s.Date.Year == student.Date.Year)) + 1;

            Department aDepartment = db.Departments.Where(d => d.DepartmentId == student.DepartmentId).FirstOrDefault();

            string registrationId = aDepartment.DepartmentCode + "-" + student.Date.Year + "-";

            string addZero = "";

            int len = 3 - id.ToString().Length;

            for (int i = 0; i < len; i++)
            {
                addZero = "0" + addZero;
            }

            return registrationId + addZero + id;
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
        public JsonResult DoesStudentEmailExist(string StudentEmail)
        {
            return Json((!db.Students.Any(x => x.StudentEmail == StudentEmail)), JsonRequestBehavior.AllowGet);
        }
    }
}
