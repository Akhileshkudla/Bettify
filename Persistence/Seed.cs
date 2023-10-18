using System.Globalization;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static DateTime ConvertLocalToUtc(string localTimeString)
        {
            // Parse the local time string to a DateTime object.
            DateTime localTime = DateTime.ParseExact(localTimeString, "dd/MM/yyyy hh:mmtt", CultureInfo.InvariantCulture);

            // Get the local time zone.
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            // Convert the local time zone to UTC.
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, localTimeZone);

            // Return the converted UTC DateTime object.
            return utcTime;
        }

        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Activities.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Vishal Kulkarni",
                        UserName = "vishal",
                        Email = "vishal@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="a7emsulkfvfbn6blvgm2",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540647/a7emsulkfvfbn6blvgm2.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Phani Kasum",
                        UserName = "phani",
                        Email = "phani@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="ga7mp2zpvbviopxjo48h",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540647/ga7mp2zpvbviopxjo48h.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Soumyashree R",
                        UserName = "soumya",
                        Email = "soumya@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="soowhu9o6bxcgpnlpbzp",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540647/soowhu9o6bxcgpnlpbzp.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Riddhi Shah",
                        UserName = "riddhi",
                        Email = "riddhi@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="zsul2q1vy6rmdhdlnrhz",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540647/zsul2q1vy6rmdhdlnrhz.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Shriya Devadiga",
                        UserName = "shriya",
                        Email = "shriya@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="pqnnrifzshado96ubfqq",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540646/pqnnrifzshado96ubfqq.jpg"
                            }
                        }
                    },                    
                    new AppUser
                    {
                        DisplayName = "Shatakshi Keshari",
                        UserName = "shatakshi",
                        Email = "shatakshi@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="beg2ntalve0lf8qq9tur",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540646/beg2ntalve0lf8qq9tur.jpg"
                            }
                        }
                    },                    
                    new AppUser
                    {
                        DisplayName = "Maheshwar Behera",
                        UserName = "maheshwar",
                        Email = "maheshwar@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="xhix4rdkjgytjjvqjnrc",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540647/xhix4rdkjgytjjvqjnrc.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Priya Verma",
                        UserName = "priyaverma",
                        Email = "priyaverma@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="lpeemwcd3ukq7o4gfyrn",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/lpeemwcd3ukq7o4gfyrn.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Abhishek Kumar",
                        UserName = "abhishek",
                        Email = "abhishek@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="scf6m70q6mrqvolihacb",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/scf6m70q6mrqvolihacb.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Vishal Kangante",
                        UserName = "vishalkangante",
                        Email = "vishalkangante@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="k2mno6c5knzb24on4aan",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/k2mno6c5knzb24on4aan.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Newton Sheikh",
                        UserName = "newton",
                        Email = "newton@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="n3g5klje2tzhm6ei0axs",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/n3g5klje2tzhm6ei0axs.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Amit Jaiswal",
                        UserName = "amit",
                        Email = "amit@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="iz0cwryuskqezncw4kvw",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/iz0cwryuskqezncw4kvw.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Laya K P",
                        UserName = "laya",
                        Email = "laya@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="zgmyp9ajrbxtbsc96tdv",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/zgmyp9ajrbxtbsc96tdv.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Lavanya K C",
                        UserName = "lavanya",
                        Email = "lavanya@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="ws0lih7fdmgtzhupio4w",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/ws0lih7fdmgtzhupio4w.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Rahul Patel",
                        UserName = "rahul",
                        Email = "rahul@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="lsoknceeir2yujdavrw1",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540646/lsoknceeir2yujdavrw1.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Akhilesh K",
                        UserName = "akhi",
                        Email = "akhi@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="djeqj9ny9ddpwjc4hr9y",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540646/djeqj9ny9ddpwjc4hr9y.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Manisman Parida",
                        UserName = "manisman",
                        Email = "manisman@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="cf8qcfxat52fs8pnzijt",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/cf8qcfxat52fs8pnzijt.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Jeevaraj Kamashetti",
                        UserName = "jeevaraj",
                        Email = "jeevaraj@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="k4scouamc3udqvutfwtz",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/k4scouamc3udqvutfwtz.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Royal Prakash",
                        UserName = "prakash",
                        Email = "prakash@bet.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="ghiarjddhztuzrbtc71s",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697540645/ghiarjddhztuzrbtc71s.jpg"
                            }
                        }
                    },
                    new AppUser
                    {
                        DisplayName = "Almighty Admin",
                        UserName = "admin",
                        Email = "admin@admin.com",
                        Photos = new List<Photo> 
                        { 
                            new Photo 
                            {
                                Id="eunlknuw7qpiwf2r11eu",
                                IsMain=true,
                                Url="https://res.cloudinary.com/dtpzeuru1/image/upload/v1697541405/eunlknuw7qpiwf2r11eu.jpg"
                            }
                        }                   
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
                        Title = "England vs New Zealand",
                        Date = ConvertLocalToUtc("05/10/2023 02:00pm"),
                        Description = "New Zealand won by 9 wkts",
                        Category = "cricket",
                        City = "Ahmedabad",
                        Venue = "Narendra Modi Stadium",
                        Attendees = GetAllAttendees(users, "NZ"),
                        AmountIfLose = 100,
                        AmountIfWon=0,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"ENG", "NZ"}
                    },
                    new Activity
                    {
                        Title = "Pakistan vs Netherlands",
                        Date = ConvertLocalToUtc("06/10/2023 02:00pm"),
                        Description = "New Zealand won by 9 wkts",
                        Category = "cricket",
                        City = "Hyderabad",
                        Venue = "Rajiv Gandhi International Stadium",
                        Attendees = GetAllAttendees(users, "PAK"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"PAK", "NL"}
                    },
                    new Activity
                    {
                        Title = "Afghanistan vs Bangladesh",
                        Date = ConvertLocalToUtc("07/10/2023 10:30am"),
                        Description = "Bangladesh won by 6 wkts",
                        Category = "cricket",
                        City = "Dharamsala",
                        Venue = "Himachal Pradesh Cricket Association Stadium",
                        Attendees = GetAllAttendees(users, "BAN"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"AFG", "BAN"}
                    },
                    new Activity
                    {
                        Title = "South Africa vs Sri Lanka",
                        Date = ConvertLocalToUtc("07/10/2023 02:00pm"),
                        Description = "South Africa won by 102 runs",
                        Category = "cricket",
                        City = "Delhi",
                        Venue = "Arun Jaitley Stadium",
                        Attendees = GetAllAttendees(users, "SA"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"SA", "SL"}
                    },
                    new Activity
                    {
                        Title = "Australia vs India",
                        Date = ConvertLocalToUtc("08/10/2023 02:00pm"),
                        Description = "India won by 6 wkts",
                        Category = "cricket",
                        City = "Chennai",
                        Venue = "MA Chidambaram Stadium",
                        Attendees = GetAllAttendees(users, "IND"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"AUS", "IND"}
                    },
                    new Activity
                    {
                        Title = "New Zealand vs Netherlands",
                        Date = ConvertLocalToUtc("09/10/2023 02:00pm"),
                        Description = "New Zealand won by 99 runs",
                        Category = "cricket",
                        City = "Hyderabad",
                        Venue = "Rajiv Gandhi International Stadium",
                        Attendees = GetAllAttendees(users, "NZ"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"NZ", "NTL"}
                    },
                    new Activity
                    {
                        Title = "England vs Bangladesh",
                        Date = ConvertLocalToUtc("10/10/2023 10:30am"),
                        Description = "England won by 137 runs",
                        Category = "cricket",
                        City = "Dharamsala",
                        Venue = "Himachal Pradesh Cricket Association Stadium",
                        Attendees = GetAllAttendees(users, "ENG"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"ENG", "BAN"}
                    },
                    new Activity
                    {
                        Title = "Sri Lanka vs Pakistan",
                        Date = ConvertLocalToUtc("10/10/2023 02:00pm"),
                        Description = "Pakistan won by 6 wkts",
                        Category = "cricket",
                        City = "Hyderabad",
                        Venue = "Rajiv Gandhi International Stadium",
                        Attendees = GetAllAttendees(users, "PAK"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"SL", "PAK"}
                    },
                    new Activity
                    {
                        Title = "Afghanistan vs India",
                        Date = ConvertLocalToUtc("11/10/2023 02:00pm"),
                        Description = "India won by 8 wkts",
                        Category = "cricket",
                        City = "Delhi",
                        Venue = "Arun Jaitley Stadium",
                        Attendees = GetAllAttendees(users, "IND"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"AFG", "IND"}
                    },
                    new Activity
                    {
                        Title = "South Africa vs Australia",
                        Date = ConvertLocalToUtc("12/10/2023 02:00pm"),
                        Description = "South Africa won by 134 runs",
                        Category = "cricket",
                        City = "Lucknow",
                        Venue = "Bharat Ratna Shri Atal Bihari Vajpayee Ekana Cricket Stadium",
                        Attendees = GetAllAttendees(users, "SA"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"SA", "AUS"}
                    },
                    new Activity
                    {
                        Title = "Bangladesh vs New Zealand",
                        Date = ConvertLocalToUtc("13/10/2023 02:00pm"),
                        Description = "New Zealand won by 8 wkts",
                        Category = "cricket",
                        City = "Chennai",
                        Venue = "MA Chidambaram Stadium",
                        Attendees = GetAllAttendees(users, "NZ"),
                        AmountIfLose = 70,
                        AmountIfWon=20,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"BAN", "NZ"}
                    },
                    new Activity
                    {
                        Title = "Pakistan vs India",
                        Date = ConvertLocalToUtc("14/10/2023 02:00pm"),
                        Description = "India won by 7 wkts",
                        Category = "cricket",
                        City = "Ahmedabad",
                        Venue = "Narendra Modi Stadium",
                        Attendees = GetAllAttendees(users, "IND"),
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"PAK", "IND"}
                    },
                    new Activity
                    {
                        Title = "Afghanistan vs England",
                        Date = ConvertLocalToUtc("15/10/2023 02:00pm"),
                        Description = "Afghanistan won by 69 runs",
                        Category = "cricket",
                        City = "Delhi",
                        Venue = "Arun Jaitley Stadium",
                        Attendees = GetAllAttendees(users, "AFG"),
                        AmountIfLose = 100,
                        AmountIfWon=0,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"AFG", "ENG"}
                    },
                    new Activity
                    {
                        Title = "Sri Lanka vs Australia",
                        Date = ConvertLocalToUtc("16/10/2023 02:00pm"),
                        Description = "Australia won by 5 wkts",
                        Category = "cricket",
                        City = "Lucknow",
                        Venue = "Bharat Ratna Shri Atal Bihari Vajpayee Ekana Cricket Stadium",
                        Attendees = GetAllAttendees(users, "AUS"),
                        AmountIfLose = 100,
                        AmountIfWon=50,
                        IsMandatoryActivity=true,
                        WinningOption="",
                        Options = new []{"SL", "AUS"}
                    },
                };

                await context.Activities.AddRangeAsync(activities);
                var result = await context.SaveChangesAsync();
                Console.WriteLine("Save Result: " + result);
            }
        }

        private static ICollection<ActivityAttendee> GetAllAttendees(List<AppUser> users, string option = "")
        {
            IList<ActivityAttendee> attendees = new List<ActivityAttendee>();

            foreach(var user in users)
            {
                attendees.Add(new ActivityAttendee
                    {
                        AppUser = user,
                        IsHost = user.UserName == "admin",
                        ChosenOption = option
                    }   
                );
            }

            return attendees;
        }
    }
}
