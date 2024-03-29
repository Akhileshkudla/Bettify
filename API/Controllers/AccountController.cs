using System.Security.Claims;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {		
        var user = await _userManager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        if(user == null) return Unauthorized();

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if(result)
        {
            return CreateUserObject(user);
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            ModelState.AddModelError("email", "Email already registered! Please try to login.");
            return ValidationProblem();
        }
        if(await _userManager.Users.AnyAsync(u => u.UserName == registerDto.Username))
        {
            ModelState.AddModelError("username", "User name already in use, please choose a different user name.");
            return ValidationProblem();
        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Username
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if(result.Succeeded)
        {
            return CreateUserObject(user);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var identityResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (!identityResult.Succeeded)
        {
            return BadRequest(identityResult.Errors);
        }

        return Ok();
    }

    [Authorize(Policy = "IsAdmin")]
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteUser([FromBody] AmountDto amountDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == amountDto.Username);

        var identityResult = await _userManager.DeleteAsync(user);

        if (!identityResult.Succeeded)
        {
            return BadRequest(identityResult.Errors);
        }

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

        return CreateUserObject(user);
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        List<AppUser> user = await _userManager.Users.ToListAsync();
        List<UserDto> userDtos = new List<UserDto>(); 
        user.ForEach(x => userDtos.Add(CreateUserObject(x, false).Value));
        return userDtos;
    }

    [Authorize(Policy = "IsAdmin")]
    [HttpPut("setamount")]
    public async Task<IActionResult> SetAmount(AmountDto amountDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == amountDto.Username);

        if(user == null) return BadRequest("User not found");

        user.Amount += amountDto.Amount;                

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Ok("User amount updated successfully");
        }
        else
        {
            return BadRequest("Failed to update user amount");
        }
    }
   

    private ActionResult<UserDto> CreateUserObject(AppUser user, bool isTokenReq = true)
    {
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Image = user?.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
            Token = isTokenReq ? _tokenService.CreateToken(user) : "",
            Username = user.UserName,
            Amount = user.Amount
        };
    }
}