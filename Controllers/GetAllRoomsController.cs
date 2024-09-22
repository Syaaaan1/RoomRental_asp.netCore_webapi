using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Crud;
using System.Globalization;

namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllRoomsController : Controller
    {
        private readonly InMemoryDatabase _database;

        public GetAllRoomsController(InMemoryDatabase database)
        {
            _database = database;
        }

        [HttpGet("GetAllRoomsController")]
        public ActionResult<IEnumerable<ConferenceRoom>> GetAllRooms()
        {
            return Ok(_database.ConferenceRooms);
        }
    }
}
