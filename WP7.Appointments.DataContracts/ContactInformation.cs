using System.Runtime.Serialization;

namespace WP7.Appointments.DataContracts
{
    [DataContract]
    public class ContactInformation : EntityBase
    {
        private string _contactAttributeKey;

        [DataMember]
        public string ContactAttributeKey
        {
            get { return _contactAttributeKey; }
            set
            {
                if (_contactAttributeKey != value)
                {
                    _contactAttributeKey = value;
                    base.PropertyChangedHandler(this, "ContactAttributeKey");
                }
            }
        }

        private string _contactAttributeValue;

        [DataMember]
        public string ContactAttributeValue
        {
            get { return _contactAttributeValue; }
            set
            {
                if (_contactAttributeValue != value)
                {
                    _contactAttributeValue = value;
                    base.PropertyChangedHandler(this, "ContactAttributeValue");
                }
            }
        }
    }
}
