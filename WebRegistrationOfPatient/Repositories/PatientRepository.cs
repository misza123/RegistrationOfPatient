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
      using (var dbContext = new WebRegistrationContext())
      {
        dbContext.Patients.Add(patient);
        dbContext.SaveChanges();
      }
    }

    public IEnumerable<Patient> GettAll()
    {
      using (var dbContext = new WebRegistrationContext())
      {
        return dbContext.Patients.ToList();
      }
    }

    public Patient Find(int id)
    {
      using (var dbContext = new WebRegistrationContext())
      {
        return dbContext.Patients.FirstOrDefault(x => x.Id == id);
      }
    }

    public void Remove(int id)
    {
      using (var dbContext = new WebRegistrationContext())
      {
        var patientToRemove = dbContext.Patients.FirstOrDefault(x => x.Id == id);
        dbContext.Patients.Remove(patientToRemove);
        dbContext.SaveChanges();
      }
    }

    public void Update(Patient patient)
    {
      using (var dbContext = new WebRegistrationContext())
      {
        dbContext.Patients.Update(patient);
      }
    }
  }
}
