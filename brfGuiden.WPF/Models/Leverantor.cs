using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class Leverantor
{
    [Key]
    [Required(ErrorMessage = "Fältet ID är obligatoriskt.")]
    [ConcurrencyCheck]
    public required string LeverantorId { get; set; }

    [Required(ErrorMessage = "Leverantörens namn är obligatoriskt.")]
    [StringLength(40, ErrorMessage = "Namnet får vara högst 40 tecken.")]
    public string? Namn { get; set; }
    [Required(ErrorMessage = "OrgNr är obligatoriskt.")]
    [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "Felaktigt format i OrgNr.")]
    public string? OrgNr { get; set; }

    [StringLength(40, ErrorMessage = "Adress får vara högst 40 tecken.")]
    public string? Adress { get; set; }

    public string? Telefon { get; set; }

    [RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "Felaktigt format i PostNr.")]
    [MaxLength(6, ErrorMessage = "Max antal tecken är 6")]
    [MinLength(5, ErrorMessage = "Fel antal tecken i PostNr")]
    public string? PostNr { get; set; }

    public string? PostOrt { get; set; }

    public int? Betyg { get; set; }

    public string? Hemsida { get; set; }
}
