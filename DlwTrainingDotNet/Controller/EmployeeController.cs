using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DlwTrainingDotNet.Models;
using DlwTrainingDotNet.Resources;
using DlwTrainingDotNet.Resources.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DlwTrainingDotNet.Controller
{
    [ApiController]
    [Route("api/[controller]")] 
    public class EmployeeController :ControllerBase
    {
        private readonly IEmployeeResource _employeeResource;

        public EmployeeController(IEmployeeResource employeeResource)
        {
            _employeeResource = employeeResource;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _employeeResource.GetAll();
            return employees.Select(employeeEntity => new Employee(employeeEntity.FirstName, employeeEntity.LastName));
        }

        [HttpPost]
        public void Create([FromBody] Employee employee)
        {
           _employeeResource.Add();
           //await _employeeResource.Delete(15);
        }


        [HttpDelete("{id:int}")]
        public async Task Delete([FromRoute] int id)
        {
           await _employeeResource.Delete(id);
        }
    }
}