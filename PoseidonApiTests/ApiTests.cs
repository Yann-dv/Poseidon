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
            
            var result = authController.Login(new PoseidonApi.Models.LoginModel()
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
            
            var FakeDB = new ApiDbContext( new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "FakeDb")
                .Options);

            var bidList = new List<Bid>()
            {
                new Bid()
                {
                    Id = 1,
                    Status = "Live",
                    BidListDate = DateTime.Now,
                    Account = "Account1",
                    Type = "Type1",
                    BidQuantity = 1,
                    AskQuantity = 1,
                    BidValue = 1,
                    Ask = 1,
                    Benchmark = "Benchmark1",
                    Commentary = "Commentary1",
                    Security = "Security1",
                    Trader = "Trader1",
                    Book = "Book1",
                    CreationName = "CreationName1",
                    CreationDate = DateTime.Now,
                    RevisionName = "RevisionName1",
                    RevisionDate = DateTime.Now,
                    DealName = "DealName1",
                    DealType = "DealType1",
                    SourceListId = "SourceListId1",
                    Side = "Side1"
                }
            };
            
            for (int i = 0; i < bidList.Count; i++)
            {
                FakeDB.Bids.Add(bidList[i]);
            }
            FakeDB.SaveChangesAsync();

            var bidController = new PoseidonApi.Controllers.BidController(FakeDB);

            var getBids = bidController.GetBids();

            Assert.That(getBids.Result.Value.Count(), Is.EqualTo(1));
        }
    }
}