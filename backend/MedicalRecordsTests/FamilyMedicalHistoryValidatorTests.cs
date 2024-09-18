using System;
using MedicalRecords.Models;
using MedicalRecords.Utils;
using MedicalRecords.Validators;

namespace MedicalRecordsTests;

[TestClass]
public class FamilyMedicalHistoryValidatorTests
{
    FamilyMedicalHistory history = new FamilyMedicalHistory();

    [TestMethod]
    public void IsValid_ValidData_ReturnsTrue()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_FamilyMemberNameIsNull_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = null;
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_FamilyMemberNameIsEmptyString_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "";
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_RelationshipIsNull_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = null;
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_RelationshipIsEmptyString_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = "";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_MedicalConditionLengthTooLong_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes, Diabetes, Diabetes, Diabetes, Diabetes, Diabetes, Diabetes";
        history.Treatment = "Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_TreatmentLengthTooLong_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin injection, Insulin injection, Insulin injection, Insulin injection";
        history.Notes = "Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_NotesLengthTooLong_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin";
        history.Notes = "Notes about the condition, Notes about the condition, Notes about the condition, Notes about the condition, Notes about the condition, Notes about the condition, Notes about the condition, Notes about the condition";
        history.PatientId = 1;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_PatientIdIsZero_ReturnsFalse()
    {
        // Arrange
        history.FamilyMemberName = "Family member name";
        history.Relationship = "Mother";
        history.MedicalCondition = "Diabetes";
        history.Treatment = "Insulin";
        history.Notes = "Notes about the condition";
        history.PatientId = 0;

        // Act
        var result = FamilyMedicalHistoryValidator.IsValid(history.AsDto());

        // Assert
        Assert.IsFalse(result);
    }
}
