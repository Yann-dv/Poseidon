using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoseidonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace PoseidonApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public UserController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Users
        /// <summary>
        /// Get all Users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _dbContext.Users
                .Select(x => UserToDTO(x))
                .ToListAsync();
        }

        /// <summary>
        /// Get a specific User.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserToDTO(user);
        }

        /// <summary>
        /// Update a specific User.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserDTO userDto)
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
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns>A newly created User</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Id": (auto generated)
        ///        "Username": "SampleUsername",
        ///        "Fullname": "Sample Fullname",
        ///        "Password": "MyPassword123"?
        ///        "Role": "Employee"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDto)
        {
            var newUser = new User
            {
                Username = userDto.Username,
                Fullname = userDto.Fullname,
                Password = userDto.Password
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUser),
                new { id = newUser.Id },
                UserToDTO(newUser));
        }

        /// <summary>
        /// Deletes a specific User.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
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

        private bool UserExists(long id) =>
            _dbContext.Users.Any(e => e.Id == id);

        private static UserDTO UserToDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Fullname = user.Fullname,
                Password = user.Password
            };
    }
}