using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace WP7.Appointments.DataAccess
{
    public class AppointmentsContext : System.Data.Entity.DbContext 
    {
        public AppointmentsContext() : base("AppointmentsContext")
        {
                
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ContactInformation> ContactInformations { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Slot> Slots { get; set; }

        public DbSet<ResourceType> ResourceTypes { get; set; }

        public DbSet<SlotType> SlotTypes { get; set; }

        public DbSet<ResourceNonAvailability> ResourceNonAvailabilities { get; set; }

        public DbSet<ResourceNonAvailabilityDays> ResourceNonAvailabilityDays { get; set; }
    }
}
