using System.ComponentModel.DataAnnotations;
using System.Net;
using AralytiksTest2.Contracts;
using AralytiksTest2.DTO;
using AralytiksTest2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AralytiksTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => 
            this._userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return Problem("", "", (int)HttpStatusCode.BadRequest, "Page and pageSize must be positive numbers.", "https://tools.ietf.org/html/rfc9110#section-15.5.1");

            var (users, totalCount) = await _userService.GetAllUsersAsync(page, pageSize);
            var response = new PaginatedResponse<User>
            {
                Items = users,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] [Required] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"User with Id = {id} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] [Required] int id, [FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"User with Id = {id} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return Problem("", "", (int)HttpStatusCode.NotFound, $"User with Id = {id} not found.", "https://tools.ietf.org/html/rfc9110#section-15.5.5");

            return NoContent();
        }
    }
}
