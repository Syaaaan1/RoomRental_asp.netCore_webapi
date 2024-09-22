using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Services;

namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("bookings-count")]
        public async Task<ActionResult<BookingsCountReport>> GetBookingsCountReport()
        {
            var report = await _reportService.GetBookingsCountReportAsync();
            return Ok(report);
        }

        [HttpGet("total-revenue")]
        public async Task<ActionResult<TotalRevenueReport>> GetTotalRevenueReport()
        {
            var report = await _reportService.GetTotalRevenueReportAsync();
            return Ok(report);
        }
    }
}
