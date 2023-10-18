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
                await _telegramService.SendMessageAsync($"Activity : {activity.Title} has comepleted, Winning option is {activity.WinningOption}");

                foreach (ActivityAttendee attendee in activity.Attendees)
                {
                    if(attendee.IsHost) continue; //TODO: Since for now only admin is Host! Should be removed later

                    if(string.IsNullOrEmpty( attendee.ChosenOption))
                    {
                        int origAmount = attendee.AppUser.Amount;
                        attendee.AppUser.Amount = activity.AmountIfLose * 2; //If did not vote, Double on the loosing side
                        
                        attendee.Message = $"Added {activity.AmountIfLose * 2} for cheating on activity {activity.Title}, Total now is {attendee.AppUser.Amount}";
                        await _telegramService.SendMessageAsync($"User {attendee.AppUser.DisplayName}, "+ 
                            $"You forgot to bet, Your total amount increased from {origAmount} to {attendee.AppUser.Amount}");
                    }

                    if(attendee.ChosenOption != activity.WinningOption)
                    {
                        attendee.AppUser.Amount += activity.AmountIfLose;
                        attendee.Message = $"Added {activity.AmountIfLose } for loosing bet on activity {activity.Title}, Total now is {attendee.AppUser.Amount}";
                        await _telegramService.SendMessageAsync($"User {attendee.AppUser.DisplayName}, "+ 
                            $"You lost the bet, Your total amount increased from {attendee.AppUser.Amount - activity.AmountIfLose} to {attendee.AppUser.Amount}");
                    }
                    else
                    {
                        attendee.AppUser.Amount += activity.AmountIfWon;
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
        }
    }
}