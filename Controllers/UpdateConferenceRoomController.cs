using Microsoft.AspNetCore.Mvc;
using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Models;
using RoomRentalTZ_1at.Crud;
using System.Globalization;

namespace RoomRentalTZ_1at.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateConferenceRoomController : Controller
    {
        private readonly InMemoryDatabase _database;
        public UpdateConferenceRoomController(InMemoryDatabase database)
        {
            _database = database;
        }

        [HttpPut("UpdateConferenceRoomController/{id}")]
        public ActionResult<ConferenceRoom> UpdateConferenceRoom(int id, UpdateRoomRequest request)
        {
            var room = _database.ConferenceRooms.FirstOrDefault(x => x.Id == id);
            if (room == null)
            {
                return NotFound($"Conference room with ID {id} not found.");
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                room.Name = request.Name;
            }
            if (request.Capacity.HasValue && request.Capacity > 0)
            {
                room.Capacity = request.Capacity.Value;
            }
            if (request.BaseHourlyRate.HasValue && request.BaseHourlyRate > 0)
            {
                room.BaseHourlyRate = request.BaseHourlyRate.Value;
            }

            if (request.AddServices != null)
            {
                foreach (var ServiceRequest in request.AddServices)
                {
                    if (string.IsNullOrWhiteSpace(ServiceRequest.Name)) { }
                    else
                    {
                        var newService = new Service
                        {
                            Id = _database.Services.Count + 1,
                            Name = ServiceRequest.Name,
                            Price = ServiceRequest.Price,
                        };

                        _database.Services.Add(newService);
                        room.AvailableServices.Add(newService);
                    }
                }
            }

            if (request.RemoveServiceIds != null)
            {
                room.AvailableServices.RemoveAll(x => request.RemoveServiceIds.Contains(x.Id));
            }

            return Ok(new { Message = "Conference room updated successfully", Room = room });

        }
    }
}
