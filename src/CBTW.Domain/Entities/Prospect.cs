namespace CBTW.Domain.Entities;

/// <summary>
/// Represents a prospect (table: prospects).
/// </summary>
public class Prospect
{
    public int DrawId { get; set; }
    public int Position { get; set; }
    public string ProspectType { get; set; } = default!;
    public int N1 { get; set; }
    public int N2 { get; set; }
    public int N3 { get; set; }
    public int N4 { get; set; }
    public int N5 { get; set; }
    public decimal? Weight { get; set; }
}


