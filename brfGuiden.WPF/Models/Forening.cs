using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class Forening
{

    [Key]
    public required string ForeningId { get; set; } = null!;

    public string? Namn { get; set; }

    public string? OrgNr { get; set; }

    public string? Adress { get; set; }

    public string? PostNr { get; set; }

    public string? PostOrt { get; set; }
}
