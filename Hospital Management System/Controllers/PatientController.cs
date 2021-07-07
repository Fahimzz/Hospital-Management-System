using Hospital_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Management_System.Controllers
{
    
    public class PatientController : Controller
    {
        HMSEntities context = new HMSEntities();
        // GET: Patient
        [Authorize]
        public ActionResult Index()
        {
            var Patients = context.Patients.ToList();
            return View(Patients);
        }
        [Authorize]
        public ActionResult Doctors_Details()
        {
            var Doctors = context.Doctors.ToList();
            return View(Doctors);
        }
        

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Patient p, Login l)
        {
            l.Type = "Patient";
            context.Patients.Add(p);
            context.Logins.Add(l);
            context.SaveChanges();

            return RedirectToAction("WelcomeForPatient");
        }
        [Authorize]
        public ActionResult WelcomeForPatient()
        {
            return View();
        }
        [Authorize]
        public ActionResult Appointment(Appointment a)
        {
            a.Status = "Pending";

            context.Appointments.Add(a);
            context.SaveChanges();
            return RedirectToAction("AppointSuccessfull");

        }
        [Authorize]
        public ActionResult AppointSuccessfull()
        {
            return View();
        }

        [Authorize]
        public ActionResult Appointment_Details(Appointment a)
        {
            var Username = a.Patients;
            //var appointment = context.Appointments.FirstOrDefault(e=> e.Patients==Username);
            //var a = context.Appointments.ToList();
            return View(context.Appointments.Where(x=> x.Patients.Contains(Username)).ToList());
        }
       [Authorize]
        public ActionResult Edit(string Username)
        {
            var p = context.Patients.FirstOrDefault(e => e.Username == Username);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(Patient p)
        {
            var oldp = context.Patients.FirstOrDefault(e => e.Username == p.Username);
            context.Entry(oldp).CurrentValues.SetValues(p);
            context.SaveChanges();

            return RedirectToAction("Doctors_Details");

        }



    }
}