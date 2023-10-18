using Application.Core;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class TransactionDetails
    {
        public class Query : IRequest<Result<List<string>>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<string>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
            }

            public async Task<Result<List<string>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var messages = await _context.ActivityAttendees
                .Where(x => x.AppUser.UserName == request.Username)
                .Select(x => x.Message)
                .ToListAsync(cancellationToken); // Execute the query asynchronously and materialize the results to a list

                return Result<List<string>>.Sucess(messages);
            }
        }
    }
}