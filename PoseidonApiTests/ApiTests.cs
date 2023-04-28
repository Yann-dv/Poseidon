using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace PoseidonApiTests;

public class ApiTest
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        public void Should_return_ok_status_when_expected_authorized_user_logged_in()
        {
            var authController = new PoseidonApi.Controllers.AuthController();
            
            var result = authController.Login(new PoseidonApi.Models.LoginModel()
            {
                UserName = "johndoe",
                Password = "John_doe99$"
            });
            
            Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
        }
    }
}