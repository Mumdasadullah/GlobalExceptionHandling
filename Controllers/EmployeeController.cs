using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDBSpecificationRepositoryUOWProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetEmployees()
        {
            var response = _service.GetEmployees();
            return Ok(new ApiResponse { Message = "Employees Fetched Successfully", Data = response });
        }

        [HttpGet("get-by-id")]
        public IActionResult GetEmployeeById(int Id)
        {
            var response = _service.GetEmployee(Id);
            return Ok(new ApiResponse { Message = "Employee Fetched Successfully", Data = response });
        }
    }
}
