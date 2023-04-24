namespace PoseidonApi.Models;

public class RuleDTO
{
    /// <example>1</example>
    public long Id { get; internal set; }
    
    /// <example>RuleNameSample</example>
    public string Name { get; set; }
    
    /// <example>DescriptionSample</example>
    public string Description { get; set; }
    
    /// <example>JsonSample</example>
    public string Json { get; set; }
    
    /// <example>TemplateSample</example>
    public string Template { get; set; }
    
    /// <example>SqlStrSample</example>
    public string SqlStr { get; set; }
    
    /// <example>SqlPartSample</example>
    public string SqlPart { get; set; }
}