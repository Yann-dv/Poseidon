using PoseidonApi.Models;

namespace PoseidonApi.Data;

public class TradeSeed
{
    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
            await context.Database.EnsureCreatedAsync();

            if (!context.Trades.Any())
            {
                var tradesToSeed = new List<Trade>
                {
                    new Trade() {
                        Account = "GuestAccount",
                        Type = "Guest",
                        BuyQuantity = 500,
                        SellQuantity = 200,
                        BuyPrice = 10,
                        SellPrice = 15,
                        Benchmark = "Benchmark",
                        TradeDate = DateTime.Now,
                        Security = "Low",
                        Status = "Pending",
                        Trader = "TradingInc",
                        Book = "Book1",
                        CreationName = "CreationName",
                        CreationDate = DateTime.Now,
                        RevisionName = "SampleRevisionName",
                        RevisionDate = DateTime.Now,
                        DealName = "SampleDealName",
                        DealType = "SampleDealType",
                        SourceListId = "SampleSourceListId",
                        Side = "SampleSide",
                    },
                };

                for (int i = 0; i < tradesToSeed.Count; i++)
                {
                    context.Trades.Add(tradesToSeed[i]);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}