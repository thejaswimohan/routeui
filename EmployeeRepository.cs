namespace SampleAPIProject.Repository
{
    using System;

    public class EmployeeRepository : IDisposable
    {
        private EmployeeEntities context = new EmployeeEntities();

        private CommonRepository<Employee> employeeRepository;


        public CommonRepository<Employee> employee_Repository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new CommonRepository<Employee>(context);
                }
                return employeeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}