using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebRegistrationOfPatient.Models;
using WebRegistrationOfPatient.Repositories.Interfaces;

namespace WebRegistrationOfPatient.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly Configuration _configuration;

        public PatientRepository(IOptions<Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        public void Add(Patient patient)
        {
            using (var patientContext = new PatientContext())
            {
                patientContext.Patients.Add(patient);
                patientContext.SaveChanges();
            }
        }

        public IEnumerable<Patient> GettAll()
        {
#if DEBUG
            using (var patientContext = new PatientContext())
            {
                patientContext.Patients.Add(new Patient()
                {
                    Name = "name",
                    PersonalIdentityNumber = Guid.NewGuid().ToString(),
                    Surname = "surname",
                    Address = null
                });
                patientContext.SaveChanges();
            }
#endif
            using (var patientContext = new PatientContext())
            {
                return patientContext.Patients.ToList();
            }
        }

        public Patient Find(int id)
        {
            using (var patientContext = new PatientContext())
            {
                return patientContext.Patients.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Remove(int id)
        {
            using (var patientContext = new PatientContext())
            {
                var patientToRemove = patientContext.Patients.FirstOrDefault(x => x.Id == id);
                patientContext.Patients.Remove(patientToRemove);
                patientContext.SaveChanges();
            }
        }

        public void Update(Patient patient)
        {
            using (var patientContext = new PatientContext())
            {
                patientContext.Patients.Update(patient);
            }
        }
    }
}