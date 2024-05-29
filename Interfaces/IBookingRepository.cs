using Programming_Web_API.Models;

namespace Programming_Web_API.Interfaces
{
    public interface IBookingRepository
    {

        //GET
        ICollection<Booking> GetBookings(); //Returns a list of all Bookings
        Booking GetBooking(int id); //Get a specific booking
        ICollection<Booking> GetCampingSpotBookings(int id); //Get all bookings of a Camping Spot ID
        ICollection<Booking> GetUserBookings(int id); //Get all bookings made by a user ID



        //POST
        bool CreateBooking(int FK_BookedBy, int FK_CampingSpot, Booking booking);


        //PUT
        bool UpdateBooking(Booking booking);


        //DEL
        bool DeleteBooking(Booking booking);
        bool DeleteBookings(List<Booking> bookings);



        bool Save();
    }
}
