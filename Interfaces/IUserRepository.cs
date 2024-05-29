using Programming_Web_API.Models;

namespace Programming_Web_API.Interfaces
{
    public interface IUserRepository
    {
        //GET
        User GetUser(int id); //Get a user based on ID
        User GetUserByEmail(string email); //Get a user based on Email
        ICollection<User> GetUsers(); //Returns a list (ICollection) of all Users
        ICollection<Booking> GetUserBookings(int id); //Get all bookings of a user ID
        ICollection<Review> GetUserReviews(int id); //Get all Reviews of a user ID
        ICollection<CampingSpot> GetUserCampingSpots(int id); //Get all Campingspots of a user ID
        public bool UserEmailExists(string email); //Check if user exists with this Email address


        //POST
        bool CreateUser(User user);


        //PUT
        bool UpdateUser(User user);


        //DEL
        bool DeleteUser(User user);


        bool Save();
    }
}
