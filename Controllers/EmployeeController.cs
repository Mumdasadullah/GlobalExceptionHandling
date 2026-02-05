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

        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO request)
        {
            var response = await _service.AddEmployee(request);
            return Ok(new ApiResponse { Message = "Employee Added Successfully", Data = response });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var response = await _service.DeleteEmployee(id);
            return Ok(new ApiResponse { Message = "Employee Deleted Successfully", Data = response });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _service.GetEmployees();
            return Ok(new ApiResponse { Message = "Employees Fetched Successfully", Data = response });
        }

        //[HttpGet("all")]
        //public IActionResult GetEmployees()
        //{
        //    var response = _service.GetEmployees();
        //    return Ok(new ApiResponse { Message = "Employees Fetched Successfully", Data = response });
        //}

        //[HttpGet("get-by-id")]
        //public IActionResult GetEmployeeById(int Id)
        //{
        //    var response = _service.GetEmployee(Id);
        //    return Ok(new ApiResponse { Message = "Employee Fetched Successfully", Data = response });
        //}

        //[HttpPost("add")]
        //public IActionResult AddEmployeeAndCountry()
        //{
        //    var response = _service.EmployeeAndCountryAdd();
        //    return Ok(new ApiResponse { Message = "Employee and Country Added Successfully", Data = response });
        //}

        //[HttpPut("edit")]
        //public IActionResult UpdateEmployee()
        //{
        //    var response = _service.EmployeeUpdate();
        //    return Ok(new ApiResponse { Message = "Employee Updated Successfully", Data = response });
        //}

        //[HttpDelete("remove")]
        //public IActionResult RemoveEmployee()
        //{
        //    var response = _service.EmployeeDelete();
        //    return Ok(new ApiResponse { Message = "Employee Deleted Successfully", Data = response });
        //}
    }
}
