using System;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class EnrollCourse
    {
        public int EnrollCourseId { get; set; }

        [Required(ErrorMessage = "Student Reg. No. can't be empty")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required(ErrorMessage = "Course can't be empty")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date can't empty")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual string GradeName { set; get; }
    }
}