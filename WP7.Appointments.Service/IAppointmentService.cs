using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WP7.Appointments.DataContracts;

namespace WP7.Appointments.Service
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IAppointmentService
    {
        [OperationContract]
        IList<Resource> GetResourcesByDate(DateTime date);

        [OperationContract]
        IList<Slot> GetAllAvailableSlots(int resourceId, DateTime date);

        [OperationContract]
        void BookAppointment(DateTime date, int resourceId, int slotId, Customer customer);
    }
}
