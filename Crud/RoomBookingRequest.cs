using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RoomRentalTZ_1at.Crud
{
    public class RoomBookingRequest
    {
        [Required]
        public int ConferenceRoomId { get; set; }

        [DataType(DataType.DateTime)]//вказуэмо що StartTime представляє собою значення типу дати і часу
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd HH:mm}")]//задаєм формат відображення та прописуємо, що форматування буде застосовуватись не тільки при відображенні даних, але й під час редагування.
        public string StartTime { get; set; }

        [Required]
        public double Duration { get; set; } // тривалість в годинах 

        public List<int> ServiceIds { get; set; } = new List<int>(); // ID вибраних послуг
    }
}

