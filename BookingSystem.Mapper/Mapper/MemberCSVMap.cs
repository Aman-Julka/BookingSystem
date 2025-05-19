using BookingSystemModel.DTO;
using CsvHelper.Configuration;
using System.Globalization;

namespace BookingSystem.Mapper.Mapper
{
    public class MemberCSVMap : ClassMap<MemberDTOModel>
    {
        public MemberCSVMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.Surname).Name("surname");
            Map(m => m.BookingCount).Name("booking_count");
            Map(m => m.DateJoined)
                .Name("date_joined")
                .TypeConverterOption
                .DateTimeStyles(DateTimeStyles.None)
                .TypeConverterOption
                //.TypeConverterOption.Format("yyyy-MM-ddTHH:mm:ss"); // ISO 8601
                .Format("yyyy-MM-ddTHH:mm:ss"); // Force this format!
        }
    }
}
