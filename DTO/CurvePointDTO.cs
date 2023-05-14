namespace PoseidonApi.DTO;

public class CurvePointDTO
{
    /// <example>0</example>
    public long Id { get; set; }
    
    /// <example>233</example>
    public int CurveId { get; set; }
    
    /// <example>0001-01-01</example>
    public DateTime AsOfDate { get; set; }
    
    /// <example>10.0</example>
    public double Term { get; set; }
    
    /// <example>100.0</example>
    public double Value { get; set; }
    
    /// <example>0001-01-01</example>
    public DateTime CreationDate { get; set; }
}