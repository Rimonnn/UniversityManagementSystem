using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class ResultEntryController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");

            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "Name");

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ResultEntryId,StudentId,CourseId,GradeId")] ResultEntry resultentry)
        {
            if (ModelState.IsValid)
            {
                var result = db.ResultEntries.Count(r => r.StudentId == resultentry.StudentId && r.CourseId == resultentry.CourseId) == 0;

                if (result)
                {
                    Grade aGrade = db.Grades.Where(g => g.GradeId == resultentry.GradeId).FirstOrDefault();

                    EnrollCourse resultEnrollCourse = db.EnrollCourses.FirstOrDefault(r => r.CourseId == resultentry.CourseId && r.StudentId == resultentry.StudentId);

                    if (resultEnrollCourse != null) 
                        resultEnrollCourse.GradeName = aGrade.Name;

                    db.ResultEntries.Add(resultentry);
                    db.SaveChanges();

                    TempData["success"] = "Result Successfully Entered";

                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["Already"] = "Result Of This Course has Already Been Assigned";

                    return RedirectToAction("Create");
                }
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", resultentry.CourseId);

            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "Name", resultentry.GradeId);

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId", resultentry.StudentId);

            return View(resultentry);
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

        public PartialViewResult CourseLoad(int? studentId)
        {
            List<EnrollCourse> enrollmentList = new List<EnrollCourse>();

            List<Course> courseList = new List<Course>();

            if (studentId != null)
            {
                enrollmentList = db.EnrollCourses.Where(e => e.StudentId == studentId).ToList();

                foreach (EnrollCourse anEnrollment in enrollmentList)
                {
                    Course aCourse = db.Courses.FirstOrDefault(c => c.CourseId == anEnrollment.CourseId);
                    courseList.Add(aCourse);
                }

                ViewBag.CourseId = new SelectList(courseList, "CourseId", "CourseName");
            }

            return PartialView("~/Views/shared/_FilteredCourse.cshtml");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ViewResult()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationId");

            return View();
        }

        //public ActionResult ResultPDF(int? studentId)
        //{
        //    List<EnrollCourse> enrollCourseList = new List<EnrollCourse>();

        //    if (studentId != null)
        //    {
        //        Student aStudent = db.Students.FirstOrDefault(s => s.StudentId == studentId);
        //        ViewBag.Name = aStudent.StudentName;
        //        ViewBag.Dept = aStudent.Department.DepartmentName;
        //        ViewBag.Email = aStudent.StudentEmail;
        //        ViewBag.RegNo = aStudent.RegistrationId;

        //        enrollCourseList = db.EnrollCourses.Where(r => r.StudentId == studentId).ToList();

        //        //if (enrollCourseList.Count == 0)
        //        //{
        //        //    Student bStudent = db.Students.Find(studentId);

        //        //    ViewBag.NotEnrolled = bStudent.RegistrationId + "  : has not taken any course";
        //        //}
        //    }

        //    return View(enrollCourseList);
        //}

        //public ActionResult GeneratePDF()
        //{
        //    return new Rotativa.ActionAsPdf("ResultPDF");
        //}

        //public ActionResult GeneratePDF()
        //{
        //    return new Rotativa.ViewAsPdf("ViewResult");
        //}

        public PartialViewResult ResultPDF(int? studentId)
        {
            List<EnrollCourse> enrollCourseList = new List<EnrollCourse>();

            if (studentId != null)
            {
                Student aStudent = db.Students.FirstOrDefault(s => s.StudentId == studentId);
                ViewBag.Name = aStudent.StudentName;
                ViewBag.Dept = aStudent.Department.DepartmentName;
                ViewBag.Email = aStudent.StudentEmail;
                ViewBag.RegNo = aStudent.RegistrationId;

                enrollCourseList = db.EnrollCourses.Where(r => r.StudentId == studentId).ToList();

                //if (enrollCourseList.Count == 0)
                //{
                //    Student bStudent = db.Students.Find(studentId);

                //    ViewBag.NotEnrolled = bStudent.RegistrationId + "  : has not taken any course";
                //}
            }

            return PartialView("~/Views/shared/_ResultPDF.cshtml", enrollCourseList);
        }

        public ActionResult GeneratePDF()
        {
            return new Rotativa.PartialViewAsPdf("_ResultPDF");
        }

        public PartialViewResult ResultInfoLoad(int? studentId)
        {

            List<EnrollCourse> enrollCourseList = new List<EnrollCourse>();

            if (studentId != null)
            {
                enrollCourseList = db.EnrollCourses.Where(r => r.StudentId == studentId).ToList();

                if (enrollCourseList.Count == 0)
                {
                    Student aStudent = db.Students.Find(studentId);

                    ViewBag.NotEnrolled = aStudent.RegistrationId + "  : has not taken any course";
                }
            }

            return PartialView("~/Views/shared/_resultInformation.cshtml", enrollCourseList);
        }
    }
}
