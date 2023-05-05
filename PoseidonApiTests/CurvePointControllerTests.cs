using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public class CurvePointControllerTests
{
    private static ApiDbContext TestDbContext;

    [SetUp]
    public void Init()

    {
        TestDbContext = Setup.FakeCurvePointDbContext();
        TestDbContext.ChangeTracker.Clear();
    }

    [Test]
    public void Should_return_all_curve_points_when_read_action()
    {
        var curvePointController = new PoseidonApi.Controllers.CurvePointController(TestDbContext);

        var getAllCurvePoints = curvePointController.GetCurvePoints();

        Assert.That(getAllCurvePoints.Result.Value!.Count(), Is.EqualTo(2));
    }


    [TestCase(1)]
    [TestCase(12)]
    [Test]
    public void Should_return_expected_curve_point_when_get_curve_point_by_id(int id)
    {
        var curvePointController = new PoseidonApi.Controllers.CurvePointController(TestDbContext);

        var getCurvePointById = curvePointController.GetCurvePoint(id);

        Assert.That(getCurvePointById.Result.Value!.Id, Is.EqualTo(id));
    }


    [Test]
    public void Should_return_expected_newly_created_curve_point_when_post()
    {
        var curvePointController = new PoseidonApi.Controllers.CurvePointController(TestDbContext);

        var curvePointToAdd = curvePointController.PostCurvePoint(
            new CurvePointDTO()
            {
                CurveId = 3,
                Term = 3,
                Value = 3,
                CreationDate = DateTime.Now
            });

        Assert.That(curvePointToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
        Assert.That(TestDbContext.CurvePoints.Count(), Is.EqualTo(3));
        Assert.That(TestDbContext.CurvePoints.ToList()[2].Id, Is.EqualTo(13));
    }


    [Test, Order(1)]
    public void Should_return_expected_updated_curve_point_when_put()
    {
        var curvePointController = new PoseidonApi.Controllers.CurvePointController(TestDbContext);

        var curvePointToUpdate = curvePointController.PutCurvePoint(id: 1,
            new CurvePointDTO()
            {
                Id = 1,
                CurveId = 999,
                Term = 1,
                Value = 1,
                CreationDate = DateTime.Now
            }, TestDbContext);

        Assert.That(curvePointToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.CurvePoints.Count(), Is.EqualTo(2)); //Ensure no POST
        Assert.That(TestDbContext.CurvePoints.ToList().Find(c => c.Id == 1).CurveId, Is.EqualTo(999));
    }


    [Test]
    public void Should_return_expected_deleted_curve_point_when_delete()
    {
        var curvePointController = new PoseidonApi.Controllers.CurvePointController(TestDbContext);

        var curvePointToDelete = curvePointController.DeleteCurvePoint(1);

        Assert.That(curvePointToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.CurvePoints.Count(), Is.EqualTo(1));
        Assert.That(TestDbContext.CurvePoints.ToList().Find(c => c.Id == 1), Is.Null);
    }
}
