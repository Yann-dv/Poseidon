using Microsoft.EntityFrameworkCore;
using PoseidonApi.Models;

namespace PoseidonApiTests;

public class Setup
{
    public static ApiDbContext FakeDbContext ()
        {
            
            var FakeDContext = new ApiDbContext(new DbContextOptionsBuilder<ApiDbContext>()
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
                },
                new Bid()
                {
                    Id = 13748,
                    Status = "Live",
                    BidListDate = DateTime.Now,
                    Account = "TestAcocount",
                    Type = "TestType1",
                    BidQuantity = 1,
                    AskQuantity = 1,
                    BidValue = 1,
                    Ask = 1,
                    Benchmark = "TestBenchmark1",
                    Commentary = "TestCommentary1",
                    Security = "TestSecurity1",
                    Trader = "TestTrader1",
                    Book = "Book1",
                    CreationName = "TestCreationName1",
                    CreationDate = DateTime.Now,
                    RevisionName = "TestRevisionName1",
                    RevisionDate = DateTime.Now,
                    DealName = "TestDealName1",
                    DealType = "TestDealType1",
                    SourceListId = "TestSourceListId1",
                    Side = "TestSide1"
                }
            };

            for (int i = 0; i < bidList.Count; i++)
            {
                FakeDContext.Bids.Add(bidList[i]);
            }

            FakeDContext.SaveChangesAsync();
            return FakeDContext;
        }
}