namespace MedicalRecords.DTOS;

public record class MedicalHistoryDTO(string? Medications, string? Allergies, string? Conditions, string? Surgeries, string? ControlledSubstances, int PatientId);
