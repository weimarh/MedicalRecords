using System;
using MedicalRecords.DTOS;
using MedicalRecords.Models;

namespace MedicalRecords.Utils;

public static class Extensions
{
    public static DoctorDTO AsDto(this Doctor doctor) => 
        new DoctorDTO(doctor.FullName, doctor.Specialization, doctor.MedicalFacilityId);

    public static FamilyMedicalHistoryDTO AsDto(this FamilyMedicalHistory history) => 
        new FamilyMedicalHistoryDTO(history.FamilyMemberName, history.Relationship,
        history.MedicalCondition, history.Treatment, history.Notes, history.PatientId);

    public static MedicalFacilityDTO AsDto(this MedicalFacility facility) =>
        new MedicalFacilityDTO(facility.FacilityName, facility.Address);

    public static MedicalHistoryDTO AsDto(this MedicalHistory history) =>
        new MedicalHistoryDTO(history.Medications, history.Allergies, history.Conditions,
            history.Surgeries, history.ControlledSubstances, history.PatientId);

    public static MedicalRecordDTO AsDto(this MedicalRecord record) =>
        new MedicalRecordDTO(record.VisitDate, record.Symptoms, record.Height, 
            record.Weight, record.Diagnosis, record.Studies, record.Treatment, record.DoctorId,
            record.PatientId, record.DoctorScore);

    public static PatientDTO AsDto(this Patient patient) =>
        new PatientDTO(patient.Id, patient.FirstName, patient.LastName, patient.BirthDate,
        patient.Gender);
}
