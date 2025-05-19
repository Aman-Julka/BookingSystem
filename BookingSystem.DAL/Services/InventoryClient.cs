using BookingSystem.DAL.Interfaces;
using BookingSystemModel.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Services
{
    public class InventoryClient:IInventoryClient
    {
        private readonly IConfiguration _configuration;
        private readonly IDBExecuter _dbexecuter;

        public InventoryClient(IConfiguration configuration, IDBExecuter dbexecuter)
        {
            _configuration = configuration;
            _dbexecuter = dbexecuter;
        }

        public async Task UpsertManyInventoryAsync(List<InventoryModel> inventoryList)
        {
            try
            {
                var dataTable = new DataTable();
                var properties = typeof(InventoryModel).GetProperties();

                foreach (var prop in properties)
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                foreach (var inventory in inventoryList)
                {
                    var values = properties.Select(p => p.GetValue(inventory) ?? DBNull.Value).ToArray();
                    dataTable.Rows.Add(values);
                }

                using (var dbConnection = _dbexecuter.GetDbConnection())
                {
                    if (dbConnection is not SqlConnection sqlConnection)
                        throw new InvalidOperationException("Expected a SqlConnection from _dbexecuter.GetDbConnection().");

                    using var command = new SqlCommand("SP_UpsertInventoryDetails", sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    var inventoryParameter = new SqlParameter
                    {
                        ParameterName = "@InventoryDetails",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "Inventory_UDTT", // Ensure this matches your SQL Server UDTT
                        Value = dataTable
                    };

                    command.Parameters.Add(inventoryParameter);
                    if (sqlConnection.State != ConnectionState.Open)
                        await sqlConnection.OpenAsync();

                    await command.ExecuteNonQueryAsync();
                }

            }
            catch(Exception ex)
            {

            }
        }

        public async Task<InventoryModel> GetInventoryItem_ByIdAsync(int inventoryItemId)
        {
            try
            {
                using var dbConnection = _dbexecuter.GetDbConnection();

                var inventoryModel = await dbConnection.QueryFirstOrDefaultAsync<InventoryModel>(
                    "SP_GetInventory_ById",
                    new { InventoryItemId = inventoryItemId },
                    commandType: CommandType.StoredProcedure
                );
                return inventoryModel ?? new InventoryModel(); // or throw an exception instead
            }
            catch (Exception ex)
            {
                // Log ex if needed
                return new InventoryModel(); // or rethrow/handle gracefully
            }
        }

        public async Task<bool> UpdateInventoryInfoAsync(InventoryModel inventory)
        {
            bool updateInventory = false;
            try
            {
                using var dbConnection = _dbexecuter.GetDbConnection();

                var rowsAffected = await dbConnection.ExecuteAsync(
                    "SP_UpdateInventoryInfo_ById",
                    new
                    {
                        InventoryItemId = inventory.Id,
                        RemainingCount = inventory.RemainingCount
                        
                    },
                    commandType: CommandType.StoredProcedure
                );

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                // _logger.LogError(ex, "Failed to update inventory with ID {InventoryId}", inventory.InventoryId);
                return false;
            }
        }

    }
}
