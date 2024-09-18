using System;
using MedicalRecords.DTOS;

namespace MedicalRecords.Validators;

public static class MedicalHistoryValidator
{
    public static bool IsValid(MedicalHistoryDTO history)
    {
        if (history.Medications?.Length > 300 ||
            history.Allergies?.Length > 100 ||
            history.Conditions?.Length > 100 ||
            history.Surgeries?.Length > 100 ||
            history.ControlledSubstances?.Length > 100 ||
            history.PatientId == 0)
        {
            return false;
        } 
        return true;
    }
}
