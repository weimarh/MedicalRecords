using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecords.Models;

public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(60)]
    public string? LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [StringLength(10)]
    public string? Gender { get; set; }
}
