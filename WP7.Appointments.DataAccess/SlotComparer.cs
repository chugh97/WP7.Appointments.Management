using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP7.Appointments.DataAccess
{
    public class SlotComparer : IEqualityComparer<Slot>
    {
        public bool Equals(Slot x, Slot y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Slot obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
