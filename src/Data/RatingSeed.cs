using PoseidonApi.Models;

namespace PoseidonApi.Data;

public class RatingSeed
{

    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
            await context.Database.EnsureCreatedAsync();

            if (!context.Ratings.Any())
            {
                var ratingsToSeed = new List<Rating>
                {
                    new Rating()
                    {
                        OrderNumber = 1,
                        MoodysRating = "MoodysRating",
                        SandPRating = "SandPRating",
                        FitchRating = "FitchRating"
                    },
                    new Rating()
                    {
                        OrderNumber = 2,
                        MoodysRating = "MoodysRating",
                        SandPRating = "SandPRating",
                        FitchRating = "FitchRating"
                    },
                };

                for (int i = 0; i < ratingsToSeed.Count; i++)
                {
                    context.Ratings.Add(ratingsToSeed[i]);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}