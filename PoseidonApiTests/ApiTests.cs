using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public abstract class ApiTests
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        public void Should_return_ok_status_when_expected_authorized_user_login()
        {
            var authController = new PoseidonApi.Controllers.AuthController();

            var result = authController.Login(new PoseidonApi.Models.LoginModel()
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
        private readonly ApiDbContext _dbContext;

        [Test]
        public void Should_return_all_bids_when_read_action()
        {

            var dbContext = Setup.FakeDbContext();

            var bidController = new PoseidonApi.Controllers.BidController(dbContext);

            var getAllBids = bidController.GetBids();

            Assert.That(getAllBids.Result.Value.Count(), Is.EqualTo(2));
        }

        [TestCase(1)]
        [TestCase(13748)]
        [Test]
        public void Should_return_expected_bid_when_get_bid_by_id(int id)
        {
            var dbContext = Setup.FakeDbContext();

            var bidController = new PoseidonApi.Controllers.BidController(dbContext);

            var getBidById = bidController.GetBid(id);

            Assert.That(getBidById.Result.Value.Id, Is.EqualTo(id));
        }
        
        [Test]
        public void Should_return_expected_newly_created_bid_when_post()
        {
            var dbContext = Setup.FakeDbContext();

            var bidController = new PoseidonApi.Controllers.BidController(dbContext);

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
            
            Assert.That(bidToAdd.Result.Result.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
            Assert.That(dbContext.Bids.Count(), Is.EqualTo(3));
            Assert.That(dbContext.Bids.ToList()[2].Id, Is.EqualTo(13749));
        }

        [Test]
        public void Should_return_expected_updated_bid_when_put()
        {
            var dbContext = Setup.FakeDbContext();

            var bidController = new PoseidonApi.Controllers.BidController(dbContext);

            var bidToUpdate = bidController.PutBid(1,
                new BidDTO()
                {
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
                });
            Assert.That(bidToUpdate.Result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
            Assert.That(dbContext.Bids.Count(), Is.EqualTo(2)); //Ensure no POST
            Assert.That(dbContext.Bids.ToList()[0].Status, Is.EqualTo("Updated"));
        }
    }
}