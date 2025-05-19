using BookingSystemModel.Models;

namespace BookingSystem.BAL.Interfaces
{
    public interface IInventoryClientBAL
    {
        Task UpsertManyInventoryAsync(List<InventoryModel> inventoryList);
        Task<InventoryModel> GetInventoryItem_ByIdAsync(int inventoryItemId);
    }
}
