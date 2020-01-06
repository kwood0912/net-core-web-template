using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SolutionName.Data.Common
{
    public class DatabaseRepository<T>
    {
        private readonly IConfiguration _configuration;

        protected DatabaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int? Insert(T model)
        {
            int? result = 0;
            using (var connection = GetConnection())
            {
                result = connection.Insert(model);
            }
            return result;
        }

        public int? Update(T model)
        {
            int? result = 0;
            using (var connection = GetConnection())
            {
                result = connection.Update(model);
            }
            return result;
        }

        public IEnumerable<T> GetList(object whereConditions)
        {
            IEnumerable<T> result = null;
            using (var connection = GetConnection())
            {
                result = connection.GetList<T>(whereConditions);
            }
            return result ?? new List<T>();
        }

        public IEnumerable<T> GetList()
        {
            IEnumerable<T> result = null;
            using (var connection = GetConnection())
            {
                result = connection.GetList<T>();
            }
            return result ?? new List<T>();
        }

        public T Get(object id)
        {
            T result = default;
            using (var connection = GetConnection())
            {
                result = connection.Get<T>(id);
            }
            return result;
        }

        public int RecordCount(object whereConditions)
        {
            int result = 0;
            using (var connection = GetConnection())
            {
                result = connection.RecordCount<T>(whereConditions);
            }
            return result;
        }

        public int Delete(object id)
        {
            int result = 0;
            using (var connection = GetConnection())
            {
                result = connection.Delete<T>(id);
            }
            return result;
        }

        public int Delete(T model)
        {
            int result = 0;
            using (var connection = GetConnection())
            {
                result = connection.Delete<T>(model);
            }
            return result;
        }

        public int DeleteList(object whereConditions)
        {
            int result = 0;
            using (var connection = GetConnection())
            {
                result = connection.DeleteList<T>(whereConditions);
            }
            return result;
        }

        public Z ExecuteScalar<Z>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Z result = default(Z);
            using (var connection = GetConnection())
            {
                result = connection.ExecuteScalar<Z>(sql, param, transaction, commandTimeout, commandType);
            }
            return result;
        }

        public IEnumerable<Z> Query<Z>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<Z> result = null;
            using (var connection = GetConnection())
            {
                result = connection.Query<Z>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
            return result;
        }

        public IEnumerable<Z> Query<Z, K>(string sql, Func<Z, K, Z> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<Z> result = null;
            using (var connection = GetConnection())
            {
                result = connection.Query<Z, K, Z>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            return result;
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = 0;
            using (var connection = GetConnection())
            {
                result = connection.Execute(sql, param, transaction, commandTimeout, commandType);
            }
            return result;
        }

        private MySqlConnection GetConnection()
        {
            string conStr = _configuration.GetConnectionString("DefaultConnection");
            MySqlConnection sqlConnection = new MySqlConnection(conStr);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
