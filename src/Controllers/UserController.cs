using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace PoseidonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public UserController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _dbContext.Users
                .Select(x => UserItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserItem(long id)
        {
            var userItem = await _dbContext.Users.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return UserItemToDTO(userItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserItem(long id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var userItem = await _dbContext.Users.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            userItem.Fullname = userDto.Fullname;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUserItem(UserDTO userDto)
        {
            var userItem = new User
            {
                Fullname = userDto.Fullname
            };

            _dbContext.Users.Add(userItem);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUserItem),
                new { id = userItem.Id },
                UserItemToDTO(userItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(long id)
        {
            var userItem = await _dbContext.Users.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(userItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserItemExists(long id) =>
            _dbContext.Users.Any(e => e.Id == id);

        private static UserDTO UserItemToDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                Fullname = user.Fullname,
            };
    }
}