using AutoMapper;
using BookingSystemModel.DTO;
using BookingSystemModel.Models;

namespace BookingSystem.Mapper.Mapper
{
    public class MemberMapping : Profile
    {
        public MemberMapping()
        {
            CreateMap<MemberDTOModel, MemberModel>();
        }
    }
}
