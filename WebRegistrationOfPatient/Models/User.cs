namespace WebRegistrationOfPatient.Models
{
  public class User
  {
    public int Id { get; set; }

    public Patient Patient { get; set; }

    public Employee Employee { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }
  }
}
