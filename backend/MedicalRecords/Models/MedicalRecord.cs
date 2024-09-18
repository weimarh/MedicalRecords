using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecords.Models;

public class MedicalRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime VisitDate { get; set; }

    [StringLength(300)]
    public string? Symptoms { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    [StringLength(300)]
    public string? Diagnosis { get; set; }

    [StringLength(300)]
    public string? Studies { get; set; }

    [StringLength(300)]
    public string? Treatment { get; set; }

    public int DoctorScore { get; set; }

    
    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor? Doctor { get; set; }
    

    public int PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient? Patient { get; set; }
}
