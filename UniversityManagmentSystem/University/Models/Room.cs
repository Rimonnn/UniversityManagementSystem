using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Room
    {
        public int RoomId { set; get; }

        [DisplayName("Room No.")]
        [Required(ErrorMessage = "Room No. Field is required")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Name { set; get; }

        public virtual List<RoomAllocation> RoomAllocationList { get; set; }
    }
}