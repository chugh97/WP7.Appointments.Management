using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class ResourceNonAvailability
    {
        [Key]
        public int Id { get; set; }

        public DateTime NonAvailabilityDate { get; set; }

        public int SlotId { get; set; }

        [ForeignKey("SlotId")]
        public virtual Slot Slot { get; set; }

    }
}
