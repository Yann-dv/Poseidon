using PoseidonApi.Models;

namespace PoseidonApi.Data;

public class RuleSeed
{

    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
            await context.Database.EnsureCreatedAsync();

            if (context.Rules.Any())
            {
                return;   // already seeded
            }

            var rulesToSeed = new List<Rule>
            {
                new Rule()
                {
                    Name = "Rule 1",
                    Description = "Description",
                    Template = "Template",
                    Json = "Json",
                    SqlStr = "SqlStr",
                    SqlPart = "SqlPart"
                },
                new Rule()
                {
                    Name = "Rule 2",
                    Description = "Description",
                    Template = "Template",
                    Json = "Json",
                    SqlStr = "SqlStr",
                    SqlPart = "SqlPart"
                },
            };

            for (int i = 0; i < rulesToSeed.Count; i++)
            {
                context.Rules.Add(rulesToSeed[i]);
            }

            foreach (Rule r in rulesToSeed)
            {
                context.Rules.Add(r);
            }
            await context.SaveChangesAsync();
        }
    }
}

