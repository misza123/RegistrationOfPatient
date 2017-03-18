using Microsoft.EntityFrameworkCore;
using WebRegistrationOfPatient.Models;

namespace WebRegistrationOfPatient.Repositories
{
    public class PatientContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.DefaultConnection);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}