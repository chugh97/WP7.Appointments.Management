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
using WP7.ValidationControl;

namespace WP7.Appointments.Management
{
    public partial class CustomerDetails : PhoneApplicationPage
    {
        public CustomerDetails()
        {
            InitializeComponent();
            string emailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            this.CustomerName.ValidationRule = new RegexValidationRule(emailPattern);

            //Validates an URL 
            string urlPattern = @"^((http|https?:\/\/)?((?:[-a-z0-9]+\.)+[a-z]{2,}))$";
            this.CustomerName2.ValidationRule = new RegexValidationRule(urlPattern);  

        }

        private void CustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}