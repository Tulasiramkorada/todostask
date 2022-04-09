using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

//using System.Text.Json.Serialization;

namespace Dotsql.DTOs;

public record UserDTO
{

    public long UserId { get; set; }


    public string UserName { get; set; }

    public string Password { get; set; }

    public long Mobile { get; set; }

    public string Email { get; set; }
}

public record UserCreateDTO
{
    public long UserId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }


    public string Email { get; set; }


    public long Mobile { get; set; }
}
public record UserUpdateDTO
{

    public string UserName { get; set; }

    public string Password { get; set; }



    public long Mobile { get; set; }


    public string Email { get; set; }
}