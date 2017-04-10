using AgeRangerBO.Models;
using AgeRangerWebApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeRangerWebApi.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly AgeRangerDO.Interface.IPersonRepository _repoPerson;
        private readonly AgeRangerDO.Interface.IAgeGroupRepository _repoAgeGrp;

        public PersonService()
        {
            _repoPerson = new AgeRangerDO.Implementation.PersonRepository();
            _repoAgeGrp = new AgeRangerDO.Implementation.AgeGroupRepository();
        }

        //ToDo: Unit Testing
        public PersonService(AgeRangerDO.Interface.IPersonRepository repoPerson,
            AgeRangerDO.Interface.IAgeGroupRepository repoAgeGrp)
        {
            _repoPerson = repoPerson;
            _repoAgeGrp = repoAgeGrp;
        }

        public IEnumerable<Person> GetAll()
        {
            var persons = _repoPerson.SelectAll() as IEnumerable<AgeRangerDO.Model.Person>;
            var retPersons = persons.Select(PersonRepoToBO);
            return retPersons;
        }

        private AgeGroup AgeGroupOfPerson(long age)
        {
            var ag = new AgeRangerDO.Implementation.RepoExtensions<AgeRangerDO.Model.AgeGroup>(_repoAgeGrp).GetAgeGroupByAge(age);
            return AgeGroupRepoToBO(ag);
        }

        private Person PersonRepoToBO(AgeRangerDO.Model.Person person)
        {
            Person per = new Person
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Agegroup = person.Age == null ? null : AgeGroupOfPerson(person.Age.Value)
            };
            return per;
        }

        private AgeRangerDO.Model.Person PersonBOToRepo(Person person)
        {
            AgeRangerDO.Model.Person per = new AgeRangerDO.Model.Person
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age
            };
            return per;
        }

        private AgeGroup AgeGroupRepoToBO(AgeRangerDO.Model.AgeGroup ag)
        {
            AgeGroup agrp = new AgeGroup
            {
                Id = ag.Id,
                MinAge = ag.MinAge,
                MaxAge = ag.MaxAge,
                Description = ag.Description
            };
            return agrp;
        }

        public Person GetSingle(long id)
        {
            var rPerson = _repoPerson.Select(id);
            var person = PersonRepoToBO(rPerson);
            return person;
        }

        public IEnumerable<Person> GetByName(string name)
        {
            var persons = _repoPerson.SelectByName(name) as IEnumerable<AgeRangerDO.Model.Person>;
            var retPersons = persons.Select(PersonRepoToBO);
            return retPersons;
        }

        public Person Insert(Person person)
        {
            long iRes = _repoPerson.Insert(PersonBOToRepo(person));
            if (iRes == 1)
            {
                long id = _repoPerson.GetLastInsertId();
                person.Id = id;
                return person;
            }
            return null;
        }

        public long Update(Person person)
        {
            long iRes = _repoPerson.Update(PersonBOToRepo(person));
            return iRes;
        }

        public long Delete(long id)
        {
            var person = new AgeRangerDO.Model.Person { Id = id };
            long iRes = _repoPerson.Delete(person);
            return iRes;
        }
    }
}