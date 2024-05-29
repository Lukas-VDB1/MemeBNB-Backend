using Programming_Web_API.Data;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;

namespace Programming_Web_API.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRepository(DataContext context)
        {
            _context = context;
        }


        //GET: get all reviews
        public ICollection<Booking> GetBookings()
        {
            return _context.Booking.OrderBy(p => p.ID).ToList();
        }

        //GET: get a specific booking
        public Booking GetBooking(int bookingId)
        {
            var booking = _context.Booking.Where(p => p.ID == bookingId).FirstOrDefault(); ;
            return booking;
        }

        //GET: get all Bookings of a camping spot
        public ICollection<Booking> GetCampingSpotBookings(int campingSpotId)
        {
            var campingSpotBookings = _context.Booking.Where(p => p.FK_CampingSpot == campingSpotId).ToList();
            return campingSpotBookings;
        }


        //GET: get all Bookings of a User
        public ICollection<Booking> GetUserBookings(int userId)
        {
            var userBookings = _context.Booking.Where(p => p.FK_BookedBy == userId).ToList();
            return userBookings;
        }




        //POST
        public bool CreateBooking(int FK_BookedBy, int FK_CampingSpot, Booking booking)
        {
            booking.FK_BookedBy = FK_BookedBy;
            booking.FK_CampingSpot = FK_CampingSpot;
            _context.Add(booking);
            return Save();
        }

      

        //PUT
        public bool UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            return Save();
        }


        //DEL
        public bool DeleteBooking(Booking booking)
        {
            _context.Remove(booking);
            return Save();
        }

        public bool DeleteBookings(List<Booking> bookings)
        {
            _context.RemoveRange(bookings);
            return Save();
        }



        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
