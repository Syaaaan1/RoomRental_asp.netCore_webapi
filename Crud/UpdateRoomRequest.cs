namespace RoomRentalTZ_1at.Crud
{
    public class UpdateRoomRequest
    {
        public string Name { get; set; }

        public int? Capacity { get; set; }

        public decimal? BaseHourlyRate { get; set; }

        public List<ServiceRequest> AddServices { get; set; }

        public List<int> RemoveServiceIds { get; set; }
    }
}

