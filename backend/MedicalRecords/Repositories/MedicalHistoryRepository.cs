using System;
using MedicalRecords.Data;
using MedicalRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Repositories;

public class MedicalHistoryRepository : IRepository<MedicalHistory>
{
    private readonly MedicalRecordsContext _context;
    public MedicalHistoryRepository(MedicalRecordsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicalHistory>> GetAllAsync()
    {
        try
        {
            return await _context.MedicalHistories.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical history: {ex.Message}");
        }
    }

    public async Task<MedicalHistory> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid Medical history ID");
        }

        MedicalHistory? medicalHistory = null;

        try
        {
            medicalHistory = await _context.MedicalHistories.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical history: {ex.Message}");
        }

        if (medicalHistory == null)
        {
            throw new Exception("Patient not found");
        }

        return medicalHistory;
    }

    public async Task AddAsync(MedicalHistory entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            await _context.MedicalHistories.AddAsync(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding medical history: {ex.Message}");
        }
    }

    public async Task UpdateAsync(MedicalHistory entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        MedicalHistory? medicalHistory = null;

        try
        {
            medicalHistory = await _context.MedicalHistories.FindAsync(entity.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical history: {ex.Message}");
        }

        if(medicalHistory == null)
        {
            throw new Exception("Medical history not found");
        }

        try
        {
            _context.Entry(medicalHistory).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating medical history: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid medical history ID");
        }

        MedicalHistory? medicalHistory = null;

        try
        {
            medicalHistory = await _context.MedicalHistories.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical history: {ex.Message}");
        }

        if(medicalHistory == null)
        {
            throw new Exception("Medical history not found");
        }

        try
        {
            _context.MedicalHistories.Remove(medicalHistory);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting medical history: {ex.Message}");
        }
    }
}
