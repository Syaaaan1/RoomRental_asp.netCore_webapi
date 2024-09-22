namespace RoomRentalTZ_1at.Models
{
    public class BookingsCountReport
    {
        public List<RoomBookingsCount> BookingsCounts { get; set; }//список кількості бронювань 
    }

    public class RoomBookingsCount
    {
        public string RoomName { get; set; }
        public int BookingsCount { get; set; }
    }
}
