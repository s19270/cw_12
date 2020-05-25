using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw_12.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw_12.Controllers
{
    public class PatientsController : Controller
    {
        private readonly s19270Context _context;

        public PatientsController(s19270Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Patient.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}