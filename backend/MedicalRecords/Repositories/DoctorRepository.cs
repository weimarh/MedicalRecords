using System;
using MedicalRecords.Data;
using MedicalRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Repositories;

public class DoctorRepository : IRepository<Doctor> 
{
    private readonly MedicalRecordsContext _context;
    public DoctorRepository(MedicalRecordsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        try
        {
            return await _context.Doctors.ToListAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"Error retrieving doctors: {ex.Message}");
        }
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid doctor ID.");
        }

        Doctor? doctor = null;

        try
        {
            doctor = await _context.Doctors.FindAsync(id);
        }
        catch(Exception ex)
        {
            throw new Exception($"Error retrieving doctor: {ex.Message}");
        }

        if (doctor == null)
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        return doctor;
    }

    public async Task AddAsync(Doctor entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            _context.Doctors.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"Error adding doctor: {ex.Message}");
        }
    }

    public async Task UpdateAsync(Doctor entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Doctor? doctor = null;

        try
        {
            doctor = await _context.Doctors.FindAsync(entity.Id);
        }
        catch(Exception ex)
        {
            throw new Exception($"Error retrieving doctor: {ex.Message}");
        }

        if (doctor == null)
        {
            throw new KeyNotFoundException($"Doctor with ID {entity.Id} not found.");
        }

        try
        {
            _context.Entry(doctor).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"Error updating doctor: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid doctor ID.");
        }

        Doctor? doctor = null;

        try
        {
            doctor = await _context.Doctors.FindAsync(id);
        }
        catch(Exception ex)
        {
            throw new Exception($"Error retrieving doctor: {ex.Message}");
        }

        if (doctor == null)
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        try
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"Error deleting doctor: {ex.Message}");
        }
    }
}
