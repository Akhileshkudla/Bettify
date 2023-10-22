using Application.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetTransactions(string username)
        {
            return HandleResults(await Mediator.Send(new List.Query { Username = username }));
        }
    }
}