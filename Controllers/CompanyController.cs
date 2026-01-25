using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDBSpecificationRepositoryUOWProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> InsertCompany(AddCompanyDTO company)
        {
            var result = await _service.AddCompany(company);
            return Ok(new ApiResponse { Message = "Company Added Successfully", Data = result });
        }
    }
}
