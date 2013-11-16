using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace WP7.Appointments.DataContracts
{
    [DataContract]
    public class Customer : EntityBase
    {
        private int _id;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.PropertyChangedHandler(this, "Id");
                }
            }
        }

        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.PropertyChangedHandler(this, "Name");
                }
            }
        }

        [DataMember]
        public ObservableCollection<ContactInformation> ContactInformation { get; set; }

    }
}
