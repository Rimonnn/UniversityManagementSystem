using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace University.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [DisplayName("Code")]
        [Required(ErrorMessage = "Course Code Field is required")]
        [Index(IsUnique = true)]
        [Remote("DoesCourseCodeExist", "Course", HttpMethod = "POST", ErrorMessage = "Course Code already exist")]
        [MaxLength(50)]
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long", MinimumLength = 5)]
        public string CourseCode { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Course Name Field is required")]
        [Index(IsUnique = true)]
        [Remote("DoesCourseNameExist", "Course", HttpMethod = "POST", ErrorMessage = "Course Name already exist")]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [DisplayName("Credit")]
        [Required(ErrorMessage = "Course Credit Field is required")]
        [Range(.5, 5, ErrorMessage = "Course Credit must be lies between 0.5  to 5.0")]
        public double CourseCredit { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        public string CourseDescription { get; set; }
        
        [Required(ErrorMessage = "Department can't empty")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }

        public virtual String AssignTo { get; set; }

        public virtual List<AssignCourse> AssignCourseList { get; set; }
        public virtual List<EnrollCourse> EnrollCourseList { get; set; }
        public virtual List<ResultEntry> ResultEntryList { get; set; }
        public virtual List<RoomAllocation> RoomAllocationList { get; set; }
    }
}