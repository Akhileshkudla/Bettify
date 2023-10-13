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
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
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

                foreach (ActivityAttendee attendee in activity.Attendees)
                {
                    if(attendee.ChosenOption != activity.WinningOption)
                    {
                        attendee.AppUser.Amount += activity.AmountIfLose;
                    }
                    else
                    {
                        attendee.AppUser.Amount += activity.AmountIfWon;
                    }
                }

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Sucess(Unit.Value) : Result<Unit>.Failure("Problem updating winning bet");
            }
        }
    }
}