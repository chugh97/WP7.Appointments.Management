using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WP7.Appointments.Service.Proxy.AppointmentServiceClient;
using proxy = WP7.Appointments.Service.Proxy;

namespace WP7.Appointments.Management.ViewModels
{
    public class SlotsViewModel
    {
        public SlotsViewModel()
        {
                
        }

        public ObservableCollection<proxy.AppointmentServiceClient.Slot> SlotItems { get; set; }
    }
}
