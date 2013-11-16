using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WP7.Appointments.DataContracts
{
    [DataContract]
    public abstract class EntityBase : INotifyPropertyChanged
    {
        protected void PropertyChangedHandler(EntityBase sender,
            string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}
