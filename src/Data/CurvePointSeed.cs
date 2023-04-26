using PoseidonApi.Models;

namespace PoseidonApi.Data;

public class CurvePointSeed
{

    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
            await context.Database.EnsureCreatedAsync();

            if (!context.CurvePoints.Any())
            {
                var curvePointsToSeed = new List<CurvePoint>
                {
                    new CurvePoint() {
                        Term = 1,
                        Value = 10,
                        CreationDate = DateTime.Now,
                        AsOfDate = DateTime.Now,
                        CurveId = 1
                        },
                    new CurvePoint() {
                        Term = 2,
                        Value = 20,
                        CreationDate = DateTime.Now,
                        AsOfDate = DateTime.Now,
                        CurveId = 1
                        },
                    new CurvePoint() {
                        Term = 3,
                        Value = 30,
                        CreationDate = DateTime.Now,
                        AsOfDate = DateTime.Now,
                        CurveId = 1
                        },
                    };

                for (int i = 0; i < curvePointsToSeed.Count; i++)
                {
                    context.CurvePoints.Add(curvePointsToSeed[i]);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}