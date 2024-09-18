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
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IRepository<MedicalFacility> _medicalFacilityRepository;
        public DoctorController(IRepository<Doctor> repository, IRepository<MedicalFacility> medicalFacilityRepository)
        {
            _repository = repository;
            _medicalFacilityRepository = medicalFacilityRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorDTO>> GetAll()
        {
            var doctors = await _repository.GetAllAsync();
            return doctors.Select(doctor => doctor.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetById(int id)
        {
            if (id == 0)
                throw new Exception("Error getting doctor");

            var doctor = await _repository.GetByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> Create(DoctorDTO doctorDTO)
        {
            ArgumentNullException.ThrowIfNull(doctorDTO);

            if (!DoctorValidator.IsValid(doctorDTO))
                throw new Exception("Error adding doctor");
            
            MedicalFacility medicalFacility = await _medicalFacilityRepository.GetByIdAsync(doctorDTO.MedicalFacilityId);
            
            Doctor doctor = new Doctor()
            {
                FullName = doctorDTO.FullName,
                Specialization = doctorDTO.Specialization,
                MedicalFacilityId = doctorDTO.MedicalFacilityId,
                Facility = medicalFacility
            };

            await _repository.AddAsync(doctor);

            return Ok(doctor.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DoctorDTO doctorDTO)
        {
            ArgumentNullException.ThrowIfNull(doctorDTO);
            
            if(id <= 0)
                throw new Exception("Error updating doctor");
            
            if (!DoctorValidator.IsValid(doctorDTO))
                throw new Exception("Error updating doctor");
            
            Doctor doctor = await _repository.GetByIdAsync(id);

            if (doctor == null)
                throw new Exception("Error updating doctor");
            
            MedicalFacility medicalFacility = await _medicalFacilityRepository.GetByIdAsync(doctorDTO.MedicalFacilityId);
            
            doctor.FullName = doctorDTO.FullName;
            doctor.Specialization = doctorDTO.Specialization;
            doctor.MedicalFacilityId = doctorDTO.MedicalFacilityId;
            doctor.Facility = medicalFacility;

            await _repository.UpdateAsync(doctor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new Exception("Error deleting doctor");

            Doctor doctor = await _repository.GetByIdAsync(id);

            if (doctor == null)
                throw new Exception("Error deleting doctor");

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
