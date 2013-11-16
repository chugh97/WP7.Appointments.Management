using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class AppointmentsContextInitializer : DropCreateDatabaseAlways<AppointmentsContext>
    {
        public AppointmentsContextInitializer()
        {
           
        }

        protected override void Seed(AppointmentsContext context)
        {
            var defaultSlotType = new SlotType() {SlotTypeName = "DefaultSlot"};
            var fullDaySlot = new SlotType() { SlotTypeName = "FullDaySlot" };
            var halfDaySlot = new SlotType() { SlotTypeName = "HalfDaySlot" };
            
            context.SlotTypes.Add(defaultSlotType);
            context.SlotTypes.Add(fullDaySlot);
            context.SlotTypes.Add(halfDaySlot);

            var doctorResourceType = new ResourceType() { Description = "Doctor" };
            var nurseResourceType = new ResourceType() { Description = "Nurse" };

            context.ResourceTypes.Add(doctorResourceType);
            context.ResourceTypes.Add(nurseResourceType);

            var slot1 = new Slot()
                            {
                                SlotType = defaultSlotType,
                                StartTime = new TimeSpan(8,0,0),
                                EndTime = new TimeSpan(8, 30, 0),
                            };

            var slot2 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(8, 30, 0),
                EndTime = new TimeSpan(9, 0, 0),
            };

            var slot3 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(9, 30, 0),
            };

            var slot4 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(9, 30, 0),
                EndTime = new TimeSpan(10, 0, 0),
            };

            var slot5 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
            };

            var slot6 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(10, 30, 0),
                EndTime = new TimeSpan(11, 0, 0),
            };

            var slot7 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(11, 0, 0),
                EndTime = new TimeSpan(11, 30, 0),
            };

            var slot8 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(11, 30, 0),
                EndTime = new TimeSpan(12, 0, 0),
            };

            var slot9 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(12, 0, 0),
                EndTime = new TimeSpan(12, 30, 0),
            };

            var slot10 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(12, 30, 0),
                EndTime = new TimeSpan(13, 0, 0),
            };

            var slot11 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(13, 30, 0),
            };

            var slot12 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(13, 30, 0),
                EndTime = new TimeSpan(14, 0, 0),
            };

            var slot13 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(14, 30, 0),
            };

            var slot14 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(14, 30, 0),
                EndTime = new TimeSpan(15, 00, 0),
            };

            var slot15 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(15, 00, 0),
                EndTime = new TimeSpan(15, 30, 0),
            };

            var slot16 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(15, 30, 0),
                EndTime = new TimeSpan(16, 00, 0),
            };

            var slot17 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(16, 0, 0),
                EndTime = new TimeSpan(16, 30, 0),
            };

            var slot18 = new Slot()
            {
                SlotType = defaultSlotType,
                StartTime = new TimeSpan(16, 30, 0),
                EndTime = new TimeSpan(17, 00, 0),
            };

            var slothalfDaySlot = new Slot()
            {
                SlotType = halfDaySlot,
                StartTime = new TimeSpan(08, 00, 0),
                EndTime = new TimeSpan(13, 00, 0),
            };

            var slotfullDaySlot = new Slot()
                                      {
                                          SlotType = fullDaySlot,
                                          StartTime = new TimeSpan(08, 00, 0),
                                          EndTime = new TimeSpan(17, 00, 0),
                                      };

            context.Slots.Add(slot1);
            context.Slots.Add(slot2);
            context.Slots.Add(slot3);
            context.Slots.Add(slot4);
            context.Slots.Add(slot5);
            context.Slots.Add(slot6);
            context.Slots.Add(slot7);
            context.Slots.Add(slot8);
            context.Slots.Add(slot9);
            context.Slots.Add(slot10);
            context.Slots.Add(slot11);
            context.Slots.Add(slot12);
            context.Slots.Add(slot13);
            context.Slots.Add(slot14);
            context.Slots.Add(slot15);
            context.Slots.Add(slot16);
            context.Slots.Add(slot17);
            context.Slots.Add(slot18);
            context.Slots.Add(slothalfDaySlot);
            context.Slots.Add(slotfullDaySlot);

            ICollection<ResourceNonAvailabilityDays> jNonWorkingDays = new Collection<ResourceNonAvailabilityDays>();
            jNonWorkingDays.Add(new ResourceNonAvailabilityDays() { DayOfWeek = 4 , IsDeleted = false });

            ICollection<ResourceNonAvailability> jBusySlots = new Collection<ResourceNonAvailability>();
            jBusySlots.Add(new ResourceNonAvailability() { NonAvailabilityDate = DateTime.Today.AddDays(30), Slot = slot1});
            jBusySlots.Add(new ResourceNonAvailability() { NonAvailabilityDate = DateTime.Today.AddDays(30), Slot = slot2 });
            
            var nurseJudith = new Resource()
                                  {
                                      ResourceType = nurseResourceType,
                                      Description = "Judith",
                                      ResourceNonAvailabilityDays = jNonWorkingDays,
                                      ResourceNonAvailabilities = jBusySlots
                                  };
            context.Resources.Add(nurseJudith);

            ICollection<ResourceNonAvailability> dBusySlots= new Collection<ResourceNonAvailability>();
            dBusySlots.Add(new ResourceNonAvailability() { NonAvailabilityDate = DateTime.Today.AddDays(30), Slot = slothalfDaySlot });
            
            var doctorTaspfield = new Resource()
            {
                ResourceType = doctorResourceType,
                Description = "Dr. Wills Tapsfield",
                ResourceNonAvailabilityDays = new Collection<ResourceNonAvailabilityDays>(),
                ResourceNonAvailabilities = dBusySlots
            };
            context.Resources.Add(doctorTaspfield);

            ICollection<ContactInformation> customer1ContactInformation = new Collection<ContactInformation>();
            customer1ContactInformation.Add(new ContactInformation() { ContactAttributeKey = "Phone", ContactAttributeValue = "01912586160" });
            customer1ContactInformation.Add(new ContactInformation() { ContactAttributeKey = "Email", ContactAttributeValue = "chugh@yahoo.com" });

            var customer1 = new Customer() {Name = "Shaleen Chugh", ContactInformation = customer1ContactInformation};
            context.Customers.Add(customer1);

            ICollection<ContactInformation> customer2ContactInformation = new Collection<ContactInformation>();
            customer2ContactInformation.Add(new ContactInformation() { ContactAttributeKey = "Phone", ContactAttributeValue = "01912586162" });
            customer2ContactInformation.Add(new ContactInformation() { ContactAttributeKey = "Email", ContactAttributeValue = "t.c@yahoo.com" });

            var customer2 = new Customer() { Name = "Tanay Chugh", ContactInformation = customer2ContactInformation };
            context.Customers.Add(customer2);

            var appointment1 = new Appointment()
                                  {Customer = customer1, Date = DateTime.Today, Slot = slot4, Resource = nurseJudith};

            context.Appointments.Add(appointment1);

            var appointment2 = new Appointment() { Customer = customer2, Date = DateTime.Today, Slot = slot4, Resource = doctorTaspfield };

            context.Appointments.Add(appointment2);

            base.Seed(context);
        }
    }

}
