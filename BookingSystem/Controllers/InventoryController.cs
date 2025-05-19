using AutoMapper;
using BookingSystem.BAL.Interfaces;
using BookingSystem.Mapper.Mapper;
using BookingSystemModel.DTO;
using BookingSystemModel.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BookingSystem.Controllers
{
    [Route("Inventory")]
    [ApiController]
   
    public class InventoryController : Controller
    {
        private readonly IInventoryClientBAL _inventoryClientBAL;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryClientBAL inventoryClientBAL,IMapper mapper)
        {
            _inventoryClientBAL = inventoryClientBAL;
            _mapper = mapper;
        }

        [HttpPost("UpsertInventoryDetails")]
        public async Task<IActionResult> UpsertInventoryDetails(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            }) ;

            csv.Context.RegisterClassMap<InventoryCSVMap>();

            var records = csv.GetRecords<InventoryDTOModel>().ToList();
            var inventoryList = _mapper.Map<List<InventoryModel>>(records);
            
            await _inventoryClientBAL.UpsertManyInventoryAsync(inventoryList);

            return Ok(new { Message = "Inventory data upserted successfully.", Count = inventoryList.Count });


        }
    }
}
