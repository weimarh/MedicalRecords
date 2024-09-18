using MedicalRecords.Models;

namespace MedicalRecords.DTOS;

public record class MedicalRecordDTO(DateTime VisitDate, string? Symptoms, double Height, 
    double Weight, string? Diagnosis, string? Studies, string? Treatment, int DoctorId, int PatientId,
    int DoctorScore);
