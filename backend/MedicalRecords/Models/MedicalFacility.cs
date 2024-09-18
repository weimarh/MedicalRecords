using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecords.Models;

public class MedicalFacility
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(40)]
    public string? FacilityName { get; set; }

    [Required]
    [StringLength(40)]
    public string? Address { get; set; }
}
