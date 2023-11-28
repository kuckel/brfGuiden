using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class StyrelseMedlem
{

    [Key]
    public required string StyrelseMedlemId { get; set; }

    public string? Namn { get; set; }

    public string? Epost { get; set; }

    public string? Roll { get; set; }
}
