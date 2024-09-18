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
    [Route("api/record")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IRepository<MedicalRecord> _repository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        public MedicalRecordController(IRepository<MedicalRecord> repository, 
            IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicalRecordDTO>> GetAll()
        {
            var medicalRecords = await _repository.GetAllAsync();
            return medicalRecords.Select(medicalRecord => medicalRecord.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordDTO>> GetById(int id)
        {
            if (id == 0)
                throw new Exception("Error getting Medical record");

            var medicalRecord = await _repository.GetByIdAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return Ok(medicalRecord.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecordDTO>> Create(MedicalRecordDTO medicalRecordDto)
        {
            ArgumentNullException.ThrowIfNull(medicalRecordDto);

            if (!MedicalRecordValidator.IsValid(medicalRecordDto))
                throw new Exception("Error creating Medical record");

            Patient patient = await _patientRepository.GetByIdAsync(medicalRecordDto.PatientId);
            Doctor doctor = await _doctorRepository.GetByIdAsync(medicalRecordDto.DoctorId);

            MedicalRecord medicalRecord = new MedicalRecord()
            {
                Patient = patient,
                PatientId = medicalRecordDto.PatientId,
                VisitDate = medicalRecordDto.VisitDate,
                Symptoms = medicalRecordDto.Symptoms,
                Height = medicalRecordDto.Height,
                Weight = medicalRecordDto.Weight,
                Diagnosis = medicalRecordDto.Diagnosis,
                Studies = medicalRecordDto.Studies,
                Treatment = medicalRecordDto.Treatment,
                DoctorScore = medicalRecordDto.DoctorScore,
                DoctorId = medicalRecordDto.DoctorId,
                Doctor = doctor
            };

            await _repository.AddAsync(medicalRecord);

            return Ok(medicalRecord.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, MedicalRecordDTO medicalRecordDto)
        {
            if (id == 0)
                throw new Exception("Error updating Medical record");

            MedicalRecord medicalRecordToUpdate = await _repository.GetByIdAsync(id);

            if (medicalRecordToUpdate == null)
            {
                return NotFound();
            }

            Patient patient = await _patientRepository.GetByIdAsync(medicalRecordDto.PatientId);
            Doctor doctor = await _doctorRepository.GetByIdAsync(medicalRecordDto.DoctorId);

            MedicalRecord medicalRecord = new MedicalRecord()
            {
                Patient = patient,
                PatientId = medicalRecordDto.PatientId,
                VisitDate = medicalRecordDto.VisitDate,
                Symptoms = medicalRecordDto.Symptoms,
                Height = medicalRecordDto.Height,
                Weight = medicalRecordDto.Weight,
                Diagnosis = medicalRecordDto.Diagnosis,
                Studies = medicalRecordDto.Studies,
                Treatment = medicalRecordDto.Treatment,
                DoctorScore = medicalRecordDto.DoctorScore,
                DoctorId = medicalRecordDto.DoctorId,
                Doctor = doctor
            };

            await _repository.UpdateAsync(medicalRecord);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
                throw new Exception("Error deleting Medical record");

            MedicalRecord medicalRecord = await _repository.GetByIdAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return Ok();
        }
    }
}
