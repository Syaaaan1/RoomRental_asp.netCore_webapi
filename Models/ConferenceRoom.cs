namespace RoomRentalTZ_1at.Models
{
    public class ConferenceRoom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public decimal BaseHourlyRate { get; set; }

        public List<Service> AvailableServices { get; set; } = new List<Service>(); // список сервісів
    }
}
