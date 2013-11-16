using System;
using System.Collections.Generic;
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
    public partial class Slots : PhoneApplicationPage
    {
        bool _isNewPageInstance = false;

        public Slots()
        {
            InitializeComponent();

            _isNewPageInstance = true;

            (Application.Current as App).ApplicationDataObjectChanged += new EventHandler(Slot_ApplicationDataObjectChanged);

            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel.LastPageLoaded = "/Slots";
            }
        }

        void Slot_ApplicationDataObjectChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() => UpdateApplicationDataUI());
        }

        private void UpdateApplicationDataUI()
        {
            // Set the ApplicationData and ApplicationDataStatus members of the ViewModel
            // class to update the UI.
            this.LayoutRoot.DataContext = (Application.Current as App).CompleteApplicationDataViewModel.SlotViewModel;
            //listbox.SelectedIndex = -1;
            //int selectedIndex = (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotIndex;
            //listbox.SelectedIndex = selectedIndex;
            //statusTextBlock.Text = (Application.Current as App).ApplicationDataStatus;
        }

        public DateTime SelectedDate { get; set; }

        public int ResourceId { get; set; }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (_isNewPageInstance)
            {
                if ((Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotId > 0)
                {
                    UpdateApplicationDataUI();
                }
                else
                {
                    var date = NavigationContext.QueryString["date"];
                    SelectedDate = Convert.ToDateTime(date);
                    ResourceId = Convert.ToInt32(NavigationContext.QueryString["resourceId"]);
                    AppointmentServiceClient client = null;

                    try
                    {
                        client = new proxy.AppointmentServiceClient.AppointmentServiceClient();
                        client.GetAllAvailableSlotsCompleted += new EventHandler<GetAllAvailableSlotsCompletedEventArgs>(client_GetAllAvailableSlotsCompleted);
                        client.GetAllAvailableSlotsAsync(ResourceId, Convert.ToDateTime(date));
                    }
                    finally
                    {
                        if (client != null) client.CloseAsync();
                    }
                }
            }
            else
            {
                if ((Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotId > 0)
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
                //if (null != listbox.SelectedItem)
                //{
                //    (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotId = ((proxy.AppointmentServiceClient.Slot)listbox.SelectedItem).Id;
                //    (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotIndex = listbox.SelectedIndex;
                //}

                (Application.Current as App).CompleteApplicationDataViewModel.SlotViewModel = (SlotsViewModel)this.DataContext;
            }
        }

        void client_GetAllAvailableSlotsCompleted(object sender, GetAllAvailableSlotsCompletedEventArgs e)
        {
            SlotsViewModel svm = new SlotsViewModel();
            svm.SlotItems = e.Result;
            this.DataContext = svm;
        }

        private void hypBookAppointment_Click(object sender, RoutedEventArgs e)
        {
            SaveResourceId();
            SaveSelectedDate();
            
            proxy.AppointmentServiceClient.Slot selectedSlot = null;
           
            //if (null != listbox.SelectedItem)
            //{
                var hyperlinkButton = sender as HyperlinkButton;
                var selectedSlotId = ((Slot)hyperlinkButton.DataContext).Id;
                this.SaveSelectedSlotId(selectedSlotId);

                NavigationService.Navigate(new Uri("/CustomerDetails?date=" + SelectedDate.ToShortDateString() + "&resourceId=" +
                                           ResourceId.ToString() + "&slotId=" + selectedSlotId, UriKind.Relative));
            //}
            //else
            //{
            //    MessageBox.Show("please select an appointment");
            //}

            
            //proxy.AppointmentServiceClient.AppointmentServiceClient client = null;
            //try
            //{
            //    client = new proxy.AppointmentServiceClient.AppointmentServiceClient();
            //    client.BookAppointmentCompleted +=new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_BookAppointmentCompleted);
            //    client.BookAppointmentAsync(selectedDate.Value, resourceId, );
            //}
            //finally
            //{
            //    if (client != null) client.CloseAsync();
            //}
        }

        //void client_BookAppointmentCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
        private void SaveSelectedSlotId(int slotId)
        {
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotId = slotId;
            }
        }

        private void SaveSelectedDate()
        {
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel.SelectedDate = Convert.ToDateTime(NavigationContext.QueryString["date"]);
            }
        }

        private void SaveResourceId()
        {
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceId = Convert.ToInt32(NavigationContext.QueryString["resourceId"]);
            }
        }

        //private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (null != listbox.SelectedItem)
        //    {
        //        var selectedSlot = listbox.SelectedItem as proxy.AppointmentServiceClient.Slot;

        //        if ((Application.Current as App).CompleteApplicationDataViewModel != null)
        //        {
        //            (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotId = selectedSlot.Id;
        //            (Application.Current as App).CompleteApplicationDataViewModel.SelectedSlotIndex =
        //                listbox.SelectedIndex;
        //        }

        //    }
        //}

       
    }
}