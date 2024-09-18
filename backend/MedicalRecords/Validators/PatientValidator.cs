using System;
using MedicalRecords.DTOS;
using MedicalRecords.Models;

namespace MedicalRecords.Validators;

public static class PatientValidator
{
    public static bool IsValid(PatientDTO patient)
    {
        if (patient.Id == 0 || patient.FirstName == null || patient.FirstName == "" || patient.FirstName.Length > 60 ||
            patient.LastName == null || patient.LastName == "" || patient.LastName.Length > 60 ||
            patient.Gender == null || patient.Gender == "" || patient.Gender.Length > 10)
            {
                return false;
            }
        
        return true;
    }
}
