using RoomRentalTZ_1at.Models;

namespace RoomRentalTZ_1at.Data
{

    //
    //"Заглушка" бд
    //
    public class InMemoryDatabase
    {
        public List<ConferenceRoom> ConferenceRooms { get; set; }
        public List<Service> Services { get; set; }
        public List<Booking> Bookings { get; set; }

        public InMemoryDatabase()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            Services = new List<Service>
        {
            new Service { Id = 1, Name = "Проєктор", Price = 500 },
            new Service { Id = 2, Name = "Wi-Fi", Price = 300 },
            new Service { Id = 3, Name = "Звук", Price = 700 }
        };

            ConferenceRooms = new List<ConferenceRoom>
        {
            new ConferenceRoom
            {
                Id = 1,
                Name = "Зал А",
                Capacity = 50,
                BaseHourlyRate = 2000,
                AvailableServices = new List<Service> { Services[0], Services[1] }
            },
            new ConferenceRoom
            {
                Id = 2,
                Name = "Зал B",
                Capacity = 100,
                BaseHourlyRate = 3500,
                AvailableServices = new List<Service> { Services[0], Services[1], Services[2] }
            },
            new ConferenceRoom
            {
                Id = 3,
                Name = "Зал C",
                Capacity = 30,
                BaseHourlyRate = 1500,
                AvailableServices = new List<Service> { Services[1] }
            }
        };

            Bookings = new List<Booking>();
        }
    }
}
