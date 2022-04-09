using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TodoTask.Controllers;

[ApiController]
[Route("api/user")]
// [Authorize]
public class userController : ControllerBase
{
    private readonly ILogger<userController> _logger;
    private readonly IUserRepository _user;
    private IConfiguration _configuration;

    public userController(ILogger<userController> logger, IUserRepository user, IConfiguration configuration)
    {
        _logger = logger;
        _user = user;
        _configuration = configuration;
    }

    [HttpGet("{user_id}")]


    public async Task<ActionResult<UserDTO>> GetById([FromRoute] long user_id)
    {
        var user = await _user.GetById(user_id);
        if (user is null)
            return NotFound("No Student found with given userid");
        var dto = user.asDto;


        return Ok(dto);
    }

    [HttpPost("Create")]



    public async Task<ActionResult<UserDTO>> Createuser([FromBody] UserCreateDTO Data)
    {

        Console.WriteLine(" inside user create : " + Data.Email);

        var toCreateuser = new User
        {
            UserName = Data.UserName,
            //Name = Data.Name.Trim(),
            Email = Data.Email.Trim(),
            Password = Data.Password.Trim(),
            Mobile = Data.Mobile,


        };
        var createduser = await _user.Create(toCreateuser);

        return StatusCode(StatusCodes.Status201Created, createduser.asDto);
        // return Createuser();


    }



    [HttpPost]
    [Route("log_in")]


    public async Task<ActionResult<UserDTO>> Login([FromBody] Login Login)
    {
        var currentUser = await _user.GetById(Login.UserId);
        if (currentUser == null)
            return NotFound("user not found");

        if (currentUser.Password != Login.Password)
            return Unauthorized("Invalid password");
        var token = Generate(currentUser);
        return Ok(token);
    }



    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.Mobile.ToString()),
            // new Claim(ClaimTypes.GivenName, user.Password)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}