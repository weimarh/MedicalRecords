using MedicalRecords.DTOS;
using MedicalRecords.Models;
using MedicalRecords.Repositories;
using MedicalRecords.Utils;
using MedicalRecords.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/facility")]
    [ApiController]
    public class MedicalFacilityController : ControllerBase
    {
        private readonly IRepository<MedicalFacility> _repository;
        public MedicalFacilityController(IRepository<MedicalFacility> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicalFacilityDTO>> GetAll()
        {
            var medicalFacilities = await _repository.GetAllAsync();
            return medicalFacilities.Select(m => m.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalFacilityDTO>> GetById(int id)
        {
            if (id == 0)
                throw new Exception("Error getting medical facility");

            var medicalFacility = await _repository.GetByIdAsync(id);

            if (medicalFacility == null)
            {
                return NotFound();
            }

            return Ok(medicalFacility.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<MedicalFacilityDTO>> Create(MedicalFacilityDTO facility)
        {
            ArgumentNullException.ThrowIfNull(facility);

            if (MedicalFacilityValidator.IsValid(facility))
                throw new Exception("Error adding medical facility");
            
            MedicalFacility medicalFacility = new MedicalFacility()
            {
                FacilityName = facility.FacilityName,
                Address = facility.Address,
            };

            await _repository.AddAsync(medicalFacility);

            return Ok(medicalFacility.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, MedicalFacilityDTO facility)
        {
            ArgumentNullException.ThrowIfNull(facility);

            if (id == 0)
                throw new Exception("Error updating medical facility");
            
            if (!MedicalFacilityValidator.IsValid(facility))
                throw new Exception("Error updating medical facility");

            var existingFacility = await _repository.GetByIdAsync(id);

            if (existingFacility == null)
            {
                return NotFound();
            }

            existingFacility.FacilityName = facility.FacilityName;
            existingFacility.Address = facility.Address;

            await _repository.UpdateAsync(existingFacility);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
                throw new Exception("Error deleting medical facility");

            var medicalFacility = await _repository.GetByIdAsync(id);

            if (medicalFacility == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
