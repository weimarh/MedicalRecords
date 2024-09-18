using System;
using MedicalRecords.Data;
using MedicalRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Repositories;

public class PatientRepository : IRepository<Patient>
{
    private readonly MedicalRecordsContext _context;
    public PatientRepository(MedicalRecordsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        try 
        {
            return await _context.Patients.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving patients: {ex.Message}");
        }
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid patient ID");
        }

        Patient? patient = null;

        try
        {
            patient = await _context.Patients.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving patient: {ex.Message}");
        }

        if (patient == null)
        {
            throw new Exception("Patient not found");
        }

        return patient;
    }

    public async Task AddAsync(Patient entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            await _context.Patients.AddAsync(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding patient: {ex.Message}");
        }
    }

    public async Task UpdateAsync(Patient entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Patient? patient = null;

        try
        {
            patient = await _context.Patients.FindAsync(entity.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving patient: {ex.Message}");
        }

        if(patient == null)
        {
            throw new Exception("Patient not found");
        }

        try
        {
            _context.Entry(patient).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating patient: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid patient ID");
        }

        Patient? patient = null;

        try
        {
            patient = await _context.Patients.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving patient: {ex.Message}");
        }

        if(patient == null)
        {
            throw new Exception("Patient not found");
        }

        try
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting patient: {ex.Message}");
        }
    }
}
