using PoseidonApi.Models;

namespace PoseidonApi.Data;

public class BidSeed
{
    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
            await context.Database.EnsureCreatedAsync();

            if (context.Bids.Any())
            {
                return;   // already seeded
            }

            var bidsToSeed = new List<Bid>
            {
                new Bid() {
                    Account = "Account",
                    Type = "Type",
                    BidQuantity = 20,
                    AskQuantity = 30,
                    BidValue = 10,
                    Ask = 1,
                    Benchmark = "Benchmark",
                    BidListDate = DateTime.Now,
                    Commentary = "Commentary",
                    Security = "Security",
                    Status = "Status",
                    Trader = "Trader",
                    Book = "Book",
                    CreationName = "CreationName",
                    CreationDate = DateTime.Now,
                    RevisionName = "RevisionName",
                    RevisionDate = DateTime.Now,
                    DealName = "DealName",
                    DealType = "DealType",
                    SourceListId = "SourceListId",
                    Side = "Side",
                },
                new Bid() {
                    Account = "Account2",
                    Type = "Type2",
                    BidQuantity = 210,
                    AskQuantity = 300,
                    BidValue = 20,
                    Ask = 2,
                    Benchmark = "Benchmark2",
                    BidListDate = DateTime.Now.AddDays(1),
                    Commentary = "Commentary2",
                    Security = "Security2",
                    Status = "Status2",
                    Trader = "Trader2",
                    Book = "Book2",
                    CreationName = "CreationName2",
                    CreationDate = DateTime.Now.AddDays(1),
                    RevisionName = "RevisionName",
                    RevisionDate = DateTime.Now,
                    DealName = "DealName2",
                    DealType = "DealType2",
                    SourceListId = "SourceListId2",
                    Side = "Side2",
                },
            };

            foreach (Bid b in bidsToSeed)
            {
                context.Bids.Add(b);
            }
            await context.SaveChangesAsync();
        }
    }
}