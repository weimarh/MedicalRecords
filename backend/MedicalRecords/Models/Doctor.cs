using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecords.Models;

public class Doctor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string? FullName { get; set; }

    [StringLength(40)]
    public string? Specialization { get; set; }
    
    
    public int MedicalFacilityId { get; set; }

    [ForeignKey("MedicalFacilityId")]
    public MedicalFacility? Facility { get; set; }
}
