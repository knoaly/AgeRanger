using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.Managers
{
    public abstract class EntityManagerBase<Entity> : IEntityManager<Entity>
    {
        public Dictionary<string, string> ValidationErrors { get; set; }

        protected readonly HttpClient _client;

        protected readonly string _apiPath;

        public EntityManagerBase(string webServiceUrl, string apiPath)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(webServiceUrl)
            };
            _apiPath = apiPath + (apiPath.EndsWith("/") ? "" : "/");
        }

        public virtual async Task<IEnumerable<Entity>> GetAll()
        {
            IEnumerable<Entity> entities = null;
            string path = _apiPath;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                entities = await response.Content.ReadAsAsync<IEnumerable<Entity>>();
            }
            return entities;
        }

        public virtual async Task<Entity> GetSingle(long id)
        {
            string path = _apiPath + id;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Entity entity = await response.Content.ReadAsAsync<Entity>().ConfigureAwait(false);
                return entity;
            }
            return default(Entity);
        }

        public virtual async Task<Entity> Insert(Entity entity)
        {
            string path = _apiPath;
            HttpResponseMessage response = await _client.PostAsJsonAsync(path, entity);
            if (response.IsSuccessStatusCode)
            {
                Entity newEntity = await response.Content.ReadAsAsync<Entity>().ConfigureAwait(false);
                return newEntity;
            }
            return default(Entity);
        }

        public virtual async Task<string> Update(Entity entity)
        {
            string result = "";
            string path = _apiPath;
            HttpResponseMessage response = await _client.PutAsJsonAsync(path, entity);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            return result;
        }

        public virtual async Task<string> Delete(long id)
        {
            string result = "";
            string path = _apiPath + id;
            HttpResponseMessage response = await _client.DeleteAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            return result;
        }

    }
}
