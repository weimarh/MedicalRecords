using System;
using MedicalRecords.Models;
using MedicalRecords.Utils;
using MedicalRecords.Validators;

namespace MedicalRecordsTests;

[TestClass]
public class MedicalFacilityValidatorTests
{
    MedicalFacility facility = new MedicalFacility();

    [TestMethod]
    public void IsValid_ValidData_ReturnsTrue()
    {
        // Arrange
        facility.FacilityName = "Test Facility";
        facility.Address = "123 Test St";

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void IsValid_FacilityNameIsNull_ReturnsFalse()
    {
        // Arrange
        facility.FacilityName = null;
        facility.Address = "123 Test St";

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_FacilityNameIsEmptyString_ReturnsFalse()
    {
        // Arrange
        facility.FacilityName = "";
        facility.Address = "123 Test St";

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_FacilityNameTooLong_ReturnsFalse()
    {
        // Arrange
        facility.FacilityName = "Too long, Too long, Too long, Too long, Too long, Too long, ";
        facility.Address = "123 Test St";

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_FacilityAddressIsNull_ReturnsFalse()
    {
        // Arrange
        facility.FacilityName = "Test Facility";
        facility.Address = null;

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_FacilityAddressIsEmptyString_ReturnsFalse()
    {
        // Arrange
        facility.FacilityName = "Test Facility";
        facility.Address = "";

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void IsValid_FacilityAddressIsTooLong_ReturnsFalse()
    {
        // Arrange
        facility.FacilityName = "Test Facility";
        facility.Address = "Too long, Too long, Too long, Too long, Too long, Too long, ";

        // Act
        bool isValid = MedicalFacilityValidator.IsValid(facility.AsDto());

        // Assert
        Assert.IsFalse(isValid);
    }
}
