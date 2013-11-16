using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class ContactInformation
    {
        [Key]
        public int Id { get; set; }

        public string ContactAttributeKey { get; set; }

        public string ContactAttributeValue { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
