using System;
using MedicalRecords.DTOS;

namespace MedicalRecords.Validators;

public static class MedicalRecordValidator
{
    public static bool IsValid(MedicalRecordDTO record)
    {
        if (record.Symptoms?.Length > 300 || record.Diagnosis?.Length > 300 || record.Studies?.Length > 300 ||
            record.Treatment?.Length > 300 || record.DoctorId == 0 || record.PatientId == 0)
            {
                return false;
            }
        
        return true;
    }
}
