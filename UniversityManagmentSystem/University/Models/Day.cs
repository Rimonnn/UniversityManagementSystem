using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Day
    {
        public int DayId { set; get; }

        [DisplayName("Day")]
        [Required(ErrorMessage = "Day Field is required")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Name { set; get; }

        public virtual List<RoomAllocation> RoomAllocationList { get; set; }
    }
}