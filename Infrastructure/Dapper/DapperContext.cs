using System.Data;
using Dapper;
using Infrastructure.Dapper.Interfaces;
using Npgsql;


namespace Infrastructure.Dapper;

public class DapperContext(IDapperSettings dapperSettings) : IDapperContext
{
    public async Task<T?> FirstOrDefault<T>(IQueryObject queryObject)
    {
        return await Execute(query =>query.QueryFirstOrDefaultAsync<T>(queryObject.Sql, queryObject.Params));
    }

    public async Task<List<T>?> ListOrEmpty<T>(IQueryObject queryObject)
    {
        return (await Execute(query => query.QueryAsync<T>(queryObject.Sql, queryObject.Params))).ToList();
    }

    public async Task<T> CommandWithResponse<T>(IQueryObject queryObject)
    {
        return await Execute(query => query.QueryFirstAsync<T>(queryObject.Sql, queryObject.Params));
    }

    public async Task Command<T>(IQueryObject queryObject)
    {
        await Execute(query => query.QueryAsync<T>(queryObject.Sql, queryObject.Params));
    }

    private async Task<T> Execute<T>(Func<IDbConnection, Task<T>> query)
    {
        await using var connection = new NpgsqlConnection(dapperSettings.ConnectionString);
        var res = await query(connection);
        await connection.CloseAsync();
        return res;
    }
}