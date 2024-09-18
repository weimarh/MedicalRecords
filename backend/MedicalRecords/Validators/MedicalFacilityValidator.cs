using System;
using MedicalRecords.DTOS;

namespace MedicalRecords.Validators;

public static class MedicalFacilityValidator
{
    public static bool IsValid(MedicalFacilityDTO facility)
    {
        if (facility.FacilityName == null || facility.FacilityName == ""
            || facility.FacilityName.Length > 40 
            || facility.Address == null || facility.Address == ""
            || facility.Address.Length > 40)
            {
                return false;
            }
        
        return true;
    }
}
