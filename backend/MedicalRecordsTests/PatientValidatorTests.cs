using System;
using MedicalRecords.Models;
using MedicalRecords.Utils;
using MedicalRecords.Validators;

namespace MedicalRecordsTests;

[TestClass]
public class PatientValidatorTests
{
    Patient patient = new Patient();

    [TestMethod]
    public void IsValid_ValidData_ReturnsTrue()
    {
        // Arrange
        patient.Id = 1;
        patient.FirstName = "John";
        patient.LastName = "Doe";
        patient.Gender = "Male";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_FirstNaneIsNull_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = null;
        patient.LastName = "Doe";
        patient.Gender = "Male";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_LastNaneIsNull_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "John";
        patient.LastName = null;
        patient.Gender = "Male";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_GenderIsNull_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "John";
        patient.LastName = "Doe";
        patient.Gender = null;
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_FirstNameIsEmpty_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "";
        patient.LastName = "Doe";
        patient.Gender = "Male";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_LastNameIsEmpty_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "John";
        patient.LastName = "";
        patient.Gender = "Male";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_GenderIsEmpty_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "John";
        patient.LastName = "Doe";
        patient.Gender = "";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_FirstNameTooLong_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = new string('a', 1000);
        patient.LastName = "Doe";
        patient.Gender = "Gender";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_LastNameTooLong_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "Jon";
        patient.LastName = new string('a', 1000);
        patient.Gender = "Gender";
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_GenderTooLong_ReturnsFalse()
    {
        // Arrange
        patient.FirstName = "Jon";
        patient.LastName = "Doe";
        patient.Gender = new string('a', 1000);
        patient.BirthDate = new DateTime(1990, 1, 1);


        // Act
        var result = PatientValidator.IsValid(patient.AsDto());

        // Assert
        Assert.IsFalse(result);
    }
}
