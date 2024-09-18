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
    [Route("api/history")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IRepository<MedicalHistory> _repository;
        private readonly IRepository<Patient> _patientRepository;
        public MedicalHistoryController(IRepository<MedicalHistory> repository, IRepository<Patient> patientRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicalHistoryDTO>> GetAll()
        {
            var medicalHistories = await _repository.GetAllAsync();
            return medicalHistories.Select(m => m.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryDTO>> GetById(int id)
        {
            if (id == 0)
                throw new Exception("Error getting medical history");
            
            var medicalHistory = await _repository.GetByIdAsync(id);
            
            if (medicalHistory == null)
                throw new Exception("Error getting medical history");

            return Ok(medicalHistory.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<MedicalHistoryDTO>> Create(MedicalHistoryDTO medicalHistoryDTO)
        {
            if (medicalHistoryDTO == null)
                throw new Exception("Error creating medical history");

            var patient = await _patientRepository.GetByIdAsync(medicalHistoryDTO.PatientId);

            if (patient == null)
                throw new Exception("Error creating medical history");

            if (!MedicalHistoryValidator.IsValid(medicalHistoryDTO))
                throw new Exception("Error creating medical history");
            
            MedicalHistory medicalHistory = new MedicalHistory
            {
                PatientId = medicalHistoryDTO.PatientId,
                Medications = medicalHistoryDTO.Medications,
                Allergies = medicalHistoryDTO.Allergies,
                Conditions = medicalHistoryDTO.Conditions,
                Surgeries = medicalHistoryDTO.Surgeries,
                ControlledSubstances = medicalHistoryDTO.ControlledSubstances,
                Patient = patient
            };

            await _repository.AddAsync(medicalHistory);

            return Ok(medicalHistory.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, MedicalHistoryDTO medicalHistoryDTO)
        {
            if (id == 0 || medicalHistoryDTO == null)
                throw new Exception("Error updating medical history");

            var medicalHistory = await _repository.GetByIdAsync(id);

            if (medicalHistory == null)
                throw new Exception("Error updating medical history");

            if (!MedicalHistoryValidator.IsValid(medicalHistoryDTO))
                throw new Exception("Error updating medical history");

            Patient patient = await _patientRepository.GetByIdAsync(medicalHistoryDTO.PatientId);

            medicalHistory.PatientId = medicalHistoryDTO.PatientId;
            medicalHistory.Medications = medicalHistoryDTO.Medications;
            medicalHistory.Allergies = medicalHistoryDTO.Allergies;
            medicalHistory.Conditions = medicalHistoryDTO.Conditions;
            medicalHistory.Surgeries = medicalHistoryDTO.Surgeries;
            medicalHistory.ControlledSubstances = medicalHistoryDTO.ControlledSubstances;
            medicalHistory.Patient = patient;

            await _repository.UpdateAsync(medicalHistory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
                throw new Exception("Error deleting medical history");

            var medicalHistory = await _repository.GetByIdAsync(id);

            if (medicalHistory == null)
                throw new Exception("Error deleting medical history");

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
