using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace University.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Teacher Name Field is required")]
        [MaxLength(100)]
        public string TeacherName { get; set; }

        [DisplayName("Address")]
        [DataType(DataType.MultilineText)]
        public string TeacherAddress { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Field is required")]
        [Index(IsUnique = true)]
        [Remote("DoesTeacherEmailExist", "Teacher", HttpMethod = "POST", ErrorMessage = "Email already exist")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100)]
        public string TeacherEmail { get; set; }

        [DisplayName("Contact No.")]
        [Required(ErrorMessage = "Contact No. Field is required")]
        [MaxLength(50)]
        public string TeacherContactNo { get; set; }
        
        [Required(ErrorMessage = "Designation can't empty")]
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }

        [Required(ErrorMessage = "Department can't empty")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [DisplayName("Credit to be taken")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Credit must contain a non-negative value")]
        public double CreditToBeTaken { get; set; }
        
        public virtual List<AssignCourse> AssignCourseList { get; set; }
    }
}