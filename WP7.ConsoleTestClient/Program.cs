using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP7.Appointments.DataAccess;

namespace WP7.ConsoleTestClient
{
    class Program
    {
        static void Main(string[] args)
        {

            //Appointments.DataAccess.AppointmentsContextInitializer ctx = new AppointmentsContextInitializer();
            AppointmentsContext ctx = new AppointmentsContext();

            var y = DateTime.MinValue;

            var x = (from c in ctx.Appointments
                     where c.Id == 1
                     select c).FirstOrDefault();
        }
    }
}
