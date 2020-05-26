using cw_12.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw_12.AbstractModels
{
    public class PatMed
    {
        public Patient pat { get; set; }
        public List<Medicament> med;
    }
}
