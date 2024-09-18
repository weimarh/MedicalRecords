using System;
using MedicalRecords.DTOS;

namespace MedicalRecords.Validators;

public static class FamilyMedicalHistoryValidator
{
    public static bool IsValid(FamilyMedicalHistoryDTO history)
    {
        if (history.FamilyMemberName == null || history.FamilyMemberName == ""
            || history.Relationship == null || history.Relationship == ""
            || history.MedicalCondition?.Length > 60 || history.Treatment?.Length > 60
            || history.Notes?.Length > 100 || history.PatientId == 0)
            {
                return false;
            }
        
        return true;
    }
}
