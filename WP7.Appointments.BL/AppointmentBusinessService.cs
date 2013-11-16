using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP7.Appointments.DataAccess;
using WP7.Appointments.Repository;

namespace WP7.Appointments.BL
{
    public class AppointmentBusinessService : IAppointmentBusinessService
    {
        private IAppointmentRepository _appointmentRepository;

        public AppointmentBusinessService()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        public IList<Resource> GetResourcesByDate(DateTime date)
        {
            return _appointmentRepository.GetResourcesByDate(date);
        }

        public IList<Slot> GetAllAvailableSlots(int resourceId, DateTime date)
        {
            return _appointmentRepository.GetAllAvailableSlots(resourceId, date);
        }

        public void BookAppointment(DateTime date, int resourceId, int slotId, Customer customer)
        {
            _appointmentRepository.BookAppointment(date, resourceId, slotId, customer);
        }
    }
}
