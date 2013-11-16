using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP7.Appointments.DataAccess;
using System.Data.Entity;
using System.Linq;

namespace WP7.Appointments.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private const string DEFAULT_SLOT = "DefaultSlot";
        private AppointmentsContext _context = null;

        public AppointmentRepository()
        {
            _context = new AppointmentsContext();
        }
        
        public IList<DataAccess.Resource> GetResourcesByDate(DateTime date)
        {
            var allResources = _context.Resources.ToList();

            var listOfResourcesToRemove  = ResourcesToRemove(date);

            foreach (var resource in listOfResourcesToRemove)
            {
                allResources.Remove(resource);
            }

            return allResources;
        }
        

        public IList<DataAccess.Slot> GetAllAvailableSlots(int resourceId, DateTime date)
        {
            var slots = new List<Slot>();
            var listofResourcesToRemove = this.ResourcesToRemove(date);

            if (listofResourcesToRemove.Any(x=> x.Id.Equals(resourceId)))
            {
                return slots;
            }

            var defaultSlots = (from c in _context.Slots.Include(y => y.SlotType)
                               where c.SlotType.SlotTypeName.Equals(DEFAULT_SLOT)
                               select c).ToList();

            var listBusySlots = this.BusySlots(resourceId, date);

            var listAfterBusySlotsRemoved = this.RemoveDefaultBusySlots(defaultSlots, listBusySlots);

            var finalSlots = this.RemoveNonStandardSlots(listAfterBusySlotsRemoved, listBusySlots);


            //TODO: REMOVE SLOTS FOR EXISTING APPOINTMENTS
            return finalSlots;
        }

        public void BookAppointment(DateTime date, int resourceId, int slotId, DataAccess.Customer customer)
        {
            var maybeExistingCustomer = (from c in _context.Customers.Include(x => x.ContactInformation)
                                         where c.Name.ToLower().Equals(customer.Name.ToLower())
                                         select c).ToList();

            bool isExistingCustomer = false;
            Customer existingCustomer = null;
            foreach (var cust in maybeExistingCustomer)
            {
                if (
                    (customer.ContactInformation.Where(x => x.ContactAttributeKey.Equals("Phone")).Select(
                        x => x.ContactAttributeValue).FirstOrDefault() ==
                    cust.ContactInformation.Where(x => x.ContactAttributeKey.Equals("Phone")).Select(
                        x => x.ContactAttributeValue).FirstOrDefault())

                    &&
                     (customer.ContactInformation.Where(x => x.ContactAttributeKey.Equals("Email")).Select(
                        x => x.ContactAttributeValue).FirstOrDefault() ==
                    cust.ContactInformation.Where(x => x.ContactAttributeKey.Equals("Email")).Select(
                        x => x.ContactAttributeValue).FirstOrDefault())
                    )
                {
                    existingCustomer = cust;
                    isExistingCustomer = true;
                    break;
                }
            }

            if (isExistingCustomer)
            {
                _context.Appointments.Add(new Appointment() { CustomerId = existingCustomer.Id, Date = date, ResourceId = resourceId, SlotId = slotId });
            }
            else
            {
                _context.Appointments.Add(new Appointment() { Customer = customer, Date = date, ResourceId = resourceId, SlotId = slotId });

            }
            _context.SaveChanges();
        }

        private IList<Slot> RemoveNonStandardSlots(IList<Slot> listAfterBusySlotsRemoved, IList<Slot> listBusySlots)
        {
            var nonDefaultSlots = listBusySlots.Where(x => x.SlotType.SlotTypeName != DEFAULT_SLOT);
            IList<Slot> slotsToRemove = new List<Slot>();
            foreach (var nonDefaultSlot in nonDefaultSlots)
            {
                 foreach (var slot in listAfterBusySlotsRemoved)
                 {
                    if (slot.StartTime >= nonDefaultSlot.StartTime && slot.EndTime <= nonDefaultSlot.EndTime) //slot time between non default slot to add to remove collection
                    {
                        slotsToRemove.Add(slot);
                    }
                 }
            }
            return listAfterBusySlotsRemoved.Except(slotsToRemove, new SlotComparer()).ToList();
        }

        private IList<Slot> RemoveDefaultBusySlots(IList<Slot> defaultSlots, IList<Slot> listBusySlots)
        {
            var slots = new List<Slot>();
            var listOfDefaultBusySlots = listBusySlots.Where(x => x.SlotType.SlotTypeName.Equals(DEFAULT_SLOT));
            return defaultSlots.Except(listOfDefaultBusySlots, new SlotComparer()).ToList();
        }

        private IList<Slot> BusySlots(int resourceId, DateTime date)
        {
            var busyResources = (from c in _context.Resources.Include(x => x.ResourceNonAvailabilities)
                                 where c.Id.Equals(resourceId)
                                 select c.ResourceNonAvailabilities.Where(y=> y.NonAvailabilityDate == date)).ToList();

            var busyFlattenedListBySelectedResource = new List<ResourceNonAvailability>();
            foreach (var busyResource in busyResources)
            {
                busyFlattenedListBySelectedResource.AddRange(busyResource);
            }

            return busyFlattenedListBySelectedResource.Select(resourceNonAvailability => resourceNonAvailability.Slot).ToList();
        }

        private List<Resource> ResourcesToRemove(DateTime date)
        {
            var listOfResourcesToRemove = new List<Resource>();
            var allResources = _context.Resources.ToList();
            foreach (var resource in allResources)
            {
                var nonAvailabilityDays = resource.ResourceNonAvailabilityDays.ToList();
                foreach (var resourceNonAvailabilityDayse in nonAvailabilityDays)
                {
                    if (!resourceNonAvailabilityDayse.StartDate.HasValue && !resourceNonAvailabilityDayse.EndDate.HasValue &&
                        resourceNonAvailabilityDayse.DayOfWeek == (int)date.DayOfWeek)
                    {
                        listOfResourcesToRemove.Add(resource);
                    }
                    else if (resourceNonAvailabilityDayse.DayOfWeek == (int)date.DayOfWeek &&
                             date >= resourceNonAvailabilityDayse.StartDate && date <= resourceNonAvailabilityDayse.EndDate)
                    {
                        listOfResourcesToRemove.Add(resource);
                    }
                }
            }

            return listOfResourcesToRemove;
        }
    }
}
