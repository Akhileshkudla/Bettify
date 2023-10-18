using API.DTOs;
using Application.Activities;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{    
    [HttpGet]
    public async Task<IActionResult> GetActivities([FromQuery] ActivityParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
        return HandleResults(await Mediator.Send(new Details.Query{ Id = id }));
    }

    [HttpGet("transactions/{username}")]
    public async Task<IActionResult> GetAllTranscations(string username)
    {
        return HandleResults(await Mediator.Send(new TransactionDetails.Query{ Username = username }));
    }

    [Authorize(Policy = "IsAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        return HandleResults(await Mediator.Send(new Create.Command {Activity = activity}));
    }

    [Authorize(Policy = "IsAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return HandleResults(await Mediator.Send(new Edit.Command {Activity = activity}));
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        return HandleResults(await Mediator.Send(new Delete.Command {Id = id}));
    }

    [HttpPost("{id}/attend")]
    public async Task<IActionResult> Attend(Guid id, OptionsDto options)
    {
        return HandleResults(await Mediator.Send(new UpdateAttendance.Command{Id = id, ChosenOption = options.option}));
    }

    [Authorize(Policy = "IsAdmin")]
    [HttpPost("{id}/setwinningteam")]
    public async Task<IActionResult> SetWinningTeam(Guid id, OptionsDto options)
    {
        return HandleResults(await Mediator.Send(new UpdateWinningBet.Command{Id = id, ChosenOption = options.option}));
    }
}