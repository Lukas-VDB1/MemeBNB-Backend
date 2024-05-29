using Programming_Web_API.Data;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;

namespace Programming_Web_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        //GET
        public bool UserEmailExists(string email)
        {
            return _context.User.Any(p => p.Email == email);
        }


        //GET: Get a list of all users
        public ICollection<User> GetUsers()
        {
            return _context.User.OrderBy(p => p.ID).ToList();
        }


        //GET: Get a User
        public User GetUser(int id)
        {
            return _context.User.Where(p => p.ID == id).FirstOrDefault();
        }


        //GET: Get a User by email
        public User GetUserByEmail(string email)
        {
            return _context.User.Where(p => p.Email == email).FirstOrDefault();
        }


        //GET: Get all bookings of a User
        public ICollection<Booking> GetUserBookings(int id)
        {
            var bookings = _context.Booking.Where(p => p.ID == id).ToList();
            return bookings;
        }


        //GET: Get all Camping Spots of a user
        public ICollection<CampingSpot> GetUserCampingSpots(int id)
        {
            var campingSpots = _context.CampingSpot.Where(p => p.ID == id).ToList();
            return campingSpots;
        }


        //GET: Get all reviews of a user
        public ICollection<Review> GetUserReviews(int id)
        {     
            var reviews = _context.Review.Where(p => p.ID == id).ToList();         
            return reviews;
        }




        //POST
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        
        //PUT
        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }


        //DEL
        public bool DeleteUser(User user) 
        {
            _context.Remove(user);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
