using System.ComponentModel.DataAnnotations;

namespace PoseidonApi.Models;


public class LoginModel
{
    /// <example>johndoe</example>
    [Required(ErrorMessage = "Email Required")]
    public string UserName { get; set; }

    /// <example>John_doe99$</example>
    [Required(ErrorMessage = "Password Required")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", 
        ErrorMessage = "Password must meet requirements")]
    public string Password { get; set; }
}
