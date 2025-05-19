using BookingSystem.DAL.Interfaces;
using BookingSystemModel.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookingSystem.DAL.Services
{
    public class MemberDAL:IMemberDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDBExecuter _dbexecuter;

        public MemberDAL(IConfiguration configuration, IDBExecuter dbexecuter)
        {
            _configuration = configuration;
            _dbexecuter = dbexecuter;
        }

        public async Task UpsertManyMembersAsync(List<MemberModel> membersList)
        {
            try
            {
                var dataTable = new DataTable();
                var properties = typeof(MemberModel).GetProperties();

                foreach (var prop in properties)
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                foreach (var member in membersList)
                {
                    var values = properties.Select(p => p.GetValue(member) ?? DBNull.Value).ToArray();
                    dataTable.Rows.Add(values);
                }

                using (var dbConnection = _dbexecuter.GetDbConnection())
                {
                    if (dbConnection is not SqlConnection sqlConnection)
                        throw new InvalidOperationException("Expected a SqlConnection from _dbexecuter.GetDbConnection().");

                    using var command = new SqlCommand("SP_UpsertMemberDetails", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    var inventoryParameter = new SqlParameter
                    {
                        ParameterName = "@MemberDetails",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "Member_UDTT", // Ensure this matches your SQL Server UDTT
                        Value = dataTable
                    };

                    command.Parameters.Add(inventoryParameter);
                    if (sqlConnection.State != ConnectionState.Open)
                        await sqlConnection.OpenAsync();

                    await command.ExecuteNonQueryAsync();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<MemberModel> GetMember_ByIdAsync(int memberId)
        {
            try
            {
                using var dbConnection = _dbexecuter.GetDbConnection();

                var memberModel = await dbConnection.QueryFirstOrDefaultAsync<MemberModel>(
                    "SP_GetMember_ById",
                    new { MemberId = memberId },
                    commandType: CommandType.StoredProcedure
                );

                return memberModel ?? new MemberModel(); // or throw an exception instead
            }
            catch (Exception ex)
            {
                // Log ex if needed
                return new MemberModel(); // or rethrow/handle gracefully
            }
        }

        public async Task<bool> UpdateMemberInfoAsync(MemberModel member)
        {
            try
            {
                using var dbConnection = _dbexecuter.GetDbConnection();

                var rowsAffected = await dbConnection.ExecuteAsync(
                    "SP_UpdateMemberBookingInfo_ById",
                    new
                    {
                        MemberId = member.Id,
                        BookingCount = member.BookingCount,
                        DateJoined = member.DateJoined
                    },
                    commandType: CommandType.StoredProcedure
                ); ;

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                // _logger.LogError(ex, "Failed to update member with ID {MemberId}", inventory.Id);
                return false;
            }
        }
    }
}
