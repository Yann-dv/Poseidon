using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public class UserControllerTests
{
    private static ApiDbContext TestDbContext;

    [SetUp]
    public void Init()
    {
        TestDbContext = Setup.FakeUserDbContext();
        TestDbContext.ChangeTracker.Clear();
    }
    
    [Test]
    public void Should_return_all_users_when_read_action()
    {
        var userController = new PoseidonApi.Controllers.UserController(TestDbContext);

        var getAllUsers = userController.GetUsers();

        Assert.That(getAllUsers.Result.Value!.Count(), Is.EqualTo(2));
    }

    [TestCase(1)]
    [TestCase(2)]
    [Test]
    public void Should_return_expected_curve_point_when_get_user_by_id(int id)
    {
        var userController = new PoseidonApi.Controllers.UserController(TestDbContext);

        var getUserById = userController.GetUser(id);

        Assert.That(getUserById.Result.Value!.Id, Is.EqualTo(id));
    }

    [Test]
    public void Should_return_expected_newly_created_user_when_post()
    {
        var userController = new PoseidonApi.Controllers.UserController(TestDbContext);

        var userToAdd = userController.CreateUser(
            new UserDTO()
            {
                Id = 3,
                Username = "Username3",
                Password = "Password3",
                Fullname = "Fullname3",
                Role = "Role3"
            });

        Assert.That(userToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
        Assert.That(TestDbContext.Users.Count(), Is.EqualTo(3));
        Assert.That(TestDbContext.Users.ToList()[2].Id, Is.EqualTo(3));
    }

    [Test, Order(1)]
    public void Should_return_expected_updated_user_when_put()
    {
        var userController = new PoseidonApi.Controllers.UserController(TestDbContext);

        var userToUpdate = userController.UpdateUser(id: 2,
            new UserDTO()
            {
                Id = 2,
                Username = "UsernameUpdated",
                Password = "PasswordUpdated",
                Fullname = "FullnameUpdated",
                Role = "RoleUpdated"
            }, TestDbContext);

        Assert.That(userToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.Users.Count(), Is.EqualTo(2)); //Ensure no POST
        Assert.That(TestDbContext.Users.ToList().Find(c => c.Id == 2).Username, Is.EqualTo("UsernameUpdated"));
    }

    [Test]
    public void Should_return_expected_deleted_user_when_delete()
    {
        var userController = new PoseidonApi.Controllers.UserController(TestDbContext);

        var userToDelete = userController.DeleteUser(1);

        Assert.That(userToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.Users.Count(), Is.EqualTo(1));
        Assert.That(TestDbContext.Users.ToList().Find(c => c.Id == 1), Is.Null);
    }
}
