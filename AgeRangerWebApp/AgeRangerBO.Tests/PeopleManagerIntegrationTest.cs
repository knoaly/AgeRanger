using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRangerBO.Managers;
using System.Collections.Generic;
using AgeRangerBO.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AgeRangerBO.Tests
{
    [TestClass]
    public class PeopleManagerIntegrationTest
    {
        const string webapiUrl = "http://localhost:11883/";

        private PeopleManager _peopleMgr;

        [TestInitialize]
        public void Setup()
        {
            _peopleMgr = new PeopleManager(webapiUrl);
        }

        [TestMethod]
        public void TestPeopleManager()
        {
            int currentCount = TestGetAll().Result;
            System.Diagnostics.Debug.WriteLine("Current count: {0}", currentCount);

            var person = new Person { FirstName = "Lance", LastName = "Lai", Age = 40 };
            Person newPerson = TestInsert(person).Result;
            System.Diagnostics.Debug.WriteLine("New Person: {0}, Age {1}",
                newPerson.FirstName + " " + newPerson.LastName,
                newPerson.Age);

            Person selectedPerson = TestGetSingle(newPerson.Id.Value).Result;
            System.Diagnostics.Debug.WriteLine("Selected Person: {0}, Age {1}, Group {2}",
                selectedPerson.FirstName + " " + newPerson.LastName,
                selectedPerson.Age,
                selectedPerson.Agegroup.Description);

            selectedPerson.FirstName = "Yok Kin " + new Random().Next(0, 999);
            selectedPerson.Age = null;
            string updateMsg = TestUpdate(selectedPerson).Result;
            System.Diagnostics.Debug.WriteLine("Update person return message: " + updateMsg);

            IEnumerable<Person> selectedPeople = TestGetByName(selectedPerson.FirstName).Result;
            System.Diagnostics.Debug.WriteLine("Selected count: {0}", selectedPeople.Count());
            System.Diagnostics.Debug.WriteLine("First selected Person: {0} (Id: {3}), Age {1}, Group {2}",
                selectedPeople.First().FirstName + " " + newPerson.LastName,
                selectedPeople.First().Age,
                selectedPeople.First().Agegroup == null ? "Not found" : selectedPeople.First().Agegroup.Description,
                selectedPeople.First().Id);

            string deleteMsg = TestDelete(selectedPerson.Id.Value).Result;
            System.Diagnostics.Debug.WriteLine("Delete person return message: " + deleteMsg);

            var latestCount = TestGetAll().Result;
            System.Diagnostics.Debug.WriteLine("Redo current count: {0}", latestCount);

            Assert.AreEqual(currentCount, latestCount);
        }

        private async Task<int> TestGetAll()
        {
            IEnumerable<Person> people = await _peopleMgr.GetAll();
            return people.Count();
        }

        private async Task<Person> TestInsert(Person person)
        {
            Person newPerson = await _peopleMgr.Insert(person);
            return newPerson;
        }

        private async Task<Person> TestGetSingle(long id)
        {
            Person person = await _peopleMgr.GetSingle(id);
            return person;
        }

        private async Task<string> TestUpdate(Person person)
        {
            string retVal = await _peopleMgr.Update(person);
            return retVal;
        }

        private async Task<IEnumerable<Person>> TestGetByName(string name)
        {
            IEnumerable<Person> people = await _peopleMgr.GetByName(name);
            return people;
        }

        private async Task<string> TestDelete(long id)
        {
            string retVal = await _peopleMgr.Delete(id);
            return retVal;
        }
    }
}
