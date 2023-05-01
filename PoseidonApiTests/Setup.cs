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
            
            
            var curvePointList = new List<CurvePoint>()
            {
                new CurvePoint()
                {
                    Id = 1,
                    CurveId = 1,
                    Term = 1,
                    Value = 1,
                    CreationDate = DateTime.Now
                },
                new CurvePoint()
                {
                    Id = 12,
                    CurveId = 12,
                    Term = 12,
                    Value = 12,
                    CreationDate = DateTime.Now
                }
            };  

            for (int i = 0; i < curvePointList.Count; i++)
            {
                FakeDContext.CurvePoints.Add(curvePointList[i]);
            }

            FakeDContext.SaveChangesAsync();
            
            var ratingList = new List<Rating>()
            {
                new Rating()
                {
                    Id = 1,
                    MoodysRating = "MoodysRating1",
                    SandPRating = "SandPRating1",
                    FitchRating = "FitchRating1",
                    OrderNumber = 1
                },
                new Rating()
                {
                    Id = 20,
                    MoodysRating = "MoodysRating2",
                    SandPRating = "SandPRating2",
                    FitchRating = "FitchRating2",
                    OrderNumber = 20
                }
            };  

            for (int i = 0; i < ratingList.Count; i++)
            {
                FakeDContext.Ratings.Add(ratingList[i]);
            }

            FakeDContext.SaveChangesAsync();
            
            var ruleList = new List<Rule>()
            {
                new Rule()
                {
                    Id = 1,
                    Description = "Description1",
                    Json = "Json1",
                    Name = "Name1",
                    Template = "Template1",
                    SqlStr = "SqlStr1",
                    SqlPart = "SqlPart1"
                },
                new Rule()
                {
                    Id = 20,
                    Description = "Description2",
                    Json = "Json2",
                    Name = "Name2",
                    Template = "Template2",
                    SqlStr = "SqlStr2",
                    SqlPart = "SqlPart2"
                }
            };  

            for (int i = 0; i < ruleList.Count; i++)
            {
                FakeDContext.Rules.Add(ruleList[i]);
            }

            FakeDContext.SaveChangesAsync();


            var tradeList = new List<Trade>()
            {
                new Trade()
                {
                    Id = 1,
                    Account = "Account1",
                    Type = "Type1",
                    BuyQuantity = 1,
                    SellQuantity = 1,
                    BuyPrice = 1,
                    SellPrice = 1,
                    Benchmark = "Benchmark1",
                    TradeDate = DateTime.Now,
                    Security = "Security1",
                    Status = "Status1",
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
                new Trade()
                {
                    Id = 2,
                    Account = "Account2",
                    Type = "Type2",
                    BuyQuantity = 2,
                    SellQuantity = 2,
                    BuyPrice = 2,
                    SellPrice = 2,
                    Benchmark = "Benchmark2",
                    TradeDate = DateTime.Now,
                    Security = "Security2",
                    Status = "Status2",
                    Trader = "Trader2",
                    Book = "Book2",
                    CreationName = "CreationName2",
                    CreationDate = DateTime.Now,
                    RevisionName = "RevisionName2",
                    RevisionDate = DateTime.Now,
                    DealName = "DealName2",
                    DealType = "DealType2",
                    SourceListId = "SourceListId2",
                    Side = "Side2"
                }
            };
            
            for (int i = 0; i < tradeList.Count; i++)
            {
                FakeDContext.Trades.Add(tradeList[i]);
            }

            FakeDContext.SaveChangesAsync();

            var userList = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Username = "Username1",
                    Password = "Password1",
                    Fullname = "Fullname1",
                    Role = "Role1"
                },
                new User()
                {
                    Id = 2,
                    Username = "Username2",
                    Password = "Password2",
                    Fullname = "Fullname2",
                    Role = "Role2"
                }
            };
            
            for (int i = 0; i < userList.Count; i++)
            {
                FakeDContext.Users.Add(userList[i]);
            }
            
            FakeDContext.SaveChangesAsync();
            
            return FakeDContext;
        }
}