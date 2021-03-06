﻿using System.Collections.Generic;
using System.Web.Http;
using MongoEngine.Repositories;
using WebApi.DbModels;

namespace WebApi.Controllers
{
    [Route("api/departmets")]
    public class DepartmentsController : ApiController
    {
        private readonly GenericMongoRepository<Department> _repository = new GenericMongoRepository<Department>();

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Department> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("get/{id}")]
        public Department GetDepartment(string id)
        {
            var dept = _repository.Get(id);
            if (dept == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return dept;
        }
        
        public IHttpActionResult Post(Department dept)
        {
            if (dept == null)
            {
                return BadRequest("Argument Null");
            }
            var deptExists = _repository.Get(dept.Id);

            if (deptExists != null)
            {
                return BadRequest("Exists");
            }

            _repository.Add(dept);
            return Ok();
        }
        
        public IHttpActionResult Put(Department dept)
        {
            if (dept == null)
            {
                return BadRequest("Argument Null");
            }
            var existing = _repository.Get(dept.Id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = dept.Name;
            return Ok();
        }
        
        public IHttpActionResult Delete(string id)
        {
            var dept = _repository.Get(id);
            if (dept == null)
            {
                return NotFound();
            }
            _repository.Remove(id);
            return Ok();
        }
    }
}
