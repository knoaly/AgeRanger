using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRangerDO.Implementation;
using AgeRangerDO.Model;
using System.Linq;

namespace AgeRangerDO.Tests
{
    [TestClass]
    public class PersonRepoIntegrationTest
    {
        private PersonRepository _repoPerson;

        /// <summary>
        /// Integration Test: to make sure CRUD operation works fine on sqlite-net
        /// </summary>
        [TestMethod]
        public void TestPersonCRUD()
        {
            _repoPerson = new PersonRepository();

            IEnumerable<Person> persons = GetAllPerson();
            long initialCount = persons.Count();
            System.Diagnostics.Debug.WriteLine("Total persons: {0}", initialCount);

            InsertPersonTest();
            long personId = _repoPerson.GetLastInsertId();
            Person inserted = GetPerson(personId);
            System.Diagnostics.Debug.WriteLine("Person inserted, id: {0}, first.n: {1}, last.n: {2}", personId, inserted.FirstName, inserted.LastName);

            string strAgeGrp = AgeGroupOfPerson(inserted.Age.Value);
            System.Diagnostics.Debug.WriteLine("Person inserted, id: {0}, age: {1}, group: {2}", personId, inserted.Age, strAgeGrp);

            UpdatePersonTest(personId);
            Person updated = GetPerson(personId);
            System.Diagnostics.Debug.WriteLine("Person updated, id: {0}, first.n: {1}, last.n: {2}", personId, updated.FirstName, updated.LastName);

            strAgeGrp = AgeGroupOfPerson(updated.Age.Value);
            System.Diagnostics.Debug.WriteLine("Person updated, id: {0}, age: {1}, group: {2}", personId, updated.Age, strAgeGrp);

            DeletePersonTest(personId);
            persons = GetAllPerson();
            System.Diagnostics.Debug.WriteLine("Total persons: {0}", persons.Count());

            Assert.AreEqual(initialCount, persons.Count());
        }

        private IEnumerable<Person> GetAllPerson()
        {
            return _repoPerson.SelectAll();
        }

        private Person GetPerson(long id)
        {
            return _repoPerson.Select(id);
        }

        private void InsertPersonTest()
        {
            Person person = new Person
            {
                FirstName = "Simon",
                LastName = "Bordeoux",
                Age = 77
            };
            long iRes = _repoPerson.Insert(person);
            System.Diagnostics.Debug.WriteLine("{0} record(s) inserted", iRes);
        }

        private void UpdatePersonTest(long id)
        {
            Person person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Mayer",
                Age = 22
            };
            long iRes = _repoPerson.Update(person);
            System.Diagnostics.Debug.WriteLine("{0} record(s) updated", iRes);
        }

        private void DeletePersonTest(long id)
        {
            Person person = new Person
            {
                Id = id
            };
            long iRes = _repoPerson.Delete(person);
            System.Diagnostics.Debug.WriteLine("{0} record(s) deleted", iRes);
        }

        private string AgeGroupOfPerson(long age)
        {
            AgeGroupRepository repo = new AgeGroupRepository();
            var ag = new RepoExtensions<AgeGroup>(repo).GetAgeGroupByAge(age);
            return ag.Description;
        }
    }
}
