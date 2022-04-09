using Dapper;
using Dotsql.Models;
using Dotsql.Utilities;

namespace Dotsql.Repositories;

public interface ITodoRepository
{
    Task<Todo> Create(Todo Item);
    Task<bool> Update(Todo Item);
    Task<bool> Delete(long Id);
    Task<Todo> GetById(long Id);
    Task<List<Todo>> GetList();
    // Task Update(object toUpdateTodo);
}
public class TodoRepository : BaseRepository, ITodoRepository
{
    public TodoRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Todo> Create(Todo Item)
    {
        var query = $@"INSERT INTO public.""todo""(description,title,user_id)
	VALUES (@Description, @Title,@UserId) 
        RETURNING *";

        //INSERT INTO public."user"(
        // user_id, user_name, password, mail_id, contact_number, post_id)
        // VALUES (?, ?, ?, ?, ?, ?);
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Todo>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.todo}"" 
        WHERE id = @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }
    }

    public async Task<Todo> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.todo}"" 
        WHERE id = @Id";


        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Todo>(query, new { Id });
    }

    public async Task<List<Todo>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.todo}"" ORDER BY id";

        List<Todo> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Todo>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }



    public async Task<bool> Update(Todo Item)
    {
        var query = $@"UPDATE ""{TableNames.todo}"" SET description = @Description
         WHERE id = @Id";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }

    // public Task Update(object toUpdateTodo)
    // {
    //     throw new NotImplementedException();
    // }
}