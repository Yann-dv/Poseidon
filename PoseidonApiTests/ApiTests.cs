using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Models;
using Console = System.Console;

namespace PoseidonApiTests;

public abstract class ApiTests
{
    private static readonly ApiDbContext TestDbContext = Setup.FakeDbContext();
    

    [TestFixture]
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

    [TestFixture]
    public class BidControllerTests
    {
        [Test]
        public void Should_return_all_bids_when_read_action()
        {

            var bidController = new PoseidonApi.Controllers.BidController(TestDbContext);

            var getAllBids = bidController.GetBids();

            Assert.That(getAllBids.Result.Value!.Count(), Is.EqualTo(2));
        }

        [TestCase(1)]
        [TestCase(13748)]
        [Test]
        public void Should_return_expected_bid_when_get_bid_by_id(int id)
        {
            var bidController = new PoseidonApi.Controllers.BidController(TestDbContext);

            var getBidById = bidController.GetBid(id);

            Assert.That(getBidById.Result.Value!.Id, Is.EqualTo(id));
        }

        [Test]
        public void Should_return_expected_newly_created_bid_when_post()
        {
            var bidController = new PoseidonApi.Controllers.BidController(TestDbContext);

            var bidToAdd = bidController.PostBid(
                new BidDTO()
                {
                    Status = "Testing",
                    BidListDate = DateTime.Now,
                    Account = "TestAcocount",
                    Type = "TestType2",
                    BidQuantity = 1,
                    AskQuantity = 1,
                    BidValue = 1,
                    Ask = 1,
                    Benchmark = "TestBenchmark2",
                    Commentary = "TestCommentary2",
                    Security = "TestSecurity2",
                    Trader = "TestTrader2",
                    Book = "Book2",
                    CreationName = "TestCreationName2",
                    CreationDate = DateTime.Now,
                    RevisionName = "TestRevisionName2",
                    RevisionDate = DateTime.Now,
                    DealName = "TestDealName2",
                    DealType = "TestDealType2",
                    SourceListId = "TestSourceListId2",
                    Side = "TestSide2"
                });

            Assert.That(bidToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
            Assert.That(TestDbContext.Bids.Count(), Is.EqualTo(3));
            Assert.That(TestDbContext.Bids.ToList()[2].Id, Is.EqualTo(13749));
        }

        [Test]
        public void Should_return_expected_updated_bid_when_put()
        {
            var bidController = new PoseidonApi.Controllers.BidController(TestDbContext);

            var bidToUpdate = bidController.PutBid(id: 1,
                new BidDTO()
                {
                    Id = 1,
                    Status = "Updated",
                    BidListDate = DateTime.Now,
                    Account = "UpdatedAccount1",
                    Type = "UpdatedType1",
                    BidQuantity = 1,
                    AskQuantity = 1,
                    BidValue = 1,
                    Ask = 1,
                    Benchmark = "UpdatedBenchmark1",
                    Commentary = "UpdatedCommentary1",
                    Security = "UpdatedSecurity1",
                    Trader = "UpdatedTrader1",
                    Book = "UpdatedBook1",
                    CreationName = "UpdatedCreationName1",
                    CreationDate = DateTime.Now,
                    RevisionName = "UpdatedRevisionName1",
                    RevisionDate = DateTime.Now,
                    DealName = "UpdatedDealName1",
                    DealType = "UpdatedDealType1",
                    SourceListId = "UpdatedSourceListId1",
                    Side = "UpdatedSide1"
                }, TestDbContext);

            Assert.That(bidToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
            Assert.That(TestDbContext.Bids.Count(), Is.EqualTo(2)); //Ensure no POST
            Assert.That(TestDbContext.Bids.ToList().Find(c => c.Id == 1).Status, Is.EqualTo("Updated"));
        }

        [Test]
        public void Should_return_expected_deleted_bid_when_delete()
        {
            var bidController = new PoseidonApi.Controllers.BidController(TestDbContext);

            var bidToDelete = bidController.DeleteBid(1);

            Assert.That(bidToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
            Assert.That(TestDbContext.Bids.Count(), Is.EqualTo(1));
            Assert.That(TestDbContext.Bids.ToList().Find(c => c.Id == 1), Is.Null);
        }

    }

    [TestFixture]
    public class CurvePointControllerTests
    {
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

        [Test]
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

    [TestFixture]
    public class RatingControllerTests
    {
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
        
        [Test]
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
            Assert.That(TestDbContext.Ratings.ToList().Find(c => c.Id == 20).MoodysRating, Is.EqualTo("MoodysRatingUpdated"));
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

    [TestFixture]
    public class RuleControllerTests
    {
        [Test]
        public void Should_return_all_rules_when_read_action()
        {
            var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

            var getAllRules = ruleController.GetRule();

            Assert.That(getAllRules.Result.Value!.Count(), Is.EqualTo(2));
        }
        
        [TestCase(1)]
        [TestCase(20)]
        [Test]
        public void Should_return_expected_curve_point_when_get_rule_by_id(int id)
        {
            var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

            var getRuleById = ruleController.GetRule(id);

            Assert.That(getRuleById.Result.Value!.Id, Is.EqualTo(id));
        }
        
        [Test]
        public void Should_return_expected_newly_created_rule_when_post()
        {
            var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

            var ruleToAdd = ruleController.PostRule(
                new RuleDTO()
                {
                    Description = "Description21",
                    Json = "Json21",
                    SqlStr = "SqlStr21",
                    SqlPart = "SqlPart21"
                });

            Assert.That(ruleToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
            Assert.That(TestDbContext.Rules.Count(), Is.EqualTo(3));
            Assert.That(TestDbContext.Rules.ToList()[2].Id, Is.EqualTo(21));
        }
        
        [Test]
        public void Should_return_expected_updated_rule_when_put()
        {
            var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

            var ruleToUpdate = ruleController.PutRule(id: 20,
                new RuleDTO()
                {
                    Id = 20,
                    Description = "DescriptionUpdated",
                    Json = "JsonUpdated",
                    SqlStr = "SqlStrUpdated",
                    SqlPart = "SqlPartUpdated"
                }, TestDbContext);

            Assert.That(ruleToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
            Assert.That(TestDbContext.Rules.Count(), Is.EqualTo(2)); //Ensure no POST
            Assert.That(TestDbContext.Rules.ToList().Find(c => c.Id == 20).Description, Is.EqualTo("DescriptionUpdated"));
        }
        
        [Test]
        public void Should_return_expected_deleted_rule_when_delete()
        {
            var ruleController = new PoseidonApi.Controllers.RuleController(TestDbContext);

            var ruleToDelete = ruleController.DeleteRule(1);

            Assert.That(ruleToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
            Assert.That(TestDbContext.Rules.Count(), Is.EqualTo(1));
            Assert.That(TestDbContext.Rules.ToList().Find(c => c.Id == 1), Is.Null);
        }
    }

    [TestFixture]
    public class TradeControllerTests
    {
        [Test]
        public void Should_return_all_trades_when_read_action()
        {
            var tradeController = new PoseidonApi.Controllers.TradeController(TestDbContext);

            var getAllTrades = tradeController.GetTrades();

            Assert.That(getAllTrades.Result.Value!.Count(), Is.EqualTo(2));
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [Test]
        public void Should_return_expected_curve_point_when_get_trade_by_id(int id)
        {
            var tradeController = new PoseidonApi.Controllers.TradeController(TestDbContext);

            var getTradeById = tradeController.GetTrade(id);

            Assert.That(getTradeById.Result.Value!.Id, Is.EqualTo(id));
        }
        
        [Test]
        public void Should_return_expected_newly_created_trade_when_post()
        {
            var tradeController = new PoseidonApi.Controllers.TradeController(TestDbContext);

            var tradeToAdd = tradeController.PostTrade(
                new TradeDTO()
                {
                    Id = 3,
                    Account = "Account3",
                    Type = "Type3",
                    BuyQuantity = 3,
                    SellQuantity = 3,
                    BuyPrice = 3,
                    SellPrice = 3,
                    Benchmark = "Benchmark3",
                    TradeDate = DateTime.Now,
                    Security = "Security3",
                    Status = "Status3",
                    Trader = "Trader3",
                    Book = "Book3",
                    CreationName = "CreationName3",
                    CreationDate = DateTime.Now,
                    RevisionName = "RevisionName3",
                    RevisionDate = DateTime.Now,
                    DealName = "DealName3",
                    DealType = "DealType3",
                    SourceListId = "SourceListId3",
                    Side = "Side3"
                });

            Assert.That(tradeToAdd.Result.Result!.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
            Assert.That(TestDbContext.Trades.Count(), Is.EqualTo(3));
            Assert.That(TestDbContext.Trades.ToList()[2].Id, Is.EqualTo(3));
        }
        
        [Test]
        public void Should_return_expected_updated_trade_when_put()
        {
            var tradeController = new PoseidonApi.Controllers.TradeController(TestDbContext);

            var tradeToUpdate = tradeController.PutTrade(id: 2,
                new TradeDTO()
                {
                    Id = 2,
                    Account = "AccountUpdated",
                    Type = "TypeUpdated",
                    BuyQuantity = 2,
                    SellQuantity = 2,
                    BuyPrice = 2,
                    SellPrice = 2,
                    Benchmark = "BenchmarkUpdated",
                    TradeDate = DateTime.Now,
                    Security = "SecurityUpdated",
                    Status = "StatusUpdated",
                    Trader = "TraderUpdated",
                    Book = "BookUpdated",
                    CreationName = "CreationNameUpdated",
                    CreationDate = DateTime.Now,
                    RevisionName = "RevisionNameUpdated",
                    RevisionDate = DateTime.Now,
                    DealName = "DealNameUpdated",
                    DealType = "DealTypeUpdated",
                    SourceListId = "SourceListIdUpdated",
                    Side = "SideUpdated"
                }, TestDbContext);

            Assert.That(tradeToUpdate.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
            Assert.That(TestDbContext.Trades.Count(), Is.EqualTo(2)); //Ensure no POST
            Assert.That(TestDbContext.Trades.ToList().Find(c => c.Id == 2).Account, Is.EqualTo("AccountUpdated"));
        }
        
        [Test]
        public void Should_return_expected_deleted_trade_when_delete()
        {
            var tradeController = new PoseidonApi.Controllers.TradeController(TestDbContext);

            var tradeToDelete = tradeController.DeleteTrade(1);

            Assert.That(tradeToDelete.Result.GetType(), Is.EqualTo(typeof(NoContentResult)));
            Assert.That(TestDbContext.Trades.Count(), Is.EqualTo(1));
            Assert.That(TestDbContext.Trades.ToList().Find(c => c.Id == 1), Is.Null);
        }
    }

    [TestFixture]
    public class UserControllerTests
    {
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
        
        [Test]
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
}
