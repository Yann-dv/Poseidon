using System.ComponentModel.DataAnnotations;

namespace PoseidonApi.Models;


public class LoginModel
{
    /// <example>johndoe</example>
    [Required(ErrorMessage = "Email Required")]
    public string UserName { get; set; }

    /// <example>johndoe790</example>
    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; }
}
