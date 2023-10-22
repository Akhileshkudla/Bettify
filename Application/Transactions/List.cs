using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;

namespace Application.Transactions
{
    public class List
    {
        public class Query : IRequest<Result<List<Transaction>>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Transaction>>>
        {
            private readonly DataContext _context;

            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<List<Transaction>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Transaction> transactions = await _context.Transactions.Where(t => t.TransactionUser.UserName == _userAccessor.GetUsername()).ToListAsync();

                return Result<List<Transaction>>.Sucess(transactions);                
            }
        }
    }
}