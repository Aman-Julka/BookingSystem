using BookingSystem.BAL.Interfaces;
using BookingSystem.DAL.Interfaces;
using BookingSystemModel.Models;

namespace BookingSystem.BAL.Services
{
    public class InventoryClientBAL:IInventoryClientBAL
    {
        private readonly IInventoryClient _inventoryClient;

        public InventoryClientBAL(IInventoryClient inventoryClient)
        {
            _inventoryClient = inventoryClient;
        }

        public async Task UpsertManyInventoryAsync(List<InventoryModel> inventoryList)
        {
            try
            {
                await _inventoryClient.UpsertManyInventoryAsync(inventoryList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InventoryModel> GetInventoryItem_ByIdAsync(int inventoryItemId)
        {
            try
            {
                return await _inventoryClient.GetInventoryItem_ByIdAsync(inventoryItemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
