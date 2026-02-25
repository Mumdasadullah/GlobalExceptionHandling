using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDBSpecificationRepositoryUOWProject.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> AddUser(RegisterUserDTO register)
        {
            bool response = await _service.RegisterUser(register);
            return Ok(new ApiResponse { Message = "User Registered Successfully", Data = response });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequestDTO request)
        {
            string response = await _service.LoginUser(request);
            return Ok(new ApiResponse { Message = "Login Successful", Data = response });
        }

        [HttpGet("check-token")]
        public IActionResult CheckToken()
        {
            return Ok(new ApiResponse { Message = "Token is valid", Data = true });
        }

        [HttpPost("AssignRoles")]
        public async Task<IActionResult> AssignUsers(AssignUserRoleDTO userRoles)
        {
            bool response = await _service.AssignRoles(userRoles);
            return Ok(new ApiResponse { Message = "Roles Assigned Successfully", Data = response });
        }
    }
}
