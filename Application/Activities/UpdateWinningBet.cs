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
                    if(attendee.IsHost) continue; //Since for now only admin is Host! Should be removed later

                    if(attendee.ChosenOption != activity.WinningOption)
                    {
                        attendee.AppUser.Amount += activity.AmountIfLose;
                        await _telegramService.SendMessageAsync($"User {attendee.AppUser.DisplayName}, "+ 
                            $"You lost the bet, Your total amount increased from {attendee.AppUser.Amount - activity.AmountIfLose} to {attendee.AppUser.Amount}");
                    }
                    else
                    {
                        attendee.AppUser.Amount += activity.AmountIfWon;
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