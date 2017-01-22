using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace University.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name Field is required")]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Field is required")]
        [Index(IsUnique = true)]
        [Remote("DoesStudentEmailExist", "RegisterStudent", HttpMethod = "POST", ErrorMessage = "Email already exist")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100)]
        public string StudentEmail { get; set; }

        [DisplayName("Contact No.")]
        [Required(ErrorMessage = "Contact No. Field is required")]
        [MaxLength(50)]
        public string StudentContactNo { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date can't empty")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Address")]
        [DataType(DataType.MultilineText)]
        public string StudentAddress { get; set; }

        [DisplayName("Registration")]
        public virtual String RegistrationId { get; set; }

        [Required(ErrorMessage = "Department can't empty")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual List<EnrollCourse> EnrollCourseList { get; set; }
        public virtual List<ResultEntry> ResultEntryList { get; set; }
    }
}