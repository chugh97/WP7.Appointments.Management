using System.Runtime.Serialization;

namespace WP7.Appointments.DataContracts
{
    [DataContract]
    public class Resource : EntityBase
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

        private string _description;

        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    base.PropertyChangedHandler(this, "Description");
                }
            }
        }
    }
}
