using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class EnrollCourseController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EnrollCourseId,StudentId,CourseId,Date")] EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                var result = db.EnrollCourses.Count(u => u.StudentId == enrollcourse.StudentId && u.CourseId == enrollcourse.CourseId) == 0;

                if (result)
                {
                    TempData["success"] = "Course Enrolled";

                    db.EnrollCourses.Add(enrollcourse);
                    db.SaveChanges();

                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["Already"] = "Student Has Already Enrolled This Course";

                    return RedirectToAction("Create");
                }
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollcourse.CourseId);

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId", enrollcourse.StudentId);

            return View(enrollcourse);
        }

        public PartialViewResult CourseLoad(int? studentId)
        {
            List<Course> courseList = new List<Course>();

            if (studentId != null)
            {
                Student aStudent = db.Students.Find(studentId);

                courseList = db.Courses.Where(e => e.DepartmentId == aStudent.DepartmentId).ToList();

                ViewBag.CourseId = new SelectList(courseList, "CourseId", "CourseName");
            }

            return PartialView("~/Views/shared/_FilteredCourse.cshtml");
        }

        public PartialViewResult StudentInfoLoad(int? studentId)
        {
            if (studentId != null)
            {
                Student aStudent = db.Students.FirstOrDefault(s => s.StudentId == studentId);
                ViewBag.Name = aStudent.StudentName;
                ViewBag.Email = aStudent.StudentEmail;
                ViewBag.Dept = aStudent.Department.DepartmentName;

                return PartialView("~/Views/Shared/_StudentInformation.cshtml");
            }
            else
            {
                return PartialView("~/Views/Shared/_StudentInformation.cshtml");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
