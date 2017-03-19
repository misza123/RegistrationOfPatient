using Microsoft.EntityFrameworkCore;
using WebRegistrationOfPatient.Models;

namespace WebRegistrationOfPatient.Repositories
{
  public class PatientContext : DbContext
  {
    public DbSet<Patient> Patients { get; set; }

    public DbSet<Address> Address { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(Configuration.DefaultConnection);
    }
  }
}
