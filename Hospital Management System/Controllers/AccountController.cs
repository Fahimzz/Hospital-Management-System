using Hospital_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace Hospital_Management_System.Controllers
{
    public class AccountController : Controller
    {
        HMSEntities contex = new HMSEntities();
        // GET: Accoutn
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login l)
        {
            using (var context = new HMSEntities())
            {
               
                bool isValid = context.Logins.Any(x => x.Username == l.Username && x.Password == l.Password);
                var type = contex.Logins.FirstOrDefault(e => e.Username == l.Username);
                //Session["type"] = l.Type.ToString();

                if (isValid)
                {
                    if(type.Type=="Admin")
                    {
                        FormsAuthentication.SetAuthCookie(l.Username, true);
                        Session["username"] = l.Username.ToString();
                        return RedirectToAction("Welcome");
                    }
                    else if(type.Type=="Patient")
                    {
                        FormsAuthentication.SetAuthCookie(l.Username, false);
                        Session["username"] = l.Username.ToString();
                       
                        return RedirectToAction("Doctors_Details", "Patient");

                    }

                    else if (type.Type == "Doctor")
                    {
                        FormsAuthentication.SetAuthCookie(l.Username, false);
                        Session["username"] = l.Username.ToString();
                        return RedirectToAction("Login");

                    }
                    else if (type.Type == "Receptionist")
                    {
                        FormsAuthentication.SetAuthCookie(l.Username, false);
                        Session["username"] = l.Username.ToString();
                        return RedirectToAction("AppointmentAcceptance");

                    }



                }
                ModelState.AddModelError("", "Invalied Username Or Password ");
                return View();
            }

        }

        [Authorize]
        public ActionResult Dashboard()
        {
            var doctors = contex.Doctors.ToList();
            return View(doctors);
        }

        [Authorize]
        public ActionResult ShowReceptionist()
        {
            var r = contex.Receptionists.ToList();
            return View(r);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Homepage","HMS");
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddDoctor()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddDoctor(Doctor d, Login l)
        {
            l.Type = "Doctor";
            //d.Type = "Doctor";
            contex.Doctors.Add(d);
            contex.Logins.Add(l);
            contex.SaveChanges();
            return View("Successfull");
        }
        [Authorize]
        public ActionResult Successfull()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddReceptionist()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddReceptionist(Receptionist d, Login l)
        {
            l.Type = "Receptionist";
            contex.Receptionists.Add(d);
            contex.Logins.Add(l);
            contex.SaveChanges();
            return View("Successfull");
        }
        [Authorize]
        public ActionResult LogPatient()
        {
            var log = contex.Patientlogs.ToList();
            return View(log);
        }

        [Authorize]
        public ActionResult AppointmentAcceptance()
        { 
            var l = contex.Appointments.ToList();

            return View(l);

        }

        public ActionResult Accept(Appointment a)
        {
            a.Status = "Accepted";
            var old = contex.Appointments.FirstOrDefault(e => e.Patients == a.Patients && e.Doctors == a.Doctors && e.Id==a.Id);
            contex.Entry(old).CurrentValues.SetValues(a);
            contex.SaveChanges();
            return RedirectToAction("AppointmentAcceptance");

        }
       [Authorize]
      
       public ActionResult AppointmentLog()
        {
            var log = contex.AppointmentLogs.ToList();
            return View(log);
    }
       
        public ActionResult search(string searching)
        {
            var v = contex.AppointmentLogs.Where(x => x.Status.Contains(searching)).ToList();
            return View("AppointmentLog", v);
        }

        public ActionResult Delete(string Username)
        {
            var b = contex.Doctors.FirstOrDefault(e => e.Username == Username);
            return View(b);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteD(string Username)
        {
            var l = contex.Logins.FirstOrDefault(e => e.Username == Username);
            var d = contex.Doctors.FirstOrDefault(e => e.Username == Username);
           
            contex.Doctors.Remove(d);
            contex.Logins.Remove(l);
            contex.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public ActionResult DeleteforR(string Username)
        {
            var a = contex.Receptionists.FirstOrDefault(e => e.Username == Username);
            return View(a);
        }

        [HttpPost]
        [ActionName("DeleteForR")]
        public ActionResult DeleteR(string Username)
        {
            var l = contex.Logins.FirstOrDefault(e => e.Username == Username);
            var d = contex.Receptionists.FirstOrDefault(e => e.Username == Username);

            contex.Receptionists.Remove(d);
            contex.Logins.Remove(l);
            contex.SaveChanges();
            return RedirectToAction("ShowReceptionist");
        }

       
    }
}