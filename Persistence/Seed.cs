using System.Globalization;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users = new List<AppUser>{
                    new() {DisplayName="Akhilesh", UserName="akhilesh", Email="akhilesh@test.com"},
                    new() {DisplayName="Manisman", UserName="manisman", Email="manisman@test.com"},
                    new() {DisplayName="Vishal", UserName="vishal", Email="vishal@test.com"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }


            if (context.Activities.Any()) return;
            
            var activities = new List<Activity>
            {
                new() {
                    Title = "England vs New Zealand",
                    Date = DateTime.Parse("10/5/2023"), //DateTime.UtcNow.AddMonths(-2),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Ahmedabad",
                    Venue = "Ahmedabad",
                },
                new() {
                    Title = "Pakistan vs Netherlands",
                    Date = DateTime.Parse("10/6/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Hyderabad",
                    Venue = "Hyderabad",
                },
                new() {
                    Title = "Bangladesh vs Afghanistan (D)",
                    Date = DateTime.Parse("10/7/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Dharamsala",
                    Venue = "Dharamsala",
                },
                new() {
                    Title = "South Africa vs Sri Lanka",
                    Date = DateTime.Parse("10/7/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Delhi",
                    Venue = "Delhi",
                },
                new() {
                    Title = "India vs Australia",
                    Date = DateTime.Parse("10/8/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Chennai",
                    Venue = "Chennai",
                },
                new() {
                    Title = "New Zealand vs Netherlands",
                    Date = DateTime.Parse("10/9/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Hyderabad",
                    Venue = "Hyderabad",
                },
                new() {
                    Title = "England vs Bangladesh (D)",
                    Date = DateTime.Parse("10/10/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Ahmedabad",
                    Venue = "Ahmedabad",
                },
                new() {
                    Title = "Pakistan vs Sri Lanka",
                    Date = DateTime.Parse("10/10/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Dharamsala",
                    Venue = "Dharamsala",
                },
                new() {
                    Title = "India vs Afghanistan",
                    Date = DateTime.Parse("10/11/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Delhi",
                    Venue = "Delhi",
                },
                new() {
                    Title = "Australia vs South Africa",
                    Date = DateTime.Parse("10/12/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Lucknow",
                    Venue = "Lucknow",
                }
            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}