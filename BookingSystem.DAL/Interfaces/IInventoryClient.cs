using BookingSystemModel.Models;

namespace BookingSystem.DAL.Interfaces
{
    public interface IInventoryClient
    {
        Task UpsertManyInventoryAsync(List<InventoryModel> inventoryList);
        Task<InventoryModel> GetInventoryItem_ByIdAsync(int inventoryItemId);
        Task<bool> UpdateInventoryInfoAsync(InventoryModel inventory);
    }
}
