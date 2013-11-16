using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ContactInformation> ContactInformation { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
