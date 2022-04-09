using Dotsql.DTOs;

namespace Dotsql.Models;
public record Todo
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }

    public TodoDTO asDto => new TodoDTO
    {
        Id = Id,
        UserId = UserId,
        Description = Description,
        Title = Title
    };
}