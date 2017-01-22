using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }

        [DisplayName("Designation")]
        [Required(ErrorMessage = "Semester Field is required")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string DesignationName { get; set; }

        public virtual List<Teacher> TeacherList { get; set; }
    }
}