using Dotsql.DTOs;
using Dotsql.Models;
using Dotsql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IUserRepository _User;

    private readonly ITodoRepository _Todo;
    public TodoController(ILogger<TodoController> logger, IUserRepository user, ITodoRepository todo)
    {
        _logger = logger;
        _User = user;
        _Todo = todo;

    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAllTodo()
    {
        var todoList = await _Todo.GetList();

        // User -> UserDTO
        var dtoList = todoList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetUserById([FromRoute] long id)
    {
        var user = await _Todo.GetById(id);

        if (user is null)
            return NotFound("No user found with given user id");

        var dto = user.asDto;
        // dto.Todo = await _todo.GetAllForUser(todo.Id);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo([FromBody] TodoCreateDTO Data)
    {

        var toCreateTodo = new Todo
        {
            UserId = Data.UserId,
            Description = Data.Description.Trim().ToLower(),
            Title = Data.Description.Trim().ToLower(),
        };

        var createdTodo = await _Todo.Create(toCreateTodo);

        return StatusCode(StatusCodes.Status201Created, createdTodo.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodo([FromRoute] long id,
    [FromBody] TodoUpdateDTO Data)
    {
        var existing = await _Todo.GetById(id);
        if (existing is null)
            return NotFound("No user found with given id");

        var toUpdateTodo = existing with
        {
            Description = Data.Description?.Trim()?.ToLower() ?? existing.Description,
            // Title = Data.Title?.Trim()?.ToLower() ?? existing.Title,

        };

        var didUpdate = await _Todo.Update(toUpdateTodo);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update todo");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo([FromRoute] long id)
    {
        var existing = await _Todo.GetById(id);
        if (existing is null)
            return NotFound("No user found with given user id");

        var didDelete = await _Todo.Delete(id);

        return NoContent();
    }
}

