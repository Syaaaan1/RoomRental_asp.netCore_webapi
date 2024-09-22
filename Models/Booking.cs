namespace RoomRentalTZ_1at.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int ConferenceRoomId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<Service> BookedServices { get; set; } = new List<Service>(); //бронь сервісів з кімнатою. залежність Service.cs
    }
}
