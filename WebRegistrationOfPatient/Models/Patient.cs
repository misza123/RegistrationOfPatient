using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRegistrationOfPatient.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PersonalIdentityNumber { get; set; }

        public Address Address { get; set; }
    }
}