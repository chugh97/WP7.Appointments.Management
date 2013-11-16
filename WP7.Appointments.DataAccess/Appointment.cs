using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SlotId { get; set; }

        [ForeignKey("SlotId")]
        public virtual Slot Slot { get; set; }

        public int ResourceId { get; set; }

        [ForeignKey("ResourceId")]
        public virtual Resource Resource { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public bool? IsConfirmed { get; set; }
    }
    
}
