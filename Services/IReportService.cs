using RoomRentalTZ_1at.Models;

namespace RoomRentalTZ_1at.Services
{
    public interface IReportService//інт який надає методи для отримання звітів
    {
        Task<BookingsCountReport> GetBookingsCountReportAsync();
        Task<TotalRevenueReport> GetTotalRevenueReportAsync();
    }
}
