using System.ComponentModel.DataAnnotations.Schema;

namespace Programming_Web_API.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }



        //Collection Setup
        //1 user can have several Reviews (One-To-Many)
        public ICollection<Review> Reviews { get; set; }

        //1 user can have several Bookings (One-To-Many)
        public ICollection<Booking> Bookings { get; set; }

        //1 user can have Own Camping Spots (One-To-Many)
        public ICollection<CampingSpot> CampingSpot { get; set; }






        


    }
}
