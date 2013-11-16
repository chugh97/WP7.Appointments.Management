using System;
using System.ComponentModel.DataAnnotations;

namespace WP7.Appointments.DataAccess
{
    public class ResourceNonAvailabilityDays
    {
        [Key]
        public int Id { get; set; }

        public int DayOfWeek { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}