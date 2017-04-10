using AgeRangerBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.Managers
{
    public class PeopleManager : EntityManagerBase<Person>, IPeopleManager
    {
        private const string ApiPath = "api/Person/";

        public PeopleManager(string webServiceUrl) : base(webServiceUrl, ApiPath)
        {
        }
        
        public async Task<IEnumerable<Person>> GetByName(string name)
        {
            IEnumerable<Person> persons = null;
            string path = ApiPath + (string.IsNullOrEmpty(name) ? "" : "byname/" + name);
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                persons = await response.Content.ReadAsAsync<IEnumerable<Person>>();
            }
            return persons;
        }        
    }
}
