using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public class AuthControllerTests
{
    [Test]
    public void Should_return_ok_status_when_expected_authorized_user_login()
    {
        var authController = new PoseidonApi.Controllers.AuthController();

        var result = authController.Login(new LoginModel()
        {
            UserName = "johndoe",
            Password = "John_doe99$"
        });

        Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
    }

    [Test]
    public void Should_return_unauthorized_status_when_unexpected_user_try_to_login()
    {
        var authController = new PoseidonApi.Controllers.AuthController();

        var result = authController.Login(new LoginModel()
        {
            UserName = "randomGuy",
            Password = "random_pwd*102"
        });

        Assert.That(result.GetType(), Is.EqualTo(typeof(UnauthorizedResult)));
    }
}