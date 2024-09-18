using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalRecords.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalFacility",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalFacility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MedicalFacilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_MedicalFacility_MedicalFacilityId",
                        column: x => x.MedicalFacilityId,
                        principalTable: "MedicalFacility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMedicalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyMemberName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MedicalCondition = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMedicalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMedicalHistory_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medications = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Surgeries = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ControlledSubstances = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistory_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symptoms = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Studies = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DoctorScore = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_MedicalFacilityId",
                table: "Doctor",
                column: "MedicalFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMedicalHistory_PatientId",
                table: "FamilyMedicalHistory",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_PatientId",
                table: "MedicalHistory",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_DoctorId",
                table: "MedicalRecord",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_PatientId",
                table: "MedicalRecord",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyMedicalHistory");

            migrationBuilder.DropTable(
                name: "MedicalHistory");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "MedicalFacility");
        }
    }
}
