using System.Collections.Generic;
using WebRegistrationOfPatient.Models;

namespace WebRegistrationOfPatient.Repositories.Interfaces
{
  public interface IPatientRepository
  {
    void Add(Patient patient);

    IEnumerable<Patient> GettAll();

    Patient Find(int id);

    void Remove(int id);

    void Update(Patient patient);
  }
}
