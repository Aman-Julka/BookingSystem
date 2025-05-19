using AutoMapper;
using BookingSystem.BAL.Interfaces;
using BookingSystem.BAL.Services;
using BookingSystem.Mapper.Mapper;
using BookingSystemModel.DTO;
using BookingSystemModel.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BookingSystem.Controllers
{
    [Route("Member")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly IMemberBAL _memberBAL;
        private readonly IMapper _mapper;

        public MemberController(IMemberBAL memberBAL, IMapper mapper)
        {
            _memberBAL = memberBAL;
            _mapper = mapper;
        }

        [HttpPost("UpsertMemberDetails")]
        public async Task<IActionResult> UpsertMemberDetails(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No member's file uploaded.");

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            });

            csv.Context.RegisterClassMap<MemberCSVMap>();

            var records = csv.GetRecords<MemberDTOModel>().ToList();
            var membersList = _mapper.Map<List<MemberModel>>(records);

            await _memberBAL.UpsertManyMembersAsync(membersList);

            return Ok(new { Message = "Member's data upserted successfully.", Count = membersList.Count });


        }
    }
}
