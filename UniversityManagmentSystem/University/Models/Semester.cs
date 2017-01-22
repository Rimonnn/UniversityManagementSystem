using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Semester
    {
        public int SemesterId { get; set; }

        [DisplayName("Semester")]
        [Required(ErrorMessage = "Semester Field is required")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string SemesterName { get; set; }

        public virtual List<Course> CourseList { get; set; }
    }
}