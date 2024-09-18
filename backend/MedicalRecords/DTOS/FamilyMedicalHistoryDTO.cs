namespace MedicalRecords.DTOS;

public record class FamilyMedicalHistoryDTO(string? FamilyMemberName, string? Relationship, string? MedicalCondition, string? Treatment, string? Notes, int PatientId);