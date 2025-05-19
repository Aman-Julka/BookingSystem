using BookingSystem.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookingSystem.DAL.Services
{
    public class DBExecuter : IDBExecuter
    {
        private readonly IConfiguration _configuration;

        public DBExecuter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection(string connectionString)
        {
            try
            {
                var conn = new SqlConnection(_configuration.GetConnectionString(connectionString));
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                return new SqlConnection();
            }
        }
    }
}
