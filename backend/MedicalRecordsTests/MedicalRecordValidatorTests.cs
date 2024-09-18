using System;
using MedicalRecords.Models;
using MedicalRecords.Utils;
using MedicalRecords.Validators;

namespace MedicalRecordsTests;

[TestClass]
public class MedicalRecordValidatorTests
{
    MedicalRecord record = new MedicalRecord();

    [TestMethod]
    public void IsValid_ValidData_ReturnsTrue()
    {
        // Arrange
        record.Symptoms = "Sympthomps";
        record.Diagnosis = "Diagnosis";
        record.Studies = "Studies";
        record.Treatment = "Treatment";
        record.DoctorId = 1;
        record.PatientId = 1;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_SymptomsTooLong_ReturnsFalse()
    {
        // Arrange
        record.Symptoms = new string('a', 1001);
        record.Diagnosis = "Diagnosis";
        record.Studies = "Studies";
        record.Treatment = "Treatment";
        record.DoctorId = 1;
        record.PatientId = 1;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_DiagnosisTooLong_ReturnsFalse()
    {
        // Arrange
        record.Symptoms = "Symptoms";
        record.Diagnosis = new string('a', 1001);
        record.Studies = "Studies";
        record.Treatment = "Treatment";
        record.DoctorId = 1;
        record.PatientId = 1;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    public void IsValid_StudiesTooLong_ReturnsFalse()
    {
        // Arrange
        record.Symptoms = "Symptoms";
        record.Diagnosis = "Diagnosis";
        record.Studies = new string('a', 1001);
        record.Treatment = "Treatment";
        record.DoctorId = 1;
        record.PatientId = 1;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    public void IsValid_TreatmentTooLong_ReturnsFalse()
    {
        // Arrange
        record.Symptoms = "Symptoms";
        record.Diagnosis = "Diagnosis";
        record.Studies = "Studies";
        record.Treatment = new string('a', 1001);
        record.DoctorId = 1;
        record.PatientId = 1;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    public void IsValid_DoctorIdIsZero_ReturnsFalse()
    {
        // Arrange
        record.Symptoms = "Symptoms";
        record.Diagnosis = "Diagnosis";
        record.Studies = "Studies";
        record.Treatment = "Treatment";
        record.DoctorId = 0;
        record.PatientId = 1;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    public void IsValid_PatientIdIsZero_ReturnsFalse()
    {
        // Arrange
        record.Symptoms = "Symptoms";
        record.Diagnosis = "Diagnosis";
        record.Studies = "Studies";
        record.Treatment = "Treatment";
        record.DoctorId = 1;
        record.PatientId = 0;

        // Act
        var result = MedicalRecordValidator.IsValid(record.AsDto());

        // Assert
        Assert.IsFalse(result);
    }
}
