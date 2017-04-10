using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRangerWebApi;
using AgeRangerWebApi.Controllers;
using AgeRangerBO.Models;
using Moq;
using AgeRangerWebApi.Interface;
using System.Web.Http.Results;

namespace AgeRangerWebApi.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {
        private List<AgeGroup> _ag;
        private List<Person> _pers;
        private PersonController ctrlr;

        private Person timTam = new Person { Id = 1, FirstName = "Tim", LastName = "Tam" };
        private Person tickTack = new Person { Id = 2, FirstName = "Tick", LastName = "Tack" };

        [TestInitialize]
        public void Setup()
        {
            AgeGroup ag1 = new AgeGroup { Id = 1, MinAge = null, MaxAge = 10, Description = "Less than 10" };
            AgeGroup ag2 = new AgeGroup { Id = 2, MinAge = 10, MaxAge = 50, Description = "10 to less than 50" };
            AgeGroup ag3 = new AgeGroup { Id = 3, MinAge = 50, MaxAge = 100, Description = "50 to less than 100" };
            AgeGroup ag4 = new AgeGroup { Id = 4, MinAge = 100, MaxAge = null, Description = "Above 100" };
            _ag = new List<AgeGroup> { ag2, ag4, ag1, ag3 };

            _pers = new List<Person>();
        }

        [TestMethod]
        public void TestController()
        {
            TestWithEmptyPersonRepo();
            TestWithFilledPersonRepo();
        }

        private void TestWithEmptyPersonRepo()
        {
            var moqSvc = new Mock<IPersonService>();
            ctrlr = new PersonController(moqSvc.Object);

            moqSvc.Setup(r => r.GetAll()).Returns(_pers);
            IEnumerable<Person> pers = TestGetAll();
            Assert.AreEqual(0, pers.Count());

            moqSvc.Setup(r => r.GetSingle(1)).Returns(_pers.FirstOrDefault());
            Person per = TestGet(1);
            Assert.IsNull(per);

            moqSvc.Setup(r => r.Insert(_pers.FirstOrDefault())).Returns(_pers.FirstOrDefault());
            try
            {
                per = TestPost(per);    //should be a null - exception
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
            }

            moqSvc.Setup(r => r.Update(_pers.FirstOrDefault())).Returns(0);
            try
            {
                string res = TestPut(per);  //should be a null - exception
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
            }
            string res2 = TestPut(tickTack);
            Assert.AreEqual("NotFound", res2);

            moqSvc.Setup(r => r.Delete(1)).Returns(0);
            string res3 = TestDelete(0);
            Assert.AreEqual("NotFound", res3);
        }

        private void TestWithFilledPersonRepo()
        {
            var moqSvc = new Mock<IPersonService>();
            ctrlr = new PersonController(moqSvc.Object);

            _pers.Add(timTam);
            moqSvc.Setup(r => r.GetAll()).Returns(_pers);
            IEnumerable<Person> pers = TestGetAll();
            Assert.AreEqual(1, pers.Count());

            moqSvc.Setup(r => r.GetSingle(1)).Returns(_pers.FirstOrDefault());
            Person per = TestGet(1);
            Assert.AreEqual("Tim", per.FirstName);

            Person newPer = new Person { Id = 10 };
            moqSvc.Setup(r => r.Insert(per)).Returns(newPer);
            Person insPer = TestPost(per);
            Assert.AreEqual(10, insPer.Id);

            moqSvc.Setup(r => r.Update(timTam)).Returns(1);
            string res2 = TestPut(timTam);
            Assert.AreEqual("Person updated successfully.", res2);

            moqSvc.Setup(r => r.Delete(1)).Returns(1);
            string res3 = TestDelete(1);
            Assert.AreEqual("Person deleted successfully.", res3);
        }

        private IEnumerable<Person> TestGetAll()
        {
            IHttpActionResult actionResult = ctrlr.GetAll();
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<Person>>;
            IEnumerable<Person> pers = response.Content;
            return pers;
        }

        private Person TestGet(long id)
        {
            IHttpActionResult actionResult = ctrlr.Get(id);
            var response = actionResult as OkNegotiatedContentResult<Person>;
            Person per = response.Content;
            return per;
        }

        private Person TestPost(Person person)
        {
            IHttpActionResult actionResult = ctrlr.Post(person);
            var response = actionResult as CreatedNegotiatedContentResult<Person>;
            Person per = response.Content;
            return per;
        }

        private string TestPut(Person person)
        {
            IHttpActionResult actionResult = ctrlr.Put(person);
            if (actionResult.GetType() == typeof(NotFoundResult))
                return "NotFound";
            var response = actionResult as OkNegotiatedContentResult<string>;
            return response.Content;
        }

        private string TestDelete(long id)
        {
            IHttpActionResult actionResult = ctrlr.Delete(id);
            if (actionResult.GetType() == typeof(NotFoundResult))
                return "NotFound";
            var response = actionResult as OkNegotiatedContentResult<string>;
            return response.Content;
        }
    }
}
