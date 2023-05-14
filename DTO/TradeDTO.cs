namespace PoseidonApi.DTO;

public class TradeDTO
{
    /// <example>0</example>
    public long Id { get; set; }
    
    /// <example>Account</example>
    public string Account { get; set; }
    
    /// <example>Type</example>
    public string Type { get; set; }
    
    /// <example>0</example>
    public double BuyQuantity { get; set; }
    
    /// <example>0</example>
    public double SellQuantity { get; set; }
    
    /// <example>0</example>
    public double BuyPrice { get; set; }
    
    /// <example>0</example>
    public double SellPrice { get; set; }
    
    /// <example>Benchmark</example>
    public string Benchmark { get; set; }
    
    /// <example>0001-01-01</example>
    public DateTime TradeDate { get; set; }
    
    /// <example>Security</example>
    public string Security { get; set; }
    
    /// <example>Status</example>
    public string Status { get; set; }
    
    /// <example>Trader</example>
    public string Trader { get; set; }
    
    /// <example>Book</example>
    public string Book { get; set; }
    
    /// <example>CreationName</example>
    public string CreationName { get; set; }
    
    /// <example>0001-01-01</example>
    public DateTime CreationDate { get; set; }
    
    /// <example>RevisionName</example>
    public string RevisionName { get; set; }
    
    /// <example>0001-01-01</example>
    public DateTime RevisionDate { get; set; }
    
    /// <example>DealName</example>
    public string DealName { get; set; }
    
    /// <example>DealType</example>
    public string DealType { get; set; }
    
    /// <example>SourceListId</example>
    public string SourceListId { get; set; }
    
    /// <example>Side</example>
    public string Side { get; set; }
}