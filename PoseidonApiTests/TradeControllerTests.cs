using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public class TradeControllerTests
{
    private static ApiDbContext TestDbContext;

    [SetUp]
    public void Init()
    {
        TestDbContext = Setup.FakeTradeDbContext();
        TestDbContext.ChangeTracker.Clear();
    }
    
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

    [Test, Order(1)]
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
