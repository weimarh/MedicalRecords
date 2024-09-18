using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecords.Models;

public class FamilyMedicalHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string? FamilyMemberName { get; set; }

    [Required]
    [StringLength(20)]
    public string? Relationship { get; set; }

    [StringLength(60)]
    public string? MedicalCondition { get; set; }

    [StringLength(60)]
    public string? Treatment { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }
    

    public int PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient? Patient { get; set; }    
}
