using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class Bostad
{

    [Key]
    public required string BostadId { get; set; } 

    public string? Adress { get; set; }

    public string? InterntNr { get; set; }

    public string? ExternNr { get; set; }

    public long? Rum { get; set; }

    public virtual ICollection<Medlem> medlemmar { get; } = new List<Medlem>();
}
