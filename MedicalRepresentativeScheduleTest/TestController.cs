using MedicalRepresentativeSchedule.Controllers;
using MedicalRepresentativeSchedule.models;
using MedicalRepresentativeSchedule.Providers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRepresentativeScheduleTest
{
    class TestController
    {
        List<RepSchedule> schedulelist;
        Mock<RepScheduleController> schedulecon;
        Mock<IRepScheduleProvider> schedulepro;
        List<Doctor> doctors;
        List<MedicineStock> stock;
        List<RepresentativeDetails> representatives;
        
        [SetUp]
        public void Setup()
        {
            doctors = new List<Doctor>()
             {
               new Doctor { Name = "doc1",ContactNumber=0987654321 , TreatingAilment="Orthopaedics"},
               new Doctor { Name = "doc2",ContactNumber=0987654321 , TreatingAilment="General"},
               new Doctor { Name = "doc3",ContactNumber=0987654321 , TreatingAilment="Gynecology"},
               new Doctor { Name = "doc4",ContactNumber=0987654321 , TreatingAilment="Orthopaedics"},
            };
            representatives = new List<RepresentativeDetails>()
            {
                new RepresentativeDetails{RepresentativeName= "rep1" },
                new RepresentativeDetails{RepresentativeName= "rep2" },
                new RepresentativeDetails{RepresentativeName= "rep3" }
            };
            stock = new List<MedicineStock>()
            { new MedicineStock
            {
                Name = "Medicine1",
                ChemicalComposition = new List<string> { "chemical1", "chemical2" },
                TargetAilment = "Orthopaedics",
                DateOfExpiry = DateTime.Parse("10-10-2021"),
                NumberOfTabletsInStock = 50
            },
            new MedicineStock
            {
                Name = "Medicine2",
                ChemicalComposition = new List<string> { "chemical3", "chemical2" },
                TargetAilment = "General",
                DateOfExpiry = DateTime.Parse("10-09-2021"),
                NumberOfTabletsInStock = 50
            },
            new MedicineStock
            {
                Name = "Medicine3",
                ChemicalComposition = new List<string> { "chemical1", "chemical2" },
                TargetAilment = "Gynecology",
                DateOfExpiry = DateTime.Parse("10-10-2021"),
                NumberOfTabletsInStock = 50
            },
            };
            schedulepro = new Mock<IRepScheduleProvider>();
            schedulecon = new Mock<RepScheduleController>();

        }



        [TestCase("2020/11/12")]
        public void TestControllerLayerCorrectInput(DateTime startdate)
        {
            var pro = new RepScheduleController(schedulepro.Object);
            var res = pro.Get(startdate);
            Assert.IsNotNull(res);
        }
       


        [TestCase("2020/11/9")]
        [TestCase("2020/11/10")]
        [TestCase("2020/11/11")]
        public void ScheduleMeetingController_GetMeetingStartDate(DateTime startdate)
        {

            var pro = new RepScheduleController(schedulepro.Object);
            var data = pro.Get(startdate);
            Assert.IsNotNull(data);

        }

        [TestCase("2020/11/90")]
        [TestCase("2020/110/10")]
        [TestCase("220/11/11")]
        public void ScheduleMeetingController_InvalidInput(DateTime startdate)
        {

            var pro = new RepScheduleController(schedulepro.Object);
            var res = pro.Get(startdate);
            //var s = res as InternalServerError;
            //Assert.AreEqual(500, s.StatusCode);
            Assert.IsNull(res);
        }

    }
}
