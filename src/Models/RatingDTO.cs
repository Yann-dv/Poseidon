namespace PoseidonApi.Models;

public class RatingDTO
{
    /// <example>1</example>
    public long Id { get; set; }
    
    /// <example>MoodyRatingSample</example>
    public string MoodysRating { get; set; }
    
    /// <example>SandPRatingSample</example>
    public string SandPRating { get; set; }
    
    /// <example>FitchRatinSample</example>
    public string FitchRating { get; set; }
    
    /// <example>92134</example>
    public int OrderNumber { get; set; }
}