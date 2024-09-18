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
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<Patient> _repository;
        public PatientController(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<PatientDTO>> GetAll()
        {
            var patients = await _repository.GetAllAsync();
            return patients.Select(p => p.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetById(int id)
        {
            if (id == 0)
                throw new Exception("Error getting patient");

            var patient = await _repository.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> Create(PatientDTO patientDTO)
        {
            ArgumentNullException.ThrowIfNull(patientDTO);

            if (!PatientValidator.IsValid(patientDTO))
                throw new Exception("Error adding patient");
            
            Patient patient = new Patient()
            {
                Id = patientDTO.Id,
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                BirthDate = patientDTO.BirthDate,
                Gender = patientDTO.Gender,
            };

            await _repository.AddAsync(patient);

            return Ok(patient.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDTO>> Update(int id, PatientDTO patientDTO)
        {
            ArgumentNullException.ThrowIfNull(patientDTO);

            var patientToUpdate = await _repository.GetByIdAsync(id);

            if (patientToUpdate == null)
                return NotFound();

            if (!PatientValidator.IsValid(patientDTO))
                throw new Exception("Error updating patient");

            Patient patient = new Patient()
            {
                Id = patientDTO.Id,
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                BirthDate = patientDTO.BirthDate,
                Gender = patientDTO.Gender,
            };

            await _repository.UpdateAsync(patient);

            return Ok(patient.AsDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patientToDelete = await _repository.GetByIdAsync(id);

            if (patientToDelete == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
