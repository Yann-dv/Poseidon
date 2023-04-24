namespace PoseidonApi.Models;

public class BidDTO
{
    /// <example>0</example>
    public long Id { get; set; }
    
    /// <example>AccountSample</example>
    public string Account { get; set; }
    
    /// <example>TypeSample</example>
    public string Type { get; set; }
    
    /// <example>23.0</example>
    public double BidQuantity { get; set; }
    
    /// <example>10.0</example>
    public double AskQuantity { get; set; }
    
    /// <example>15.0</example>
    public double BidValue { get; set; }
    
    /// <example>2.0</example>
    public double Ask { get; set; }
    
    /// <example>BenchmarkSample</example>
    public string Benchmark { get; set; }
    
    /// <example>2023-03-31</example>
    public DateTime BidListDate { get; set; }
    
    /// <example>CommentarySample</example>
    public string Commentary { get; set; }
    
    /// <example>SecuritySample</example>
    public string Security { get; set; }
    
    /// <example>StatusSample</example>
    public string Status { get; set; }
    
    /// <example>TraderSample</example>
    public string Trader { get; set; }
    
    /// <example>BookSample</example>
    public string Book { get; set; }
    
    /// <example>CreationNameSample</example>
    public string CreationName { get; set; }
    
    /// <example>2023-03-01</example>
    public DateTime CreationDate { get; set; }
    
    /// <example>RevisionNameSample</example>
    public string RevisionName { get; set; }
    
    /// <example>2023-03-11</example>
    public DateTime RevisionDate { get; set; }
    
    /// <example>DealNameSample</example>
    public string DealName { get; set; }
    
    /// <example>DealTypeSample</example>
    public string DealType { get; set; }
    
    /// <example>SourceListIdSample</example>
    public string SourceListId { get; set; }
    
    /// <example>SideSample</example>
    public string Side { get; set; }
}