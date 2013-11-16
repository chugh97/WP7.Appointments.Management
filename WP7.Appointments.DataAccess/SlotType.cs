using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class SlotType
    {
        [Key]
        public int Id { get; set; }

        public string SlotTypeName { get; set; }
    }
}
