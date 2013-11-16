using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        public int ResourceTypeId { get; set; }

        [ForeignKey("ResourceTypeId")]
        public virtual ResourceType ResourceType { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ResourceNonAvailability> ResourceNonAvailabilities { get; set; }

        public virtual ICollection<ResourceNonAvailabilityDays> ResourceNonAvailabilityDays { get; set; }

    }
}