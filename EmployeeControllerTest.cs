namespace SampleNunit.Test
{
    using NUnit.Framework;
    using SampleAPIProject.Controllers;
    using System.Net.Http;
    using System.Web.Http;

    [TestFixture]
    public class EmployeeControllerTest
    {
        EmployeeController controller = null;
        HttpResponseMessage response = null;

        [SetUp]
        public void RunsBeforeEachtest()
        {
            controller = new EmployeeController();
            response = new HttpResponseMessage();
        }
        
        [Test]        
        public void Should_Return_True_When_Response_Of_GetEmployees_Has_Records()
        {           
            GivenTheRequestToGetTheListOfEmployeesAndReturnsTrue();
        }

        [Test]
        public void Should_Return_True_When_Response_Of_GetEmployee_ById_Has_Records()
        {
            GivenTheRequestWithIdToGetAnEmployeeAndReturnsTrue();
        }

        [Test]
        public void Should_Return_False_When_Response_Of_GetEmployee_ById_Has_NoRecords()
        {
            GivenTheRequestWithIdToGetAnEmployeeAndReturnsFalse();
        }

        private void GivenTheRequestToGetTheListOfEmployeesAndReturnsTrue()
        {
            RunsBeforeEachtest();
            var result = controller.GetEmployees();
            Assert.IsTrue(result != null);
        }        

        private void GivenTheRequestWithIdToGetAnEmployeeAndReturnsTrue()
        {
            RunsBeforeEachtest();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            response = controller.GetEmployee(40004).Result;
            
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        private void GivenTheRequestWithIdToGetAnEmployeeAndReturnsFalse()
        {
            RunsBeforeEachtest();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            response = controller.GetEmployee(10004).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }
    }
}
