using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDBSpecificationRepositoryUOWProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> InserUser(AddUserDTO user)
        {
            bool response = await _service.AddUser(user);
            return Ok(new ApiResponse { Message = "User Added Successfully", Data = response });
        }
    }
}
