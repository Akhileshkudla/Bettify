using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Activities.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Vishal",
                        UserName = "vishal",
                        Email = "vishal@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Prakash",
                        UserName = "prakash",
                        Email = "prakash@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Manisman",
                        UserName = "manisman",
                        Email = "manisman@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Almighty Admin",
                        UserName = "admin",
                        Email = "admin@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "ENG vs NZ",
                        Date = DateTime.UtcNow.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "cricket",
                        City = "Ahmedabad",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"ENG", "NZ"}
                    },
                    new Activity
                    {
                        Title = "PAK vs NTL",
                        Date = DateTime.UtcNow.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Category = "cricket",
                        City = "Hyderabad",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"PAK", "NTL"}
                    },
                    new Activity
                    {
                        Title = "AFG vs BANG",
                        Date = DateTime.UtcNow.AddMonths(1),
                        Description = "Activity 1 month in future",
                        Category = "cricket",
                        City = "Dharmsala",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=false,
                        WinningOption="",
                        Options = new []{"AFG", "BANG"}
                    },
                    new Activity
                    {
                        Title = "IND vs PAK",
                        Date = DateTime.UtcNow.AddMonths(2),
                        Description = "Activity 2 months in future",
                        Category = "cricket",
                        City = "Ahmedabad",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = false
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"IND", "PAK"}
                    },
                    new Activity
                    {
                        Title = "SA vs AFG",
                        Date = DateTime.UtcNow.AddMonths(3),
                        Description = "Activity 3 months in future",
                        Category = "cricket",
                        City = "Delhi",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[0],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"SA", "AFG"}
                    },
                    new Activity
                    {
                        Title = "IND vs SRI",
                        Date = DateTime.UtcNow.AddMonths(4),
                        Description = "Activity 4 months in future",
                        Category = "cricket",
                        City = "Pune",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"IND", "SRI"}
                    },
                    new Activity
                    {
                        Title = "AUS vs PAK",
                        Date = DateTime.UtcNow.AddMonths(5),
                        Description = "Activity 5 months in future",
                        Category = "cricket",
                        City = "Wankhede",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"AUS", "PAK"}
                    },
                    new Activity
                    {
                        Title = "IND vs AUS",
                        Date = DateTime.UtcNow.AddMonths(6),
                        Description = "Activity 6 months in future",
                        Category = "cricket",
                        City = "Chennai",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"IND", "AUS"}
                    },
                    new Activity
                    {
                        Title = "BNG vs PAK",
                        Date = DateTime.UtcNow.AddMonths(7),
                        Description = "Activity 7 months in future",
                        Category = "cricket",
                        City = "Eden Gardens",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[2],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"BNG", "PAK"}
                    },
                    new Activity
                    {
                        Title = "SRI vs IND",
                        Date = DateTime.UtcNow.AddMonths(8),
                        Description = "Activity 8 months in future",
                        Category = "cricket",
                        City = "Lucknow",
                        Venue = "Something stadium",
                        Attendees = new List<ActivityAttendee>
                        {
                            new ActivityAttendee
                            {
                                AppUser = users[3],
                                IsHost = true                            
                            },
                            new ActivityAttendee
                            {
                                AppUser = users[1],
                                IsHost = false                            
                            },
                        },
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"SRI", "IND"}
                    }
                };

                await context.Activities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}
