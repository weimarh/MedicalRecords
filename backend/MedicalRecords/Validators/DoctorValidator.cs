using System;
using MedicalRecords.DTOS;

namespace MedicalRecords.Validators;

public static class DoctorValidator
{
    public static bool IsValid(DoctorDTO doctor)
    {
        if (doctor.FullName?.Length > 60 || doctor.FullName?.Length < 4 
            || doctor.FullName == null || doctor.FullName == "" || doctor.MedicalFacilityId == 0)
        {
            return false;
        }
        return true;
    }
}
