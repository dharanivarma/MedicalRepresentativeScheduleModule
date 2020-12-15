using MedicalRepresentativeSchedule.models;
using MedicalRepresentativeSchedule.Providers;
using MedicalRepresentativeSchedule.repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MedicalRepresentativeScheduleTest
{
    public class Tests
    {
        Mock<IRepScheduleProvider> schedulepro;
        List<Doctor> doctors;
        List<RepresentativeDetails> representatives;
        List<MedicineStock> stock;
        List<RepSchedule> schedulelist;
        Mock<RepScheduleRepository >repos;



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
            repos = new Mock<RepScheduleRepository>();
            schedulepro.Setup(m => m.GetDoctors()).Returns(doctors);
            schedulepro.Setup(m => m.GetRepresentatives()).Returns(representatives);

        }


        [Test]
        public void TestGetDoctors()
        {
            var pro = new RepScheduleProvider(repos.Object);
            var res = pro.GetDoctors();
            Assert.IsNotNull(res);
        }

        [Test]
        public void TestGetRepresentatives()
        {
            var pro = new RepScheduleProvider(repos.Object);
            var res = pro.GetRepresentatives();
            Assert.IsNotNull(res);
        }


        [TestCase("2020/12/19")]
        [TestCase("2020/11/10")]
        [TestCase("2020/10/01")]
        public void Provider_ScheduleMeet(DateTime date1)
        {
            var pro = new RepScheduleProvider(repos.Object);
            var res = pro.GetRepScheduleAsync(date1);
            //var c = data.Count;
            //Assert.AreEqual(5, c);
            Assert.IsNotNull(res);


        }


    }



}
