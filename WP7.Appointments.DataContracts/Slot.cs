using System;
using System.Runtime.Serialization;

namespace WP7.Appointments.DataContracts
{
    [DataContract]
    public class Slot : EntityBase
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

        private TimeSpan _startTime;

        [DataMember]
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    base.PropertyChangedHandler(this, "StartTime");
                }
            }
        }

        private TimeSpan _endTime;

        [DataMember]
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    base.PropertyChangedHandler(this, "EndTime");
                }
            }
        }
    }
}
