using MedicalRecords.Models;

namespace MedicalRecords.DTOS;

public record class DoctorDTO(string? FullName, string? Specialization, int MedicalFacilityId);
