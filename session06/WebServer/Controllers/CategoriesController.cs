using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebServer.Data;

namespace WebServer.Controllers {
    
    [ApiController]
    [Route (ROUTE)]
    public class CategoriesController : Controller {
        public const string ROUTE = "/api/[Controller]";
        public static string CONTROLLER_NAME = "Categories";

        public CategoryRepository Repository { get; private set; }

        // Dependency Injection
        public CategoriesController (IHostingEnvironment environment, 
            IConfiguration configuration, CategoryRepository repository) 
        {
            this.Repository = repository;
        }

        [HttpGet]
        public ObjectResult Get ()
        {
            var data = Repository.Get().AsQueryable();

            var result = new ObjectResult(data)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Response.Headers.Add("X-Total-Count", new StringValues(data.Count().ToString()));
            Response.Headers.Add("page-size", new StringValues(data.Count().ToString()));

            return result;
        }


        [HttpGet("{id}", Name=nameof(GetById))]
        public ObjectResult GetById(int id)
        {
            var data = Repository.Get(id);

            var result = new ObjectResult(data)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return result;
        }
    }
}