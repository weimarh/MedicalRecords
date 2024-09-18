using System;
using MedicalRecords.Models;
using MedicalRecords.Utils;
using MedicalRecords.Validators;

namespace MedicalRecordsTests;

[TestClass]
public class MedicalHistoryValidatorTests
{
    MedicalHistory history = new MedicalHistory();

    [TestMethod]
    public void IsValid_ValidData_ReturnsTrue()
    {
        //Arrange
        history.Medications = "Medications";
        history.Allergies = "Allergies";
        history.Conditions = "Conditions";
        history.Surgeries = "Surgeries";
        history.ControlledSubstances = "Controlled Substances";
        history.PatientId = 1;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());
    }

    [TestMethod]
    public void IsValid_MedicationsIsTooLong_ReturnsFalse()
    {
        //Arrange
        history.Medications = "Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, ";
        history.Allergies = "Allergies";
        history.Conditions = "Conditions";
        history.Surgeries = "Surgeries";
        history.ControlledSubstances = "Controlled Substances";
        history.PatientId = 1;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());

        //Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_AllergiesIsTooLong_ReturnsFalse()
    {
        //Arrange
        history.Medications = "Medications";
        history.Allergies = "Too Long, Too Long,Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long,  Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, ";
        history.Conditions = "Conditions";
        history.Surgeries = "Surgeries";
        history.ControlledSubstances = "Controlled Substances";
        history.PatientId = 1;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());

        //Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_ConditionsIsTooLong_ReturnsFalse()
    {
        //Arrange
        history.Medications = "Medications";
        history.Allergies = "Allergies";
        history.Conditions = "Too Long, Too Long,Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too ";
        history.Surgeries = "Surgeries";
        history.ControlledSubstances = "Controlled Substances";
        history.PatientId = 1;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());

        //Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_SurgeriesIsTooLong_ReturnsFalse()
    {
        //Arrange
        history.Medications = "Medications";
        history.Allergies = "Allergies";
        history.Conditions = "Conditions";
        history.Surgeries = "Too Long, Too Long,Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too ";
        history.ControlledSubstances = "Controlled Substances";
        history.PatientId = 1;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());

        //Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_ControlledSubstancesIsTooLong_ReturnsFalse()
    {
        //Arrange
        history.Medications = "Medications";
        history.Allergies = "Allergies";
        history.Conditions = "Conditions";
        history.Surgeries = "Surgeries";
        history.ControlledSubstances = "Too Long, Too Long,Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too Long, Too ";
        history.PatientId = 1;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());

        //Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_PatientIdIsZero_ReturnsFalse()
    {
        //Arrange
        history.Medications = "Medications";
        history.Allergies = "Allergies";
        history.Conditions = "Conditions";
        history.Surgeries = "Surgeries";
        history.ControlledSubstances = "Controlled Substances";
        history.PatientId = 0;

        //Act
        bool isValid = MedicalHistoryValidator.IsValid(history.AsDto());

        //Assert
        Assert.IsFalse(isValid);
    }
}
