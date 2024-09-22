using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Crud;
using System.Globalization;

namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteConferenceRoomController : Controller
    {
        private readonly InMemoryDatabase _database;
        public DeleteConferenceRoomController(InMemoryDatabase database)
        {
            _database = database;
        }

        [HttpDelete("DeleteConferenceRoomController/{id}")]
        public ActionResult<ConferenceRoom> DeleteConferenceRoom(int id)
        {
            var room = _database.ConferenceRooms.FirstOrDefault(x => x.Id == id);

            if (room == null)
            {
                return NotFound($"Conference room with ID {id} not found.");
            }

            // Перевіряємо, чи немає активних бронювань для цього залу 
            var activeBookings = _database.Bookings.Any(b => b.ConferenceRoomId == id && b.EndTime > DateTime.Now);
            if (activeBookings)
            {
                return BadRequest("Cannot delete the room. There are active bookings for this room.");
            }

            _database.ConferenceRooms.Remove(room);

            _database.Services.RemoveAll(x => room.AvailableServices.Contains(x));

            // Видаляємо всі минулі бронювання для цієї зали
            _database.Bookings.RemoveAll(b => b.ConferenceRoomId == id);


            return Ok(new { Message = $"Conference room with ID {id} has been successfully deleted." });
        }
    }
}
