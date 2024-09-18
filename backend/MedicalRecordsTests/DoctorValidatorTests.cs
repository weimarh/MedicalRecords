using System;
using MedicalRecords.Models;
using MedicalRecords.Utils;
using MedicalRecords.Validators;

namespace MedicalRecordsTests;

[TestClass]
public class DoctorValidatorTests
{
    Doctor doctor = new Doctor();
    MedicalFacility facility = new MedicalFacility()
    {
        Id = 1,
        FacilityName = "facility test",
        Address = "address test",
    };

    [TestMethod]
    public void IsValid_ValidData_ReturnsTrue()
    {
        doctor.FullName = "John Doe";
        doctor.Specialization = "Cardiology";
        doctor.Facility = facility;
        doctor.MedicalFacilityId = facility.Id;

        Assert.IsTrue(DoctorValidator.IsValid(doctor.AsDto()));
    }

    [TestMethod]
    public void IsValid_FullNameTooShort_ReturnsFalse()
    {
        doctor.FullName = "Jo";
        doctor.Specialization = "Cardiology";
        doctor.Facility = facility;

        Assert.IsFalse(DoctorValidator.IsValid(doctor.AsDto()));
    }

    [TestMethod]
    public void IsValid_FullNameTooLong_ReturnsFalse()
    {
        doctor.FullName = "JonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonas";
        doctor.Specialization = "Cardiology";
        doctor.Facility = facility;

        Assert.IsFalse(DoctorValidator.IsValid(doctor.AsDto()));
    }

    [TestMethod]
    public void IsValid_FullNameIsNull_ReturnsFalse()
    {
        doctor.FullName = null;
        doctor.Specialization = "Cardiology";
        doctor.Facility = facility;

        Assert.IsFalse(DoctorValidator.IsValid(doctor.AsDto()));
    }

    [TestMethod]
    public void IsValid_FullNameEmptyString_ReturnsFalse()
    {
        doctor.FullName = "";
        doctor.Specialization = "Cardiology";
        doctor.Facility = facility;

        Assert.IsFalse(DoctorValidator.IsValid(doctor.AsDto()));
    }

    [TestMethod]
    public void IsValid_MedicalFacilityIdIsNull_ReturnsFalse()
    {
        doctor.FullName = "JonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonasJonas";
        doctor.Specialization = "Cardiology";
        doctor.MedicalFacilityId = 0;

        Assert.IsFalse(DoctorValidator.IsValid(doctor.AsDto()));
    }
}
