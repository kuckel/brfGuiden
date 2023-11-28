using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class Leverantor
{
    [Key]
    public required string LeverantorId { get; set; } 

    public string? Namn { get; set; }

    public string? OrgNr { get; set; }

    public string? Adress { get; set; }

    public string? Telefon { get; set; }

    public string? PostNr { get; set; }

    public string? PostOrt { get; set; }

    public int? Betyg { get; set; }

    public string? Hemsida { get; set; }
}
