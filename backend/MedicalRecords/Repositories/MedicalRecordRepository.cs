using System;
using MedicalRecords.Data;
using MedicalRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Repositories;

public class MedicalRecordRepository : IRepository<MedicalRecord>
{
    private readonly MedicalRecordsContext _context;
    public MedicalRecordRepository(MedicalRecordsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
    {
        try 
        {
            return await _context.MedicalRecords.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical records: {ex.Message}");
        }
    }

    public async Task<MedicalRecord> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid medical record ID");
        }

        MedicalRecord? record = null;

        try
        {
            record = await _context.MedicalRecords.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical record: {ex.Message}");
        }

        if (record == null)
        {
            throw new Exception("Medical record not found");
        }

        return record;
    }

    public async Task AddAsync(MedicalRecord entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            await _context.MedicalRecords.AddAsync(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding medical record: {ex.Message}");
        }
    }

    public async Task UpdateAsync(MedicalRecord entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        MedicalRecord? record = null;

        try
        {
            record = await _context.MedicalRecords.FindAsync(entity.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical record: {ex.Message}");
        }

        if(record == null)
        {
            throw new Exception("Medical record not found");
        }

        try
        {
            _context.Entry(record).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating medical record: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid medical record ID");
        }

        MedicalRecord? record = null;

        try
        {
            record = await _context.MedicalRecords.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical record: {ex.Message}");
        }

        if(record == null)
        {
            throw new Exception("Medical record not found");
        }

        try
        {
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting medical record: {ex.Message}");
        }
    }
}
