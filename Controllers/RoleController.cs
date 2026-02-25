using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDBSpecificationRepositoryUOWProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleDTO role)
        {
            bool response = await _service.AddRole(role);
            return Ok(new ApiResponse { Message = "Role Added Successfully", Data = response });
        }
    }
}
