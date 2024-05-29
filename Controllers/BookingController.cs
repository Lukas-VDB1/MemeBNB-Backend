using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Programming_Web_API.DTO;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;
using Programming_Web_API.Repository;

namespace Programming_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }




        //GET: Get All bookings
        //[Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetBookings()
        {
            var Bookings = _bookingRepository.GetBookings();  //This brings in the code from the "Repository" Folder, no need to hardcode anything

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);      //This prevents wrong data from being submitted
            }
            return Ok(Bookings);
        }


        //GET: Get A specific booking
        //[Authorize]
        [HttpGet("{booking_id}")]
        [ProducesResponseType(200, Type = typeof(Booking))]
        public IActionResult GetReview(int bookingID)
        {
            var Booking = _bookingRepository.GetBooking(bookingID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Booking);
        }


        //GET: Get All Bookings of a Campingspot
        //[Authorize]
        [HttpGet("Bookings/CampingSpot/{campingspot_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetCampingSpotBookings(int camping_spot_id)
        {
            var bookings = _bookingRepository.GetCampingSpotBookings(camping_spot_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookings);
        }


        //GET: Get All Bookings made by a user
        //[Authorize]
        [HttpGet("Bookings/User/{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetUserBookings(int user_id)
        {
            var bookings = _bookingRepository.GetUserBookings(user_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookings);
        }





        //POST: Create a new booking
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBooking([FromQuery] int bookerId, [FromQuery] int campingSpotId, [FromBody] BookingDto bookingCreate)
        {

            //If user doesn't send data => Bas request
            if (bookingCreate == null)
            {
                return BadRequest(ModelState);
            }

            var bookingMap = _mapper.Map<Booking>(bookingCreate);

            if (!_bookingRepository.CreateBooking(bookerId, campingSpotId, bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created a new booking");
        }




        //PUT: Update a booking
        //[Authorize]
        [HttpPut("Edit/{bookingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBooking(int bookingId, [FromBody] BookingDto updatedBooking)
        {
            if (updatedBooking == null)
            {
                return BadRequest(ModelState);
            }

            if (bookingId != updatedBooking.ID)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingMap = _mapper.Map<Booking>(updatedBooking);

            if (!_bookingRepository.UpdateBooking(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating booking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




        //DELETE: delete a booking
        //[Authorize]
        [HttpDelete("Delete/{bookingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBooking(int bookingId)
        {
            var bookingToDelete = _bookingRepository.GetBooking(bookingId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookingRepository.DeleteBooking(bookingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting booking");
            }

            return NoContent();
        }
    }
}
