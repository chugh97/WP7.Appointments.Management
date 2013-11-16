using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class Slot
    {
        [Key]
        public int Id  { get; set; }

        public int SlotTypeId { get; set; }

        [ForeignKey("SlotTypeId")]
        public virtual SlotType SlotType { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
