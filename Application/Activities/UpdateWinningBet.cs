using System.Security.Cryptography.X509Certificates;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class UpdateWinningBet
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id {get; set;}

            public string ChosenOption { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
        private readonly ITelegramService _telegramService;
            public Handler(DataContext context, IUserAccessor userAccessor, ITelegramService telegramService)
            {
                _telegramService = telegramService;
                _userAccessor = userAccessor;
                _context = context;                
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                    .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(activity == null) return null;

                activity.WinningOption = request.ChosenOption;                

                //TODO: Some junk logic, I need to find a better place to put this.
                await _telegramService.SendMessageAsync($"Activity : {activity.Title} has completed, Winning option is {activity.WinningOption}");

                foreach( var user in _context.Users)
                {
                    if(activity.Attendees.Any(actat => actat.AppUser.Id == user.Id)) continue;

                    var attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false,
                        ChosenOption = "",                        
                    };

                    activity.Attendees.Add(attendance);
                    await _telegramService.SendMessageAsync($"User : {user.DisplayName} forgot to bet!!");
                }


                foreach (ActivityAttendee attendee in activity.Attendees)
                {
                    if(attendee.IsHost) continue; //TODO: Since for now only admin is Host! Should be removed later

                    if(string.IsNullOrEmpty( attendee.ChosenOption))
                    {
                        int origAmount = attendee.AppUser.Amount;
                        attendee.AppUser.Amount = origAmount + activity.AmountIfLose * 2; //If did not vote, Double on the loosing side
                        
                        AddTrnsactionToUser(attendee.AppUser.Id, activity.AmountIfLose * 2, $"Chosen option: Empty", $"Forgot to vote for activity {activity.Title}");
                        attendee.Message = $"Added {activity.AmountIfLose * 2} for cheating/forgetting on activity {activity.Title}, Total now is {attendee.AppUser.Amount}";
                        await _telegramService.SendMessageAsync($"User {attendee.AppUser.DisplayName}, "+ 
                            $"You forgot to bet, Your total amount increased from {origAmount} to {attendee.AppUser.Amount}");
                    }
                    else if(attendee.ChosenOption != activity.WinningOption)
                    {
                        attendee.AppUser.Amount += activity.AmountIfLose;
                        AddTrnsactionToUser(attendee.AppUser.Id, activity.AmountIfLose, $"Chosen option: {attendee.ChosenOption}", $"Lost bet on {activity.Title}");
                        attendee.Message = $"Added {activity.AmountIfLose } for loosing bet on activity {activity.Title}, Total now is {attendee.AppUser.Amount}";
                        await _telegramService.SendMessageAsync($"User {attendee.AppUser.DisplayName}, "+ 
                            $"You lost the bet, Your total amount increased from {attendee.AppUser.Amount - activity.AmountIfLose} to {attendee.AppUser.Amount}");
                    }
                    else
                    {
                        attendee.AppUser.Amount += activity.AmountIfWon;
                        AddTrnsactionToUser(attendee.AppUser.Id, activity.AmountIfWon, $"Chosen option: {attendee.ChosenOption}", $"Won bet on {activity.Title}");
                        attendee.Message = $"Added {activity.AmountIfWon } for winning bet on activity {activity.Title}, Total now is {attendee.AppUser.Amount}";
                        await _telegramService.SendMessageAsync($"User {attendee.AppUser.DisplayName}, "+ 
                            $"Congratulations, Your total amount changed from {attendee.AppUser.Amount - activity.AmountIfWon} to {attendee.AppUser.Amount}");
                    }
                }

                var result = await _context.SaveChangesAsync() > 0;

                //Send Message on telegram
                if(result) await _telegramService.SendMessageAsync($"Activity : {activity.Title} has completed, Winning option is {activity.WinningOption}");

                return result ? Result<Unit>.Sucess(Unit.Value) : Result<Unit>.Failure("Problem updating winning bet");
            }

            private void AddTrnsactionToUser(string id, int amount, string name, string message)
            {
                _context.Transactions.AddAsync(new Transaction{
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Name = name,
                    Message = message,
                    TransactionUserId = id
                });
            }
        }
    }
}