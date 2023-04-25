using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PoseidonApi.Models;

namespace PoseidonApi.Controllers;

/// <summary>
/// Login controller.
/// </summary>
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid client request");
        }

        if (model.UserName == "johndoe" && model.Password == "John_doe99$")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@2410"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "CodeMaze",
                audience: "https://localhost:5001",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
    }
}
