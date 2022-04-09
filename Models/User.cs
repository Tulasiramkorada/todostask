using Dotsql.DTOs;

namespace Dotsql.Models;

public record User
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public long Mobile { get; set; }
    public string Email { get; set; }


    public UserDTO asDto => new UserDTO
    {
        UserId = UserId,
        UserName = UserName,
        Password = Password,
        Email = Email,
        Mobile = Mobile,


    };
}
