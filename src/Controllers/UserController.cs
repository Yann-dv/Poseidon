using Microsoft.AspNetCore.Authorization;
using PoseidonApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace PoseidonApi.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        /// <inheritdoc />
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
        /// <param name="id"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Fullname = user.Fullname != userDto.Fullname ? userDto.Fullname : user.Fullname;
            user.Password = user.Password != userDto.Password ? userDto.Password : user.Password;
            user.Username = user.Username != userDto.Username ? userDto.Username : user.Username;
            user.Role = user.Role != userDto.Role ? userDto.Role : user.Role;

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
        /// <returns>A newly created User</returns>
        /// <remarks>
        /// POST request:
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
                Username = userDto.Username.IsNullOrEmpty() ?  "EmptyUsername" : userDto.Username,
                Fullname = userDto.Fullname.IsNullOrEmpty() ? "EmptyFullname" : userDto.Fullname,
                Password = userDto.Password.IsNullOrEmpty() ? "EmptyPassword" : userDto.Password,
                Role = userDto.Role.IsNullOrEmpty() ? "Employee" : userDto.Role
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
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
                Password = user.Password,
                Role = user.Role
            };
    }
}