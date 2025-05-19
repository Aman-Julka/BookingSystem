using BookingSystemModel.DTO;
using CsvHelper.Configuration;
using System.Globalization;

namespace BookingSystem.Mapper.Mapper
{
    public sealed class InventoryCSVMap : ClassMap<InventoryDTOModel>
    {
        public InventoryCSVMap()
        {
            Map(m => m.Title).Name("title");
            Map(m => m.Description).Name("description");
            Map(m => m.RemainingCount).Name("remaining_count");
            Map(m => m.ExpirationDate)
                .Name("expiration_date")
                .TypeConverterOption
                .DateTimeStyles(DateTimeStyles.None)
                .TypeConverterOption
                .Format("dd/MM/yyyy"); // Force this format!
        }
    }
}
