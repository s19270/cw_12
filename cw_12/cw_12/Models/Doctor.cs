using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw_12.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Prescription = new HashSet<Prescription>();
        }

        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
