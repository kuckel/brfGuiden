using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class Medlem
{

    [Key]
    public required string MedlemId { get; set; } 

    public string? Namn { get; set; }

    public string? MedlemNr { get; set; }

    public virtual ICollection<Bostad> bostad { get; } = new List<Bostad>();
}
