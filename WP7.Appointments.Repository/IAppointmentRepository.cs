using System;
using System.Collections.Generic;
using WP7.Appointments.DataAccess;

namespace WP7.Appointments.Repository
{
    public interface IAppointmentRepository
    {
        IList<Resource> GetResourcesByDate(DateTime date);

        IList<Slot> GetAllAvailableSlots(int resourceId, DateTime date);

        void BookAppointment(DateTime date, int resourceId, int slotId, Customer customer);
    }
}