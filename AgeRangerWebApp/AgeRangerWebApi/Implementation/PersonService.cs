using AgeRangerBO.Models;
using AgeRangerWebApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeRangerWebApi.Implementation
{
    public class PersonService : EntityServiceBase<Person, AgeRangerDO.Model.Person>, IPersonService
    {
        private readonly AgeRangerDO.Interface.IPersonRepository _repoPerson;
        private readonly AgeRangerDO.Interface.IAgeGroupRepository _repoAgeGrp;

        public PersonService() : this (new AgeRangerDO.Implementation.PersonRepository(),
                                       new AgeRangerDO.Implementation.AgeGroupRepository()) { }

        public PersonService(AgeRangerDO.Interface.IPersonRepository repoPerson,
            AgeRangerDO.Interface.IAgeGroupRepository repoAgeGrp) : base(repoPerson)
        {
            _repoPerson = (AgeRangerDO.Interface.IPersonRepository)_repo;
            _repoAgeGrp = repoAgeGrp;
        }
        
        private AgeGroup AgeGroupOfPerson(long age)
        {
            var ag = new AgeRangerDO.Implementation.RepoExtensions<AgeRangerDO.Model.AgeGroup>(_repoAgeGrp).GetAgeGroupByAge(age);
            return AgeGroupRepoToBO(ag);
        }

        protected override Person EntityRepoToBO(AgeRangerDO.Model.Person person)
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

        protected override AgeRangerDO.Model.Person EntityBOToRepo(Person person)
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

        public IEnumerable<Person> GetByName(string name)
        {
            var persons = _repoPerson.SelectByName(name) as IEnumerable<AgeRangerDO.Model.Person>;
            var retPersons = persons.Select(EntityRepoToBO);
            return retPersons;
        }

        public override Person Insert(Person person)
        {
            long iRes = _repoPerson.Insert(EntityBOToRepo(person));
            if (iRes == 1)
            {
                long id = _repoPerson.GetLastInsertId();
                person.Id = id;
                return person;
            }
            return null;
        }
        
        public override long Delete(long id)
        {
            var person = new AgeRangerDO.Model.Person { Id = id };
            long iRes = _repoPerson.Delete(person);
            return iRes;
        }
    }
}