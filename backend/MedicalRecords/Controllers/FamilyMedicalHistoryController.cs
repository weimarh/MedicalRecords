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
    [Route("api/family")]
    [ApiController]
    public class FamilyMedicalHistoryController : ControllerBase
    {
        public readonly IRepository<FamilyMedicalHistory> _repository;
        public readonly IRepository<Patient> _patientRepository;
        public FamilyMedicalHistoryController(IRepository<FamilyMedicalHistory> repository, IRepository<Patient> patientRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FamilyMedicalHistoryDTO>> GetAll()
        {
            var medicalHistories = await _repository.GetAllAsync();
            return medicalHistories.Select(mh => mh.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyMedicalHistoryDTO>> GetById(int id)
        {
            if (id == 0)
                throw new Exception("Error getting doctor");
            
            var medicalHistory = await _repository.GetByIdAsync(id);

            if (medicalHistory == null)
                throw new Exception("Error getting doctor");
            
            return Ok(medicalHistory.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<FamilyMedicalHistoryDTO>> Create(FamilyMedicalHistoryDTO history)
        {
            ArgumentNullException.ThrowIfNull(history);

            if (!FamilyMedicalHistoryValidator.IsValid(history))
                throw new Exception("Error creating family medical history");
            
            Patient patient = await _patientRepository.GetByIdAsync(history.PatientId);

            FamilyMedicalHistory medicalHistory = new FamilyMedicalHistory()
            {
                FamilyMemberName = history.FamilyMemberName,
                Relationship = history.Relationship,
                MedicalCondition = history.MedicalCondition,
                Treatment = history.Treatment,
                Notes = history.Notes,
                PatientId = history.PatientId,
                Patient = patient
            };

            await _repository.AddAsync(medicalHistory);

            return Ok(medicalHistory.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FamilyMedicalHistoryDTO history)
        {
            if (id <= 0)
                throw new Exception("Error updating family history");

            ArgumentNullException.ThrowIfNull(history);

            if (!FamilyMedicalHistoryValidator.IsValid(history))
                throw new Exception("Error updating family medical history");

            FamilyMedicalHistory medicalHistory = await _repository.GetByIdAsync(id);

            if (medicalHistory == null)
                throw new Exception("Error updating family medical history");

            Patient patient = await _patientRepository.GetByIdAsync(history.PatientId);

            medicalHistory.FamilyMemberName = history.FamilyMemberName;
            medicalHistory.Relationship = history.Relationship;
            medicalHistory.MedicalCondition = history.MedicalCondition;
            medicalHistory.Treatment = history.Treatment;
            medicalHistory.Notes = history.Notes;
            medicalHistory.PatientId = history.PatientId;
            medicalHistory.Patient = patient;

            await _repository.UpdateAsync(medicalHistory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new Exception("Error deleting family history");

            FamilyMedicalHistory medicalHistory = await _repository.GetByIdAsync(id);

            if (medicalHistory == null)
                throw new Exception("Error deleting family medical history");

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
