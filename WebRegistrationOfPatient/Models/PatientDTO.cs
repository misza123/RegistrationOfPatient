namespace WebRegistrationOfPatient.Models
{
  public class PatientDTO
  {
    public string Name { get; set; }

    public string Surname { get; set; }

    public string PersonalIdentityNumber { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    public AddressDTO Address { get; set; }
  }
}
