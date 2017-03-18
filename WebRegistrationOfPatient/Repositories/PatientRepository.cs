using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebRegistrationOfPatient.Models;
using WebRegistrationOfPatient.Repositories.Interfaces;

namespace WebRegistrationOfPatient.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _patientContext;
        private readonly Configuration _configuration;

        public PatientRepository(PatientContext patientContext, IOptions<Configuration> configuration)
        {
            _patientContext = patientContext;
            _configuration = configuration.Value;
        }

        public void Add(Patient patient)
        {
            _patientContext.Patients.Add(patient);
            _patientContext.SaveChanges();
        }

        public IEnumerable<Patient> GettAll()
        {
#if DEBUG
            _patientContext.Patients.Add(new Patient()
            {
                Name = "name",
                PersonalIdentityNumber = "12id",
                Surname = "surname",
                Address = null
            });
            _patientContext.SaveChanges();
#endif

            var ret = _patientContext.Patients.ToList();
            return ret;
        }

        public Patient Find(int id)
        {
            return _patientContext.Patients.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var patientToRemove = _patientContext.Patients.FirstOrDefault(x => x.Id == id);
            _patientContext.Patients.Remove(patientToRemove);
            _patientContext.SaveChanges();
        }

        public void Update(Patient patient)
        {
            _patientContext.Patients.Update(patient);
        }
    }
}