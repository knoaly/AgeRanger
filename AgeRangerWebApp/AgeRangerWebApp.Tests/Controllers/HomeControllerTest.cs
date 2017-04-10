using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AgeRangerBO.Managers;
using AgeRangerWebApp.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AgeRangerBO.ViewModels;
using AgeRangerBO.Models;
using System.Collections.Generic;

namespace AgeRangerWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IPeopleManager> moqMgr;
        private Person person;
        private HomeController controller;

        [TestInitialize]
        public void Setup()
        {
            moqMgr = new Mock<IPeopleManager>();
            moqMgr.Setup(m => m.GetAll()).Returns(Task.FromResult<IEnumerable<Person>>(new List<Person>()));
            moqMgr.Setup(m => m.GetByName("")).Returns(Task.FromResult<IEnumerable<Person>>(new List<Person>()));
            moqMgr.Setup(m => m.GetSingle(1)).Returns(Task.FromResult(new Person { Id = 1 }));
            person = new Person { Id = -1 };
            moqMgr.Setup(m => m.Insert(person)).Returns(Task.FromResult(person));
            moqMgr.Setup(m => m.Delete(1)).Returns(Task.FromResult("success"));
            controller = new HomeController(moqMgr.Object);
        }

        [TestMethod]
        public async Task TestAll()
        {
            await TestView();
            await TestData();
            await TestAction();
        }

        private async Task TestView()
        {
            
            var result = await controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

            var formVm = new PeopleViewModel();
            result = await controller.Index(formVm) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

            var pvresult = await controller.Details(-1) as PartialViewResult;
            Assert.AreEqual("_Details", pvresult.ViewName);
        }

        private async Task TestData()
        {
            var controller = new HomeController(moqMgr.Object);
            var result = await controller.Index() as ViewResult;
            var vm = (PeopleViewModel)result.Model;
            Assert.AreEqual(0, vm.EntityList.Count());

            var formVm = new PeopleViewModel
            {
                EntitySearch = new Person { FirstName = "" },
                EventCommand = "search"
            };
            result = await controller.Index(formVm) as ViewResult;
            vm = (PeopleViewModel)result.Model;
            Assert.AreEqual(0, vm.EntityList.Count());

            var pvresult = await controller.Details(-1) as PartialViewResult;
            Assert.AreEqual(null, ((Person)pvresult.Model).Id);

            var jresult = await controller.Details(person) as JsonResult;
            Assert.AreEqual("{ success = True }", jresult.Data.ToString());
        }

        private async Task TestAction()
        {
            var formVm = new PeopleViewModel
            {
                EventArgument = "1",
                EventCommand = "delete"
            };
            var result = await controller.Index(formVm) as ViewResult;
            var vm = (PeopleViewModel)result.Model;
            Assert.AreEqual(0, vm.EntityList.Count());

            var pvresult = await controller.Details(1) as PartialViewResult;
            Assert.AreEqual(1, ((Person)pvresult.Model).Id);
        }
    }
}
