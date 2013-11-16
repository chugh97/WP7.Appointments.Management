using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP7.Appointments.DataAccess;

namespace WP7.Appointments.BL
{
    public interface IAppointmentBusinessService
    {
        IList<Resource> GetResourcesByDate(DateTime date);

        IList<Slot> GetAllAvailableSlots(int resourceId, DateTime date);

        void BookAppointment(DateTime date, int resourceId, int slotId, Customer customer);
    }
}
