using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WP7.Appointments.Management.ViewModels;
using WP7.Appointments.Service.Proxy.AppointmentServiceClient;
using proxy = WP7.Appointments.Service.Proxy;

namespace WP7.Appointments.Management
{
    public partial class Resource : PhoneApplicationPage
    {
        bool _isNewPageInstance = false;

        public Resource()
        {
            InitializeComponent();
            _isNewPageInstance = true;

            (Application.Current as App).ApplicationDataObjectChanged += new EventHandler(Resource_ApplicationDataObjectChanged);
            
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel.LastPageLoaded = "/Resource";
            }
        }

        void Resource_ApplicationDataObjectChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() => UpdateApplicationDataUI());
        }

        private void UpdateApplicationDataUI()
        {
            // Set the ApplicationData and ApplicationDataStatus members of the ViewModel
            // class to update the UI.
            this.DataContext = (Application.Current as App).CompleteApplicationDataViewModel.ResourceViewModel;
            listbox.SelectedIndex = 0;
            int selectedIndex = (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceIndex;
            listbox.SelectedIndex = selectedIndex;
            //statusTextBlock.Text = (Application.Current as App).ApplicationDataStatus;
        }

        public DateTime SelectedDate { get; set; }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_isNewPageInstance)
            {
                if ((Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceId > 0)
                {
                    UpdateApplicationDataUI();
                }
                else
                {
                    var date = NavigationContext.QueryString["date"];
                    SelectedDate = Convert.ToDateTime(date);
                    AppointmentServiceClient client = null;
                    try
                    {
                        client = new proxy.AppointmentServiceClient.AppointmentServiceClient();
                        client.GetResourcesByDateCompleted +=
                            new EventHandler<GetResourcesByDateCompletedEventArgs>(client_GetResourcesByDateCompleted);
                        client.GetResourcesByDateAsync(Convert.ToDateTime(Convert.ToDateTime(date).ToShortDateString()));
                    }
                    finally
                    {
                        if (client != null) client.CloseAsync();
                    }
                }
            }
            else
            {
                if ((Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceId > 0)
                {
                    UpdateApplicationDataUI();
                }
            }

            _isNewPageInstance = false;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                if (null != listbox.SelectedItem)
                {
                    (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceId = ((proxy.AppointmentServiceClient.Resource)listbox.SelectedItem).Id;
                    (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceIndex = listbox.SelectedIndex;
                }
                (Application.Current as App).CompleteApplicationDataViewModel.ResourceViewModel = (ResourceViewModel)this.DataContext;
            }
        }

        void client_GetResourcesByDateCompleted(object sender, GetResourcesByDateCompletedEventArgs e)
        {
            ResourceViewModel rvm = new ResourceViewModel();
            rvm.ResourceItems = e.Result;
            this.DataContext = rvm;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotId = 0;
            }
        }

        private void hypLinkSlots_Click(object sender, RoutedEventArgs e)
        {
            proxy.AppointmentServiceClient.Resource selectedResource = null;
            if (null != listbox.SelectedItem)
            {
                selectedResource = listbox.SelectedItem as proxy.AppointmentServiceClient.Resource;

                if ((Application.Current as App).CompleteApplicationDataViewModel != null)
                {
                    (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceId = selectedResource.Id;
                    (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceIndex = listbox.SelectedIndex;
                    (Application.Current as App).CompleteApplicationDataViewModel.SelectedDate = SelectedDate;
                }

                NavigationService.Navigate(
                    new Uri("/Slots?date=" + SelectedDate.ToShortDateString() + "&resourceId=" + selectedResource.Id, UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Please select a resource to proceed");
            }
        }
    }
}