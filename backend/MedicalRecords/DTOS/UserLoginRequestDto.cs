using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.DTOS;

public class UserLoginRequestDto
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
