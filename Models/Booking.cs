namespace Programming_Web_API.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }



        //Foreign Key Setup
        public int FK_BookedBy { get; set; } //Foreign Key ID
        public User Booker { get; set; } //Navigation property


        public int FK_CampingSpot { get; set; } //Foreign Key ID
        public CampingSpot CampingSpot { get; set; } //Navigation property



    }
}
