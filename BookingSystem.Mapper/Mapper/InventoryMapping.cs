using AutoMapper;
using BookingSystemModel.DTO;
using BookingSystemModel.Models;

namespace BookingSystem.Mapper.Mapper
{
    public class InventoryMapping : Profile
    {
        public InventoryMapping()
        {
            CreateMap<InventoryDTOModel, InventoryModel>();
        }
    }
}
