﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using AgeRangerDO.Interface;
using AgeRangerDO.Model;
using AgeRangerDO.Implementation;

namespace AgeRangerDO.Tests
{
    [TestClass]
    public class RepoExtUnitTest
    {
        AgeGroup ag1 = new AgeGroup { Id = 1, MinAge = null, MaxAge = 10, Description = "Less than 10" };
        AgeGroup ag2 = new AgeGroup { Id = 2, MinAge = 10, MaxAge = 50, Description = "10 to less than 50" };
        AgeGroup ag3 = new AgeGroup { Id = 3, MinAge = 50, MaxAge = 100, Description = "50 to less than 100" };
        AgeGroup ag4 = new AgeGroup { Id = 4, MinAge = 100, MaxAge = null, Description = "Above 100" };
        AgeGroup ag3x = new AgeGroup { Id = 3, MinAge = 51, MaxAge = 100, Description = "51 to less than 100" };

        Mock<IRepository<AgeGroup>> moqRepo;

        [TestInitialize]
        public void Setup()
        {
            moqRepo = new Mock<IRepository<AgeGroup>>();
        }

        [TestMethod]
        public void TestGetAgeGroupCorrectness()
        {
            //mock
            List<AgeGroup> ag = new List<AgeGroup> { ag2, ag4, ag1, ag3 };
            moqRepo.Setup(r => r.SelectAll()).Returns(ag);

            //assert
            var rx = new RepoExtensions<AgeGroup>(moqRepo.Object);
            Assert.AreEqual(ag1.Description, rx.GetAgeGroupByAge(1).Description);
            Assert.AreEqual(ag2.Description, rx.GetAgeGroupByAge(10).Description);
            Assert.AreEqual(ag2.Description, rx.GetAgeGroupByAge(49).Description);
            Assert.AreEqual(ag3.Description, rx.GetAgeGroupByAge(75).Description);
            Assert.AreEqual(ag4.Description, rx.GetAgeGroupByAge(100).Description);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Age could not be found in Age Range.")]
        public void TestAgeGroupException()
        {
            List<AgeGroup> ags = new List<AgeGroup> { ag2, ag4, ag1, ag3x };
            moqRepo.Setup(r => r.SelectAll()).Returns(ags);

            var rx = new RepoExtensions<AgeGroup>(moqRepo.Object);
            Assert.AreEqual(ag2.Description, rx.GetAgeGroupByAge(49).Description);
            Assert.AreEqual(ag3x.Description, rx.GetAgeGroupByAge(51).Description);

            //expect exception
            rx.GetAgeGroupByAge(50);
        }
    }
}
