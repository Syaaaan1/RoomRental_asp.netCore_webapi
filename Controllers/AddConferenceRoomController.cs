using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Crud;


namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddConferenceRoomController : Controller
    {
        private readonly InMemoryDatabase _database;
        public AddConferenceRoomController(InMemoryDatabase database)
        {
            _database = database;
        }
        [HttpPost("AddConferenceRoomController")]
        public ActionResult<ConferenceRoom> AddConferenceRoom(CreateRoomRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Capacity <= 0 || request.BaseHourlyRate <= 0)
            {
                return BadRequest("Invalid input data");
            }

            var NewRoom = new ConferenceRoom
            {
                Id = _database.ConferenceRooms.Count + 1,
                Name = request.Name,
                Capacity = request.Capacity,
                BaseHourlyRate = request.BaseHourlyRate,
                AvailableServices = new List<Service>()
            };

            foreach (var ServiceRequest in request.AvailableServices)
            {
                var Service = new Service
                {
                    Id = _database.Services.Count + 1,
                    Name = ServiceRequest.Name,
                    Price = ServiceRequest.Price,
                };

                _database.Services.Add(Service);
                NewRoom.AvailableServices.Add(Service);
            }

            _database.ConferenceRooms.Add(NewRoom);

            return CreatedAtAction(nameof(GetConferenceRoom), new { id = NewRoom.Id }, NewRoom);


        }

        [ApiExplorerSettings(IgnoreApi = true)]//скритий допоміжний метод для пошуку залу по id
        [HttpGet("GetConferenceRoom/{id}")]
        public ActionResult<ConferenceRoom> GetConferenceRoom(int id)
        {
            var room = _database.ConferenceRooms.FirstOrDefault(x => x.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }
    }
}
