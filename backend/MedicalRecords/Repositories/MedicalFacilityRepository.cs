using System;
using MedicalRecords.Data;
using MedicalRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Repositories;

public class MedicalFacilityRepository : IRepository<MedicalFacility>
{
    private readonly MedicalRecordsContext _context;
    public MedicalFacilityRepository(MedicalRecordsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicalFacility>> GetAllAsync()
    {
        try
        {
            return await _context.Facilities.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical facilities: {ex.Message}");
        }
    }

    public async Task<MedicalFacility> GetByIdAsync(int id)
    {
        if(id == 0)
        {
            throw new ArgumentException("Invalid id");
        }

        MedicalFacility? facility = null;

        try
        {
            facility = await _context.Facilities.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical facility: {ex.Message}");
        }

        if (facility == null)
        {
            throw new Exception($"Medical facility with id {id} not found");
        }

        return facility;
    }

    public async Task AddAsync(MedicalFacility entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            _context.Facilities.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding medical facility: {ex.Message}");
        }
    }

    public async Task UpdateAsync(MedicalFacility entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        MedicalFacility? facility = null;

        try
        {
            facility = await _context.Facilities.FindAsync(entity.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical facility: {ex.Message}");
        }

        if (facility == null)
        {
            throw new Exception($"Medical facility with id {entity.Id} not found");
        }

        try 
        {
            _context.Entry(facility).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating medical facility: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        if(id == 0)
        {
            throw new ArgumentException("Invalid id");
        }

        MedicalFacility? facility = null;

        try
        {
            facility = await _context.Facilities.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving medical facility: {ex.Message}");
        }

        if (facility == null)
        {
            throw new Exception($"Medical facility with id {id} not found");
        }

        try
        {
            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting medical facility: {ex.Message}");
        }
    }
}
