using Programming_Web_API.Models;

namespace Programming_Web_API.DTO
{
    public class BookingDto
    {

        public int ID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }


        public int FK_BookedBy { get; set; } //Foreign Key
        //public User User { get; set; } 

        public int FK_CampingSpot { get; set; } //Foreign Key
        //public CampingSpot CampingSpot { get; set; } 
    }
}
