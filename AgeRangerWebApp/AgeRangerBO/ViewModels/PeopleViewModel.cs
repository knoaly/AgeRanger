using AgeRangerBO.Managers;
using AgeRangerBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.ViewModels
{
    public class PeopleViewModel : ViewModelDetailBase<Person>
    {
        private readonly IPeopleManager _peopleManager;

        public PeopleViewModel() : base()
        {
        }

        public PeopleViewModel(IPeopleManager peopleManager) : base(peopleManager)
        {
            _peopleManager = peopleManager;
            EntitySearch = new Person();
        }
        
        public async Task GetEntityByName()
        {
            IEnumerable<Person> persons = await _peopleManager.GetByName(EntitySearch.FirstName);
            EntityList = persons;
        }

        public void SetupDetailForCreate()
        {
            EntityDetail = new Person();
        }

        public async Task<bool> SaveEntity()
        {
            bool result;
            if (EntityDetail.Id == -1)
            {
                Person person = await CreateNewEntity();
                result = (person.Id != null);
            }
            else
            {
                result = await UpdateEntity();
            }
            return result;
        }
    }
}
