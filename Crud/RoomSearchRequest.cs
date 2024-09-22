using System.ComponentModel.DataAnnotations;

public class RoomSearchRequest
{
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    public string StartTime { get; set; } //строки!!!

    [Required]
    public string EndTime { get; set; } 

    public int Capacity { get; set; }

    public TimeSpan GetStartTimeSpan()
    {
        return TimeSpan.ParseExact(StartTime, @"hh\:mm", null); //строка => временной интервал
    }

    public TimeSpan GetEndTimeSpan()
    {
        return TimeSpan.ParseExact(EndTime, @"hh\:mm", null); //строка => временной интервал
    }

}
