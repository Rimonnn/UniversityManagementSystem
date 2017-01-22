using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class RoomAllocation
    {
        public int RoomAllocationId { get; set; }

        [Required(ErrorMessage = "Department can't be empty")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "Course can't be empty")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required(ErrorMessage = "Room can't be empty")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required(ErrorMessage = "Day can't be empty")]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        [Display(Name = "From (Formate HH:MM) (24 Hours)")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [Display(Name = "To (Formate HH:MM) (24 Hours)")]
        public string EndTime { get; set; }
    }
}