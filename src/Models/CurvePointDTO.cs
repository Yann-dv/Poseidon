namespace PoseidonApi.Models;

public class CurvePointDTO
{
    /// <example>1</example>
    public long Id { get; set; }
    
    /// <example>233</example>
    public int CurveId { get; set; }
    
    /// <example>2023-03-01</example>
    public DateTime AsOfDate { get; set; }
    
    /// <example>10.0</example>
    public double Term { get; set; }
    
    /// <example>100.0</example>
    public double Value { get; set; }
    
    /// <example>2023-02-20</example>
    public DateTime CreationDate { get; set; }
}