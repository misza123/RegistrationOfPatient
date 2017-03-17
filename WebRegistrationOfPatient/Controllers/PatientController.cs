using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebRegistrationOfPatient.Models;
using WebRegistrationOfPatient.Repositories.Interfaces;

namespace WebRegistrationOfPatient.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GettAll();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetPatient")]
        public IActionResult GetById(int id)
        {
            var patient = _patientRepository.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            return new ObjectResult(patient);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            _patientRepository.Add(patient);

            return CreatedAtRoute("GetPatient", new { id = patient.Id }, patient);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}