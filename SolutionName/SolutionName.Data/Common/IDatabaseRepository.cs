using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SolutionName.Data.Common
{
    public interface IDatabaseRepository<T> : IRepository
    {
        int? Insert(T model);
        int? Update(T model);
        IEnumerable<T> GetList(object whereConditions);
        IEnumerable<T> GetList();
        T Get(object id);
        int RecordCount(object whereConditions);
        int Delete(object id);
        int Delete(T model);
        int DeleteList(object whereConditions);
        Z ExecuteScalar<Z>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, System.Data.CommandType? commandType = null);
        IEnumerable<Z> Query<Z>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        IEnumerable<Z> Query<Z, K>(string sql, Func<Z, K, Z> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
