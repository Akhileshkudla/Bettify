using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }

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

                if (activity == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x =>
                    x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var hostUsername = activity.Attendees.FirstOrDefault(a => a.IsHost).AppUser.UserName;

                var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (attendance != null && hostUsername == user.UserName)
                    activity.IsCancelled = !activity.IsCancelled;

                string type = "cancelled";

                if (attendance != null && hostUsername != user.UserName)
                {
                    activity.Attendees.Remove(attendance);

                }

                if (attendance == null)
                {
                    attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false,
                        ChosenOption = request.ChosenOption
                    };

                    activity.Attendees.Add(attendance);
                    type = "placed";
                }

                var result = await _context.SaveChangesAsync() > 0;

                if (result) await _telegramService.SendMessageAsync($"{attendance.AppUser.DisplayName} {type} their bet on {attendance.ChosenOption}.");

                return result ? Result<Unit>.Sucess(Unit.Value) : Result<Unit>.Failure("Problem updating attendees");
            }
        }
    }
}