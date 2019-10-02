using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdventureWorks.Models;
using Microsoft.Extensions.Primitives;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
	public class CustomersController : Controller
    {
        public AdventureWorksContext DbContext { get; set; }
        public CustomersController(AdventureWorksContext dbContext)
        {
            DbContext = dbContext;
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var data = DbContext.Customer.AsQueryable()
								.Include(c => c.CustomerAddress)
								.Select(c => new {
									c.FirstName,
									c.LastName,
									c.Phone,
									c.SalesPerson,
									c.Suffix,
									CustomerAddress =
										c.CustomerAddress.Select(a
										=> new {
											a.Rowguid,
											a.Address.AddressLine1,
											a.Address.AddressLine2,
											a.Address.City,
											a.ModifiedDate,
											a.AddressType
										}
									)
								})
								.ToArray();

            var result = new ObjectResult(data)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Response.Headers.Add("X-Total-Count", new StringValues(data.Count().ToString()));
            Response.Headers.Add("page-size", new StringValues(5.ToString()));

            return result;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
