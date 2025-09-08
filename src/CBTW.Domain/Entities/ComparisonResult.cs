using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CBTW.Domain.Entities;

/// <summary>
/// Represents the result of comparing a draw with a prospect.
/// </summary>
public class ComparisonResult
{
    public int DrawId { get; set; }
    public string DrawNumbers { get; set; } = default!;
    public string ProspectNumbers { get; set; } = default!;
    public int Hits { get; set; }
    public string? MatchedNumbers { get; set; }
}


