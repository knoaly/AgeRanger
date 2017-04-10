using AgeRangerBO.Models;
using AgeRangerWebApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgeRangerWebApi.Controllers
{
    public class PersonController : ApiController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            if (personService == null) throw new ArgumentNullException("personService");

            _personService = personService;
        }

        [HttpGet]
        [Route("api/person")]
        public IHttpActionResult GetAll()
        {
            IEnumerable<Person> value = _personService.GetAll();
            return Ok(value);
        }

        [HttpGet]
        [Route("api/person/{id}")]
        public IHttpActionResult Get(long id)
        {
            Person value = _personService.GetSingle(id);
            return Ok(value);
        }

        [HttpGet]
        [Route("api/person/byname/{name}")]
        public IHttpActionResult Get(string name)
        {
            IEnumerable<Person> value = _personService.GetByName(name);
            return Ok(value);
        }

        [HttpPost]
        [Route("api/person")]
        public IHttpActionResult Post([FromBody]Person value)
        {
            if (value == null)
                return BadRequest("Body cannot be empty, do you have a malformed Person json?");

            Person newValue = _personService.Insert(value);
            string url = Request == null ? "" : Request.RequestUri.ToString() + "/";
            url += newValue.Id;
            return Created(url, newValue);
        }

        [HttpPut]
        [Route("api/person")]
        public IHttpActionResult Put([FromBody]Person value)
        {
            if (value == null)
                return BadRequest("Body cannot be empty, do you have a malformed Person json?");

            var retValue = _personService.Update(value);
            if (retValue == 0) return NotFound();
            return Ok("Person updated successfully.");
        }

        [HttpDelete]
        [Route("api/person/{id}")]
        public IHttpActionResult Delete(long id)
        {
            var retValue = _personService.Delete(id);
            if (retValue == 0) return NotFound();
            return Ok("Person deleted successfully.");
        }
    }
}
