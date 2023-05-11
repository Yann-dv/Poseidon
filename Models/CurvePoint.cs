namespace PoseidonApi.Models;

public class CurvePoint
{
    public long Id { get; set; }
    public int CurveId { get; set; }
    public DateTime AsOfDate { get; set; }
    public double Term { get; set; }
    public double Value { get; set; }
    public DateTime CreationDate { get; set; }
}