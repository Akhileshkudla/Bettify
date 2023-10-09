using System.Globalization;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Activities.Any()) return;
            
            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "England vs New Zealand",
                    Date = DateTime.Parse("10/5/2023"), //DateTime.UtcNow.AddMonths(-2),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Ahmedabad",
                    Venue = "Ahmedabad",
                },
                new Activity
                {
                    Title = "Pakistan vs Netherlands",
                    Date = DateTime.Parse("10/6/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Hyderabad",
                    Venue = "Hyderabad",
                },
                new Activity
                {
                    Title = "Bangladesh vs Afghanistan (D)",
                    Date = DateTime.Parse("10/7/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Dharamsala",
                    Venue = "Dharamsala",
                },
                new Activity
                {
                    Title = "South Africa vs Sri Lanka",
                    Date = DateTime.Parse("10/7/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Delhi",
                    Venue = "Delhi",
                },
                new Activity
                {
                    Title = "India vs Australia",
                    Date = DateTime.Parse("10/8/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Chennai",
                    Venue = "Chennai",
                },
                new Activity
                {
                    Title = "New Zealand vs Netherlands",
                    Date = DateTime.Parse("10/9/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Hyderabad",
                    Venue = "Hyderabad",
                },
                new Activity
                {
                    Title = "England vs Bangladesh (D)",
                    Date = DateTime.Parse("10/10/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Ahmedabad",
                    Venue = "Ahmedabad",
                },
                new Activity
                {
                    Title = "Pakistan vs Sri Lanka",
                    Date = DateTime.Parse("10/10/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Dharamsala",
                    Venue = "Dharamsala",
                },
                new Activity
                {
                    Title = "India vs Afghanistan",
                    Date = DateTime.Parse("10/11/2023"),
                    Description = "2:00 PM",
                    Category = "cricket",
                    City = "Delhi",
                    Venue = "Delhi",
                },
                new Activity
                {
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