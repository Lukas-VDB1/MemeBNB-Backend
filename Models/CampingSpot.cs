using System.ComponentModel.DataAnnotations.Schema;

namespace Programming_Web_API.Models
{
    public class CampingSpot
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float CoordinatesLatitude { get; set; }
        public float CoordinatesLongitude { get; set; }
        public int PricePerNight { get; set; }
        public string MemeReference { get; set; }
        public string MemeVideoLink { get; set; }


        //Foreign Key Setups
        public int FK_Owner { get; set; } //Foreign Key ID
        public User Owner { get; set; } // Navigation property



        //Collection Setup
        //1 Camping Spot can have several Reviews (One-To-Many)
        public ICollection<Review> Reviews { get; set; }

        //1 Camping Spot can have several Bookings (One-To-Many)
        public ICollection<Booking> Bookings { get; set; }



    }
}
