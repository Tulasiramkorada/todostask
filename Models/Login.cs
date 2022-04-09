namespace Dotsql.Models;
public record Login
{
    public long UserId { get; set; }
    public string Password { get; set; }
}