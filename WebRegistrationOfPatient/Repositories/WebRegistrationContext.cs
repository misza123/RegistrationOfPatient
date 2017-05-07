using Microsoft.EntityFrameworkCore;
using WebRegistrationOfPatient.Models;

namespace WebRegistrationOfPatient.Repositories
{
  public class WebRegistrationContext : DbContext
  {
    public DbSet<Patient> Patients { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(Configuration.DefaultConnection);

    }
  }
}
