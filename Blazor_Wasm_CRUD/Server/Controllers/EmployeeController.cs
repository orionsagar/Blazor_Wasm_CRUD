﻿using Blazor_Wasm_CRUD.Server.Interfaces;
using Blazor_Wasm_CRUD.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor_Wasm_CRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            return await Task.FromResult(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = _employeeService.GetEmployeeData(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public void Post(Employee employee)
        {
            _employeeService.AddEmployee(employee);
        }

        [HttpPut]
        public void Put(Employee employee)
        {
            _employeeService.UpdateEmployee(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpGet]
        [Route("GetCityList")]
        public async Task<IEnumerable<City>> CityList()
        {
            return await Task.FromResult(_employeeService.GetCity());
        }
    }
}
