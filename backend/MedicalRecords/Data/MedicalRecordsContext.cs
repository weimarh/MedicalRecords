using System;
using MedicalRecords.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Data;

public class MedicalRecordsContext : IdentityDbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<FamilyMedicalHistory> FamilyMedicalHistories { get; set; }
    public DbSet<MedicalFacility> Facilities { get; set; }
    public DbSet<MedicalHistory> MedicalHistories { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Patient> Patients { get; set; } 

    public MedicalRecordsContext(DbContextOptions<MedicalRecordsContext> options) : base (options)
    {     
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>().ToTable("Doctor");
        modelBuilder.Entity<FamilyMedicalHistory>().ToTable("FamilyMedicalHistory");
        modelBuilder.Entity<MedicalFacility>().ToTable("MedicalFacility");
        modelBuilder.Entity<MedicalHistory>().ToTable("MedicalHistory");
        modelBuilder.Entity<MedicalRecord>().ToTable("MedicalRecord");
        modelBuilder.Entity<Patient>().ToTable("Patient");
    }
}
