using WebRegistrationOfPatient.Models.Enums;

namespace WebRegistrationOfPatient.Models
{
  public class Employee
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public EmployeeType EmployeeTypeId { get; set; }
  }
}
