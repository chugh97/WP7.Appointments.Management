using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WP7.Appointments.BL;
using WP7.Appointments.DataContracts;
using WP7.Appointments.DataAccess;
using AutoMapper;

namespace WP7.Appointments.Service
{
    public class AppointmentService : IAppointmentService
    {
        private IAppointmentBusinessService _appointmentBusinessService = null;

        public AppointmentService()
        {
            _appointmentBusinessService = new AppointmentBusinessService();     
        }

        public IList<DataContracts.Resource> GetResourcesByDate(DateTime date)
        {
            var modelResources = _appointmentBusinessService.GetResourcesByDate(DateTime.Parse(date.ToShortDateString()));
            Mapper.CreateMap<DataAccess.Resource, DataContracts.Resource>()
                .ForMember(x => x.Id, y => y.MapFrom(m => m.Id))
                .ForMember(x => x.Description, y => y.MapFrom(m => m.Description));
            Mapper.AssertConfigurationIsValid();

            return Mapper.Map<IList<DataAccess.Resource>, IList<DataContracts.Resource>>(modelResources);
            //return null;

        }

        public IList<DataContracts.Slot> GetAllAvailableSlots(int resourceId, DateTime date)
        {
            var modelSlots = _appointmentBusinessService.GetAllAvailableSlots(resourceId, DateTime.Parse(date.ToShortDateString()));
            Mapper.CreateMap<DataAccess.Slot, DataContracts.Slot>()
                .ForMember(x => x.Id, y => y.MapFrom(m => m.Id))
                .ForMember(x => x.StartTime, y => y.MapFrom(m => m.StartTime))
                .ForMember(x => x.EndTime, y => y.MapFrom(m => m.EndTime));
            Mapper.AssertConfigurationIsValid();

            return Mapper.Map<IList<DataAccess.Slot>, IList<DataContracts.Slot>>(modelSlots);

        }

        public void BookAppointment(DateTime date, int resourceId, int slotId, DataContracts.Customer customer)
        {
            Mapper.CreateMap<DataContracts.ContactInformation, DataAccess.ContactInformation>()
                 .ForMember(x => x.ContactAttributeKey, y => y.MapFrom(m => m.ContactAttributeKey))
                 .ForMember(x => x.ContactAttributeValue, y => y.MapFrom(m => m.ContactAttributeValue))
                 .ForMember(x => x.Customer, y => y.Ignore())
                 .ForMember(x => x.CustomerId, y => y.Ignore())
                 .ForMember(x => x.Id, y => y.Ignore());

            Mapper.CreateMap<DataContracts.Customer, DataAccess.Customer>()
                 .ForMember(x => x.Id, y => y.MapFrom(m => m.Id))
                 .ForMember(x => x.Name, y => y.MapFrom(m => m.Name))
                 .ForMember(x => x.ContactInformation, y => y.MapFrom(m => m.ContactInformation))
                 .ForMember(x => x.Appointments, y => y.Ignore());

            Mapper.AssertConfigurationIsValid();

            var dtoCustomer = Mapper.Map<DataContracts.Customer, DataAccess.Customer>(customer);

            _appointmentBusinessService.BookAppointment(date, resourceId, slotId, dtoCustomer);
        }
    }
}