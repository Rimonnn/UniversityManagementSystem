using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace University.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [DisplayName("Code")]
        [Required(ErrorMessage = "Code Field is required")]
        [Index(IsUnique = true)]
        [Remote("DoesDepartmentCodeExist", "Department", HttpMethod = "POST", ErrorMessage = "Code already exist")]
        [MaxLength(50)]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} characters long and less then 8 characters.", MinimumLength = 2)]
        public string DepartmentCode { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name Field is required")]
        [Index(IsUnique = true)]
        [Remote("DoesDepartmentNameExist", "Department", HttpMethod = "POST", ErrorMessage = "Name already exist")]
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        public virtual List<Course> CourseList { get; set; }
        public virtual List<Teacher> TeacherList { get; set; }
        public virtual List<AssignCourse> AssignCourseList { get; set; }
        public virtual List<Student> StudentList { get; set; }
        public virtual List<RoomAllocation> RoomAllocationList { get; set; }
    }
}