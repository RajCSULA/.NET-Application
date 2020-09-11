using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Texsperts.Models;

namespace Texsperts.Controllers
{
    public class RegistrationController : ApiController
    {

        WorkoutEntities context = new WorkoutEntities();

        //Get All Employees
        [AcceptVerbs("GET")]
     
        [HttpGet]
        public IEnumerable<Registration> GetAllEmployee()
        {

            var data = context.Registrations.ToList().OrderBy(x => x.FirstName);
            var result = data.Select(x => new Registration()
            {
                RegistrationId = x.RegistrationId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email=x.Email,
                Password=x.Password,
                ConfirmPassword=x.ConfirmPassword,
              
            });
            return result.ToList();
        }


        //Get the single employee data
        [HttpGet]
        public Registration GetEmployee(int Id)
        {
            var data = context.Registrations.Where(x => x.RegistrationId == Id).FirstOrDefault();
            if (data != null)
            {
                Registration employee = new Registration();
                employee.RegistrationId = data.RegistrationId;
                employee.FirstName = data.FirstName;
                employee.LastName = data.LastName;
                employee.Email = data.Email;
                employee.Password = data.Password;
                employee.ConfirmPassword = data.ConfirmPassword;

                return employee;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }



        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage AddEmployee(Registration model)
        {
            WorkoutEntities db = new WorkoutEntities();
            try
            {
                if (ModelState.IsValid)
                {
                    Registration emp = new Registration();
                    emp.RegistrationId = model.RegistrationId;
                    emp.FirstName = model.FirstName;
                    emp.LastName = model.LastName;
                    emp.Email = model.Email;
                    emp.Password = model.Password;
                    emp.ConfirmPassword = model.ConfirmPassword;

                    db.Registrations.Add(emp);
                    var result = db.SaveChanges();
                    if (result > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "Submitted Successfully");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something wrong !", ex);
            }
        }
    }
}