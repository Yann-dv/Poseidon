using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Data;
using PoseidonApi.DTO;
using PoseidonApi.Models;

namespace PoseidonApiTests;

[TestFixture]
public class RatingControllerTests
{
    private static ApiDbContext TestDbContext;

    [SetUp]
    public void Init()
    {
        TestDbContext = Setup.FakeRatingDbContext();
        TestDbContext.ChangeTracker.Clear();
    }

    [Test]
    public void Should_return_all_ratings_when_read_action()
    {
        var ratingController = new PoseidonApi.Controllers.RatingController(TestDbContext);

        var getAllRatings = ratingController.GetRating();

        Assert.That(getAllRatings.Result.Value!.Count(), Is.EqualTo(2));
    }

    [TestCase(1)]
    [TestCase(20)]
    [Test]
    public void Should_return_expected_curve_point_when_get_rating_by_id(int id)
    {
        var ratingController = new PoseidonApi.Controllers.RatingController(TestDbContext);

        var getRatingById = ratingController.GetRating(id);

        Assert.That(getRatingById.Result.Value!.Id, Is.EqualTo(id));
    }

    [Test]
    public void Should_return_expected_newly_created_rating_when_post()
    {
        var ratingController = new PoseidonApi.Controllers.RatingController(TestDbContext);

        var ratingToAdd = ratingController.PostRating(
            new RatingDTO()
            {
                MoodysRating = "MoodysRating21",
                SandPRating = "SandPRating21",
                FitchRating = "FitchRating21",
                OrderNumber = 21
            });

        Assert.That(ratingToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
        Assert.That(TestDbContext.Ratings.Count(), Is.EqualTo(3));
        Assert.That(TestDbContext.Ratings.ToList()[2].Id, Is.EqualTo(21));
    }

    [Test, Order(1)]
    public void Should_return_expected_updated_rating_when_put()
    {
        var ratingController = new PoseidonApi.Controllers.RatingController(TestDbContext);

        var curvePointToUpdate = ratingController.PutRating(id: 20,
            new RatingDTO()
            {
                Id = 20,
                MoodysRating = "MoodysRatingUpdated",
                SandPRating = "SandPRatingUpdated",
                FitchRating = "FitchRatingUpdated",
                OrderNumber = 20
            }, TestDbContext);

        Assert.That(curvePointToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.Ratings.Count(), Is.EqualTo(2)); //Ensure no POST
        Assert.That(TestDbContext.Ratings.ToList().Find(c => c.Id == 20).MoodysRating,
            Is.EqualTo("MoodysRatingUpdated"));
    }

    [Test]
    public void Should_return_expected_deleted_rating_when_delete()
    {
        var ratingController = new PoseidonApi.Controllers.RatingController(TestDbContext);

        var ratingToDelete = ratingController.DeleteRating(1);

        Assert.That(ratingToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
        Assert.That(TestDbContext.Ratings.Count(), Is.EqualTo(1));
        Assert.That(TestDbContext.Ratings.ToList().Find(c => c.Id == 1), Is.Null);
    }
}
