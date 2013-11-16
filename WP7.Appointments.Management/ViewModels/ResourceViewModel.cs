using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WP7.Appointments.Service.Proxy.AppointmentServiceClient;
using proxy = WP7.Appointments.Service.Proxy;

namespace WP7.Appointments.Management.ViewModels
{
    public class ResourceViewModel
    {
        public ResourceViewModel()
        {
            //AppointmentServiceClient client = null;
            //try
            //{
            //    client = new proxy.AppointmentServiceClient.AppointmentServiceClient();
            //    client.GetResourcesByDateCompleted += new EventHandler<GetResourcesByDateCompletedEventArgs>(client_GetResourcesByDateCompleted);
            //    client.GetResourcesByDateAsync(dateTime);
            //}
            //finally
            //{
            //    if (client != null)
            //        client.CloseAsync();
            //}
        }

        //void client_GetResourcesByDateCompleted(object sender, GetResourcesByDateCompletedEventArgs e)
        //{
        //    ResourceItems = e.Result;
        //}

        public ObservableCollection<proxy.AppointmentServiceClient.Resource> ResourceItems { get; set; }
    }
    
}
