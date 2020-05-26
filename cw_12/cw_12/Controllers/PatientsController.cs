using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw_12.AbstractModels;
using cw_12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdDoctor,FirstName,LastName,Birthday")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _context.Patient.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdDoctor,FirstName,LastName,Birthday")] Patient patient)
        {
            if (id != patient.IdPatient)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient.Any(e => e.IdPatient == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _context.Patient
                .FirstOrDefault(m => m.IdPatient == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _context.Patient.Find(id);
            _context.Patient.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _context.Patient
                .FirstOrDefault(m => m.IdPatient == id);
            if (patient == null)
            {
                return NotFound();
            }
            var prescription = _context.Prescription.Where(p => p.IdPatient == patient.IdPatient);
            PatMed patmed = new PatMed();
            foreach (var p in prescription)
            {
                var pres_med = _context.PrescriptionMedicament.Where(a => a.IdPrescription == p.IdPrescription);
                foreach(var a in pres_med)
                {
                    var d = _context.Medicament.Where(z => z.IdMedicament == a.IdMedicament).FirstOrDefault();
                    if(!patmed.med.Contains(d)) patmed.med.Add(d);
                }
            }
            patmed.pat = patient;
            return View(patmed);
        }
    }
}