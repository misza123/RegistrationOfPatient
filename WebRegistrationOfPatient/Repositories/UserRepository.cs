using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebRegistrationOfPatient.Models;
using WebRegistrationOfPatient.Repositories.Interfaces;

namespace WebRegistrationOfPatient.Repositories
{
  public class UserRepository : IUserRepository
  {
    public User GetUserByLogin(string login)
    {
      using (var dbContext = new WebRegistrationContext())
      {
        return dbContext.Users.Include(user => user.Employee).Include(user => user.Patient).First(x => x.Login == login);
      }
    }

    public void Add(User user)
    {
      using (var dbContext = new WebRegistrationContext())
      {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
      }
    }
  }
}
