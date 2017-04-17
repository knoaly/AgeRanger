using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AgeRangerDO.Interface;
using AgeRangerBO.Models;
using System.Collections.Generic;
using AgeRangerWebApi.Implementation;
using System.Linq;

namespace AgeRangerWebApi.Tests
{
    [TestClass]
    public class PersonServiceTest
    {
        private PersonService _personSvc;

        private Mock<IPersonRepository> _moqPersonRepo;
        private Mock<IAgeGroupRepository> _moqAgeGrpRepo;

        [TestInitialize]
        public void Setup()
        {
            _moqAgeGrpRepo = new Mock<IAgeGroupRepository>();
            var ageGrps = new List<AgeRangerDO.Model.AgeGroup>
            {
                new AgeRangerDO.Model.AgeGroup { MinAge = 1000, Description = "KauriTree" },
                new AgeRangerDO.Model.AgeGroup { MaxAge = 999, Description = "Vamp" }
            };
            _moqAgeGrpRepo.Setup(a => a.SelectAll()).Returns(ageGrps);

            _moqPersonRepo = new Mock<IPersonRepository>();
            var kaoree = new AgeRangerDO.Model.Person { Id = 1, FirstName = "Kaoree", LastName = "Tee", Age = 3000 };
            var _peeps = new List<AgeRangerDO.Model.Person>
            {
                kaoree,
                new AgeRangerDO.Model.Person { Id = 2, FirstName = "Vel", LastName = "Linton", Age = 555 }
            };
            _moqPersonRepo.Setup(p => p.SelectAll()).Returns(_peeps);
            _moqPersonRepo.Setup(p => p.Select(1)).Returns(kaoree);
            _moqPersonRepo.Setup(p => p.SelectByName("a")).Returns(new List<AgeRangerDO.Model.Person> { kaoree });
            var cruise = new AgeRangerDO.Model.Person { Id = 3, FirstName = "Dom", LastName = "Cruise", Age = 666 };
            _moqPersonRepo.Setup(p => p.Insert(cruise)).Returns(1);
            _moqPersonRepo.Setup(p => p.GetLastInsertId()).Returns(3);
            _moqPersonRepo.Setup(p => p.Update(cruise)).Returns(1);
            _moqPersonRepo.Setup(p => p.Delete(new AgeRangerDO.Model.Person { Id = 3 })).Returns(1);

            _personSvc = new PersonService(_moqPersonRepo.Object, _moqAgeGrpRepo.Object);
        }

        [TestMethod]
        public void TestPersonService()
        {
            TestGetAll();
            TestGetSingle(1, "KauriTree");
            TestGetByName("a", "KauriTree");
            var cruiseBO = new Person { Id = 3, FirstName = "Dom", LastName = "Cruise", Age = 666 };
            TestInsert(cruiseBO);
            TestUpdate(cruiseBO);
            TestDelete(3);
        }

        private void TestGetAll()
        {
            IEnumerable<Person> everyone = _personSvc.GetAll();
            Assert.AreEqual(2, everyone.Count());
            Assert.AreEqual(1, everyone.ToList().Select(e => e.Agegroup.Description)
                                                .Where(a => a.Contains("Vamp")).Count());
        }

        private void TestGetSingle(long id, string ageGrpDesc)
        {
            Person person = _personSvc.GetSingle(id);
            Assert.AreEqual(id, person.Id);
            Assert.AreEqual(ageGrpDesc, person.Agegroup.Description);
        }

        private void TestGetByName(string name, string ageGrpDesc)
        {
            IEnumerable<Person> people = _personSvc.GetByName(name);
            Assert.IsTrue(people.First().FirstName.Contains(name) || people.First().LastName.Contains(name));
            Assert.AreEqual(ageGrpDesc, people.First().Agegroup.Description);
        }

        private void TestInsert(Person personBObj)
        {
            Person person = _personSvc.Insert(personBObj);
            Assert.AreEqual(personBObj.Id, person.Id);
        }

        private void TestUpdate(Person personBObj)
        {
            var res = _personSvc.Update(personBObj);
            Assert.AreEqual(1, res);
        }

        private void TestDelete(long id)
        {
            var res = _personSvc.Delete(id);
            Assert.AreEqual(1, res);
        }
    }
}
