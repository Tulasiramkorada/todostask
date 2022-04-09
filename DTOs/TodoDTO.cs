using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

//using System.Text.Json.Serialization;

namespace Dotsql.DTOs;

public record TodoDTO
{

    public long Id { get; set; }


    public long UserId { get; set; }

    public string Description { get; set; }

    public string Title { get; set; }

}

public record TodoCreateDTO
{


    public long UserId { get; set; }

    public string Description { get; set; }


    public string Title { get; set; }
}

public record TodoUpdateDTO
{

    public string Description { get; set; }



}