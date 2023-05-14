using System.ComponentModel.DataAnnotations;

namespace PoseidonApi.DTO;

public class UserDTO
{
    /// <example>0</example>
    public long Id  { get; set; }
    
    /// <example>JohnDoe83</example>
    public string Username  { get; set; }
    
    /// <example>John Doe</example>
    public string? Fullname  { get; set; }
    
    /// <example>sample_Pwd01*</example>
    [Required(ErrorMessage = "Password Required")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,64}$", 
        ErrorMessage = "Password must meet requirements")]
    public string Password  { get; set; }
    
    /// <example>Employee</example>
    public string? Role  { get; set; }
}