using WebRegistrationOfPatient.Models;

namespace WebRegistrationOfPatient.Repositories.Interfaces
{
  public interface IUserRepository
  {
    User GetUserByLogin(string login);

    void Add(User user);
  }
}
