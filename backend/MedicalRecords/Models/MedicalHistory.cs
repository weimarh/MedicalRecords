using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecords.Models;

public class MedicalHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(300)]
    public string? Medications { get; set; }

    [StringLength(100)]
    public string? Allergies { get; set; }
    
    [StringLength(100)]
    public string? Conditions { get; set; }

    [StringLength(100)]
    public string? Surgeries { get; set; }

    [StringLength(100)]
    public string? ControlledSubstances { get; set; }


    public int PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient? Patient { get; set; }
}
