namespace PoseidonApi.Models;

public class UserDTO
{
    /// <example>0</example>
    public long Id  { get; set; }
    
    /// <example>JohnDoe83</example>
    public string Username  { get; set; }
    
    /// <example>John Doe</example>
    public string? Fullname  { get; set; }
    
    /// <example>p@sswrd123</example>
    public string Password  { get; set; }
    
    /// <example>Employee</example>
    public string? Role  { get; set; }
}