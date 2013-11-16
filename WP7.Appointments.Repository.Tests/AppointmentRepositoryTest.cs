using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using WP7.Appointments.DataAccess;

namespace WP7.Appointments.Repository.Tests
{
    [TestClass]
    public class AppointmentRepositoryTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            AppointmentsContext ctx = new AppointmentsContext();
            //AppointmentsContextInitializer initializer = new AppointmentsContextInitializer();

            var x = (from c in ctx.Appointments
                     where c.Id == 1
                     select c).FirstOrDefault();
        }

        [TestMethod]
        public void Get_Resources_By_Date()
        {
            var appointmentRepository = new AppointmentRepository();
            var resources = appointmentRepository.GetResourcesByDate(DateTime.Today);
            resources.Count.ShouldEqual(2);
        }

        [TestMethod]
        public void Get_Resources_One_Resource_Busy()
        {
            var appointmentRepository = new AppointmentRepository();
            var resources = appointmentRepository.GetResourcesByDate(new DateTime(2012,2,16));
            resources.Count.ShouldEqual(1);
        }
        
        [TestMethod]
        public void Get_All_Available_Slots()
        {
            var appointmentRepository = new AppointmentRepository();
            var slots = appointmentRepository.GetAllAvailableSlots(1, DateTime.Today);
            slots.Count.ShouldEqual(18);
        }

        [TestMethod]
        public void Get_All_Available_Slots_Nurse_On_Half_Day()
        {
            var appointmentRepository = new AppointmentRepository();
            var slots = appointmentRepository.GetAllAvailableSlots(2, DateTime.Today.AddDays(30));
            slots.Count.ShouldEqual(8);
        }

        [TestMethod]
        public void Get_All_Available_Slots_Some_Slot_NotAvailable()
        {
            var appointmentRepository = new AppointmentRepository();
            //2 is Nurse: 1 is Doctor
            var slots = appointmentRepository.GetAllAvailableSlots(1, DateTime.Today.AddDays(30));
            slots.Count.ShouldEqual(16);
        }

        [TestMethod]
        public void Book_Appointment()
        {
            var appointmentRepository = new AppointmentRepository();
            var context = new AppointmentsContext();
            var appCount = context.Appointments.Count();

            ICollection<ContactInformation> contactInfo = new Collection<ContactInformation>();
            contactInfo.Add(new ContactInformation(){ ContactAttributeKey = "Phone", ContactAttributeValue = "1312567890"});
            contactInfo.Add(new ContactInformation() { ContactAttributeKey = "Email", ContactAttributeValue = "neil.s@yahoo.com" });
            var customer = new Customer() {Name = "Neil S", ContactInformation = contactInfo };
            
            //2 is Nurse: 1 is Doctor

            appointmentRepository.BookAppointment(DateTime.Today, 1, 4, customer);


            context.Appointments.Count().ShouldEqual(appCount+1);
        }

        [TestMethod]
        public void Book_Appointment_By_CustomerId()
        {
            var appointmentRepository = new AppointmentRepository();
            var context = new AppointmentsContext();
            var appCount = context.Appointments.Count();

            ICollection<ContactInformation> contactInfo = new Collection<ContactInformation>();
            contactInfo.Add(new ContactInformation() { ContactAttributeKey = "Phone", ContactAttributeValue = "1312567890" });
            contactInfo.Add(new ContactInformation() { ContactAttributeKey = "Email", ContactAttributeValue = "neil.s@yahoo.com" });
            var customer = new Customer() { Name = "Neil S" , ContactInformation = contactInfo };

            //2 is Nurse: 1 is Doctor

            appointmentRepository.BookAppointment(DateTime.Today, 1, 4, customer);
            
            context.Appointments.Count().ShouldEqual(appCount+ 1);
            context.Customers.Count().ShouldEqual(3);
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }
    }
}
