using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using WP7.Appointments.Management.ViewModels;
using Telerik.Windows.Controls.Calendar;

namespace WP7.Appointments.Management
{
    public partial class Home : PhoneApplicationPage
    {
        bool _isNewPageInstance = false;
        // Constructor
        public Home()
        {
            InitializeComponent();

            _isNewPageInstance = true;

            if ((Application.Current as App).CompleteApplicationDataViewModel == null)
            {
                (Application.Current as App).CompleteApplicationDataViewModel = new CompleteApplicationDataViewModel();
            }

            // Set the event handler for when the application data object changes.
            (Application.Current as App).ApplicationDataObjectChanged += new EventHandler(Home_ApplicationDataObjectChanged);

            this.appointmentCalendar.CalendarWeekRule = CalendarWeekRule.FirstDay;
            this.appointmentCalendar.SelectableDateStart = DateTime.Today;
            this.appointmentCalendar.SelectableDateEnd = DateTime.Today.AddMonths(12);
            this.appointmentCalendar.SelectedValueChanging += new EventHandler<ValueChangingEventArgs<object>>(appointmentCalendar_SelectedValueChanging);
        }

        void Home_ApplicationDataObjectChanged(object sender, EventArgs e)
        {
            // Call UpdateApplicationData on the UI thread.
            Dispatcher.BeginInvoke(() => UpdateApplicationDataUI());
        }

        private void UpdateApplicationDataUI()
        {
            // Set the ApplicationData and ApplicationDataStatus members of the ViewModel
            // class to update the UI.
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                //appointmentCalendar.SelectedValue = (Application.Current as App).CompleteApplicationDataViewModel.SelectedDate;
                this.appointmentCalendar.SetValue(RadCalendar.SelectedValueProperty, (Application.Current as App).CompleteApplicationDataViewModel.SelectedDate);
            }

            //statusTextBlock.Text = (Application.Current as App).ApplicationDataStatus;
        }

        void appointmentCalendar_SelectedValueChanging(object sender, ValueChangingEventArgs<object> e)
        {
            if (((DateTime)e.NewValue).DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Sundays cannot booked");
                appointmentCalendar.ClearValue(RadCalendar.SelectedValueProperty);
                e.Cancel = true;
            }
        }

        public bool ArrivedToPageFromBackButton { get; set; }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e); 
            
            if (_isNewPageInstance)
            {
                // If the application member variable is not empty,
                // set the page's data object from the application member variable.
                if ((Application.Current as App).CompleteApplicationDataViewModel != null)
                {
                    UpdateApplicationDataUI();
                }
            }

            _isNewPageInstance = false;
        }

        private void RadCalendar_SelectedValueChanged(object sender, ValueChangedEventArgs<object> e)
        {
            if ((Application.Current as App).CompleteApplicationDataViewModel != null)
            {
                //(Application.Current as App).CompleteApplicationDataViewModel = new CompleteApplicationDataViewModel();
                (Application.Current as App).CompleteApplicationDataViewModel.SelectedDate = (DateTime)e.NewValue;
                (Application.Current as App).CompleteApplicationDataViewModel.SelectedResourceId = 0; //invalidate the resource is date is changed.
                //just initialize so they are not null
                (Application.Current as App).CompleteApplicationDataViewModel.SlotViewModel = new SlotsViewModel();
                (Application.Current as App).CompleteApplicationDataViewModel.LastPageLoaded = "/Home";
            }
        }

        private void hypLinkPageHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Resource?date=" + this.appointmentCalendar.SelectedValue, UriKind.Relative));
        }
    }
}