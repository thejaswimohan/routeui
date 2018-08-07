namespace SampleAPIProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using SampleAPIProject.Repository;

    public class EmployeeController : ApiController
    {
        private EmployeeRepository unitOfWork = new EmployeeRepository();

        [Route("api/GetEmployees")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {         
            var employee = await Task.Run(() => unitOfWork.employee_Repository.GetAll());
            return employee;
        }

        [Route("api/GetEmployee")]
        [ResponseType(typeof(Employee))]
        public async Task<HttpResponseMessage> GetEmployee(int id)
        {
            Employee employee = await Task.Run(() => unitOfWork.employee_Repository.GetById(id));
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [Route("api/GetEmployeesSortBySalary")]
        public IEnumerable<Employee> GetEmployeesSortBySalary(Employee emp)
        {
            var employee =unitOfWork.employee_Repository.GetAll();
            employee.ToList().Sort();
            return employee;
        }

        [Route("api/PutEmployee")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(long id, Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.employee_Repository.Update(employee);
                    unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Some Error Occurred" + ex.StackTrace);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Employee))]
        [Route("api/PostEmployee")]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.employee_Repository.Add(employee);
                    unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Some Error Occurred." + ex.StackTrace);
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.ID }, employee);
        }

        [Route("api/DeleteEmployee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = unitOfWork.employee_Repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            unitOfWork.employee_Repository.Delete(employee);
            unitOfWork.Save();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}