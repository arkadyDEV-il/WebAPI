using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        Data data;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
            data = Data.dataAccess;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var employees = data.GetAllEmployees();
            return new JsonResult(employees);
        }

        [HttpPost]
        public JsonResult Post(Employee em)
        {
            data.AddEmployee(em);
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee em)
        {
            var employees = data.GetAllEmployees();
            var employee = employees.Find(e => e.EmployeeId == em.EmployeeId);
            employee.Position = em.Position;
            employee.EmployeeName = em.EmployeeName;
            employee.PhotoFileName = em.PhotoFileName;

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var employees = data.GetAllEmployees();

            employees.Remove(employees.Find(e => e.EmployeeId == id));
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch(Exception)
            {
                return new JsonResult("error");
            }
        }      

        [HttpGet("GetPicture/{id}")]
        public IActionResult GetPicture(string id)
        {
            int ID = int.Parse(id);
            var filename = data.GetAllEmployees().Find(e => e.EmployeeId == ID).PhotoFileName;
            var path = _env.ContentRootPath + "/Photos/" + filename;
            return PhysicalFile(path, "image/jpeg");//http://localhost:5000/api/Employee/GetPicture/1
        }


        [HttpGet("GetEmployee/{n}")]
        public JsonResult GetEmployee(string n)
        {
            var employees = data.GetAllEmployees().FindAll(e => e.EmployeeName.Contains(n, StringComparison.OrdinalIgnoreCase) || e.Position.Contains(n, StringComparison.OrdinalIgnoreCase));
            return new JsonResult(employees); //http://localhost:5000/api/Employee/GetEmployee/Mike
        }


    }
}
