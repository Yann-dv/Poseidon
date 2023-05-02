using PoseidonApi.Models;

namespace PoseidonApi.Data;

public class UserSeed
{
    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any())
            {
                return;   // already seeded
            }

            var usersToSeed = new List<User>
            {
                new User()
                {
                    Fullname = "Main Admin",
                    Username = "Admin",
                    Password = "admin_Admin99$",
                    Role = "Admin"
                },
                new User()
                {
                    Fullname = "Main User",
                    Username = "User",
                    Password = "user_User99$",
                    Role = "User"
                },
                new User()
                {
                    Fullname = "Main Guest",
                    Username = "Guest",
                    Password = "guest_Guest99$",
                    Role = "Guest"
                }
            };

            for (int i = 0; i < usersToSeed.Count; i++)
            {
                context.Users.Add(usersToSeed[i]);
            }

            foreach (User u in usersToSeed)
            {
                context.Users.Add(u);
            }
            await context.SaveChangesAsync();
        }
    }
}
