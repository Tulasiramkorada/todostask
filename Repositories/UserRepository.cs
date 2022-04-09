using Dapper;
using Dotsql.Models;
using Dotsql.Utilities;


namespace Dotsql.Repositories;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User Item);
    Task<bool> Delete(long UserId);
    Task<User> GetById(long UserId);
    Task<List<User>> GetList();
    Task GetByUserId(long UserId);
    
}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<User> Create(User Item)
    {
        var query = $@"INSERT INTO public.""user""(user_name, password, email, mobile)
	VALUES (@UserName, @Password, @Email, @Mobile) 
        RETURNING *";

        //INSERT INTO public."user"(
        // user_id, user_name, password, mail_id, contact_number, post_id)
        // VALUES (?, ?, ?, ?, ?, ?);
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query, Item);
            return res;
        }
    }

    public Task<bool> Delete(long UserId)
    {
        throw new NotImplementedException();
    }

    // public Task<User> GetById(long UserId)
    // {
    //     throw new NotImplementedException();
    // }

    public Task GetByUserId(long UserId)
    {
        throw new NotImplementedException();
    }

    // public async Task<bool> Delete(long UserId)
    // {
    //     var query = $@"DELETE FROM ""{TableNames.user}"" 
    //     WHERE user_id = @UserId";

    //     using (var con = NewConnection)
    //     {
    //         var res = await con.ExecuteAsync(query, new { UserId });
    //         return res > 0;
    //     }
    // }

    public async Task<User> GetById(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.user}"" 
        WHERE user_id = @UserId";


        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query, new { UserId });
    }

    // public async Task<List<User>> GetList(int pageNumber, int Limit)
    // {
    //     // Query
    //     var query = $@"SELECT * FROM ""{TableNames.user}"" ORDER BY user_id LIMIT @Limit OFFSET @PageNumber";

    //     List<User> res;
    //     using (var con = NewConnection) // Open connection
    //         res = (await con.QueryAsync<User>(query, new { @PageNumber = (pageNumber - 1) * Limit, Limit })).AsList(); // Execute the query
    //     // Close the connection

    //     // Return the result
    //     return res;
    // }

    public Task<List<User>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetList(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(User Item)
    {
        throw new NotImplementedException();
    }

    // public async Task<bool> Update(User Item)
    // {
    //     var query = $@"UPDATE ""{TableNames.user}"" SET user_name = @UserName,password = @Password,
    //      email = @Email, mobile = @Mobile WHERE user_id = @UserId";

    //     using (var con = NewConnection)
    //     {
    //         var rowCount = await con.ExecuteAsync(query, Item);
    //         return rowCount == 1;
    //     }
    // }
}