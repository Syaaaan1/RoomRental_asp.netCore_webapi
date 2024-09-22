using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Crud;
using System.Globalization;

namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingConferenceRoomController : Controller
    {
        private readonly InMemoryDatabase _database;
        public BookingConferenceRoomController(InMemoryDatabase database)
        {
            _database = database;
        }

        [HttpPost("BookConferenceRoomController")]
        public ActionResult<ConferenceRoom> BookingConferenceRoom([FromBody] RoomBookingRequest request, int id)
        {
            var room = _database.ConferenceRooms.FirstOrDefault(x => x.Id == request.ConferenceRoomId);
            if (room == null)
            {
                return NotFound($"Conference room with ID {request.ConferenceRoomId} not found.");
            }

            if (!DateTime.TryParseExact(request.StartTime, "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bookingStartTime))
            {
                return BadRequest("Invalid date format. Expected format: yyyy.MM.dd HH:mm");
            }


            var bookingEndTime = bookingStartTime.AddHours(request.Duration);

            var isRoomAvailable = !_database.Bookings.Any(booking => //перевірка доступності зали по перетину часу
                booking.ConferenceRoomId == request.ConferenceRoomId &&
                ((booking.StartTime < bookingEndTime && booking.EndTime > bookingStartTime) ||
                 (booking.StartTime >= bookingStartTime && booking.StartTime < bookingEndTime)));

            if (!isRoomAvailable)
            {
                return BadRequest("Room is not available at the requested time.");
            }

            decimal totalCost = 0;
            for (var time = bookingStartTime; time < bookingEndTime; time = time.AddHours(1))
            {
                totalCost += CalculateHourlyRate(room.BaseHourlyRate, time);
            }

            foreach (var serviceId in request.ServiceIds)
            {
                var service = _database.Services.FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    totalCost += service.Price;
                }
            }

            var newBooking = new Booking
            {
                Id = _database.Bookings.Count + 1,
                ConferenceRoomId = room.Id,
                StartTime = bookingStartTime,
                EndTime = bookingEndTime,
                BookedServices = _database.Services.Where(s => request.ServiceIds.Contains(s.Id)).ToList()
            };

            _database.Bookings.Add(newBooking);

            return Ok(new { Message = "Booking confirmed", TotalCost = totalCost, Booking = newBooking });
        }

        // Метод для розрахунку вартості години з урахуванням знижок та націнок
        private decimal CalculateHourlyRate(decimal baseRate, DateTime time)
        {
            if (time.Hour >= 9 && time.Hour < 18)
            {
                return baseRate; // Стандартні часи
            }
            else if (time.Hour >= 18 && time.Hour < 23)
            {
                return baseRate * 0.8m; // скидка 20%
            }
            else if (time.Hour >= 6 && time.Hour < 9)
            {
                return baseRate * 0.9m; // скидка 10%
            }
            else if (time.Hour >= 12 && time.Hour < 14)
            {
                return baseRate * 1.15m; // націнка 15%
            }
            else
            {
                return baseRate; // інші часи
            }
        }
    }
}
