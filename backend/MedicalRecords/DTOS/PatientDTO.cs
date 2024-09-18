namespace MedicalRecords.DTOS;

public record class PatientDTO(int Id, string? FirstName, string? LastName, DateTime BirthDate, string? Gender);
