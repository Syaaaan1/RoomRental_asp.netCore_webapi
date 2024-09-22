using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;


namespace RoomRentalTZ_1at.Services
{
    public class ReportService : IReportService
    {
        private readonly InMemoryDatabase _database;

        public ReportService(InMemoryDatabase database)
        {
            _database = database;
        }

        public async Task<BookingsCountReport> GetBookingsCountReportAsync()
        {
            var bookingsCounts = _database.ConferenceRooms.Select(room => new RoomBookingsCount
            {
                RoomName = room.Name,
                BookingsCount = _database.Bookings.Count(b => b.ConferenceRoomId == room.Id)
            }).ToList();

            return new BookingsCountReport { BookingsCounts = bookingsCounts };
        }


        public async Task<TotalRevenueReport> GetTotalRevenueReportAsync()
        {
            var bookings = _database.Bookings.ToList();
            var rooms = _database.ConferenceRooms.ToList();

            var totalRevenue = bookings//з'єднує бронь з відповідним залом 
                .Join(rooms,
                    b => b.ConferenceRoomId,
                    r => r.Id,
                    (b, r) => new { Booking = b, Room = r })
                .Sum(br =>           //обч загальна вартість
                {
                    var duration = (decimal)(br.Booking.EndTime - br.Booking.StartTime).TotalHours;
                    var roomCost = duration * br.Room.BaseHourlyRate;
                    var servicesCost = br.Booking.BookedServices.Sum(s => s.Price);
                    return roomCost + servicesCost;
                });

            return new TotalRevenueReport { TotalRevenue = totalRevenue };
        }
    }
}
