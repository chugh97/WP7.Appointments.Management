using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP7.Appointments.Management.ViewModels;
using System.Runtime.Serialization;

namespace WP7.Appointments.Management
{
    [DataContract] 
    public class CompleteApplicationDataViewModel
    {
        [DataMember]
        public DateTime? SelectedDate { get; set; }

        [DataMember]
        public string LastPageLoaded { get; set; }

        [DataMember]
        public int SelectedResourceId { get; set; }

        [DataMember]
        public int SelectedResourceIndex { get; set; }

        [DataMember]
        public ResourceViewModel ResourceViewModel { get; set; }

        [DataMember]
        public SlotsViewModel SlotViewModel { get; set; }

        [DataMember]
        public int SelectedSlotIndex { get; set; }

        [DataMember]
        public int SelectedSlotId { get; set; }
    }
}
