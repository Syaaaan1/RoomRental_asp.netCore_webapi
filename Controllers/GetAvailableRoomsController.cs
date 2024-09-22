using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Crud;
using System.Globalization;

namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAvailableRoomsController : Controller
    {
        private readonly InMemoryDatabase _database;
        public GetAvailableRoomsController(InMemoryDatabase database)
        {
            _database = database;
        }

        [HttpGet("GetAvailableRoomsController")]
        public ActionResult<IEnumerable<ConferenceRoom>> GetAvailableRooms([FromQuery] RoomSearchRequest request)
        {
            var startTimeSpan = request.GetStartTimeSpan();//строка => временной интервал
            var endTimeSpan = request.GetEndTimeSpan();

            if (startTimeSpan >= endTimeSpan)
            {
                return BadRequest("Start time must be before end time.");
            }

            var searchStartDateTime = request.Date.Add(startTimeSpan); //суммируем и получаем полноценную дату и время
            var searchEndDateTime = request.Date.Add(endTimeSpan);

            var availableRooms = _database.ConferenceRooms
                .Where(room => room.Capacity >= request.Capacity)
                .Where(room => !_database.Bookings.Any(booking =>
                    booking.ConferenceRoomId == room.Id &&
                    ((booking.StartTime <= searchStartDateTime && booking.EndTime > searchStartDateTime) ||
                     (booking.StartTime < searchEndDateTime && booking.EndTime >= searchEndDateTime) ||
                     (booking.StartTime >= searchStartDateTime && booking.EndTime <= searchEndDateTime))))
                .ToList();

            return Ok(availableRooms);
        }
    }
}
