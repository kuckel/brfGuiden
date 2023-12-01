using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brfGuiden.Models;

public partial class Forening
{

    [Key]
    [Required(ErrorMessage = "Fältet ID är obligatoriskt.")]
    [ConcurrencyCheck]
    public string ForeningId { get; set; } = null!;


    [Required(ErrorMessage = "Föreningens namn är obligatoriskt.")]
    [StringLength(40, ErrorMessage = "Namnet får vara högst 40 tecken.")]
    public string? Namn { get; set; }


    [Required(ErrorMessage = "OrgNr är obligatoriskt.")]
    [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "Felaktigt format i OrgNr.")]
    public string? OrgNr { get; set; }


    [MaxLength(40, ErrorMessage = "Max antal tecken i adress är 40")]
    public string? Adress { get; set; }

   
    [RegularExpression(@"^\d{3} \d{2}$", ErrorMessage = "Felaktigt format i PostNr.")]
    [MaxLength(6, ErrorMessage = "Max antal tecken är 6")]
    [MinLength(5, ErrorMessage = "Fel antal tecken i PostNr")]
    public string? PostNr { get; set; }


    [MaxLength(40, ErrorMessage = "Max antal tecken i postort är 40")]
    public string? PostOrt { get; set; }

    [MaxLength(40, ErrorMessage = "Max antal tecken i hemsida är 40")]
    public string? Hemsida{ get; set; }
}
