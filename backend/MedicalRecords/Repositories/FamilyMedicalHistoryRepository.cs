using System;
using MedicalRecords.Data;
using MedicalRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Repositories;

public class FamilyMedicalHistoryRepository : IRepository<FamilyMedicalHistory>
{
    private readonly MedicalRecordsContext _context;
    public FamilyMedicalHistoryRepository(MedicalRecordsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FamilyMedicalHistory>> GetAllAsync()
    {
        try 
        {
            return await _context.FamilyMedicalHistories.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving family histories: {ex.Message}");
        }
    }

    public async Task<FamilyMedicalHistory> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid family history ID");
        }

        FamilyMedicalHistory? history = null;

        try
        {
            history = await _context.FamilyMedicalHistories.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving family history: {ex.Message}");
        }

        if (history == null)
        {
            throw new Exception("family history not found");
        }

        return history;
    }

    public async Task AddAsync(FamilyMedicalHistory entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            await _context.FamilyMedicalHistories.AddAsync(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding family history: {ex.Message}");
        }
    }

    public async Task UpdateAsync(FamilyMedicalHistory entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        FamilyMedicalHistory? history = null;

        try
        {
            history = await _context.FamilyMedicalHistories.FindAsync(entity.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving family history: {ex.Message}");
        }

        if(history == null)
        {
            throw new Exception("Family history not found");
        }

        try
        {
            _context.Entry(history).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating family history: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentException("Invalid family history ID");
        }

        FamilyMedicalHistory? history = null;

        try
        {
            history = await _context.FamilyMedicalHistories.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving family history: {ex.Message}");
        }

        if(history == null)
        {
            throw new Exception("Family history not found");
        }

        try
        {
            _context.FamilyMedicalHistories.Remove(history);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting family history: {ex.Message}");
        }
    }
}
