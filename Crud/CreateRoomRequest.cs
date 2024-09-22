namespace RoomRentalTZ_1at.Crud
{
    //моделі запиту 
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<ServiceRequest> AvailableServices { get; set; }
        public decimal BaseHourlyRate { get; set; }
    }

    public class ServiceRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
