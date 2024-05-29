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
   

    public class CampingSpotController : Controller
    {
        private readonly ICampingSpotRepository _campingSpotRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookingRepository _bookingRepository;

        public CampingSpotController(ICampingSpotRepository campingSpotRepository, IMapper mapper) 
        {
            _campingSpotRepository = campingSpotRepository;
            _mapper = mapper;
        }




        //GET: Get All CampingSpots
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CampingSpot>))]
        public IActionResult GetCampingSpots()
        {
            var campingSpots = _campingSpotRepository.GetCampingSpots(); //(BEFORE automapper, je moet denk niks mappen bij deze)
            //var campingSpots = _mapper.Map<CampingSpotDto>(_campingSpotRepository.GetCampingSpots()); //(AFTER AUTOMAPPER)
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(campingSpots);
        }


        //GET: Get A specific camping spot
        [HttpGet("{campingSpot_id}")]
        [ProducesResponseType(200, Type = typeof(CampingSpot))]
        public IActionResult GetReview(int campingSpotID)
        {
            //var CampingSpot = _campingSpotRepository.GetCampingSpot(campingSpotID);
            var CampingSpot = _mapper.Map<CampingSpotDto>(_campingSpotRepository.GetCampingSpot(campingSpotID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(CampingSpot);
        }


        //GET: Get All Campingspots belonging to a User
        [HttpGet("CampingSpots/{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CampingSpot>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserCampingSpots(int user_id)
        {
            var campingSpots = _campingSpotRepository.GetUserCampingSpots(user_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(campingSpots);
        }



      

        //POST: Create a new camping spot
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCampingSpot([FromQuery] int ownerId,[FromBody] CampingSpotDto campingspotCreate)
        {

            //If user doesn't send data => Bad request
            if (campingspotCreate == null)
            {
                return BadRequest(ModelState);
            }

            var campingspotMap = _mapper.Map<CampingSpot>(campingspotCreate);

            if (!_campingSpotRepository.CreateCampingSpot(ownerId, campingspotMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created a new camping spot");
        }





        //PUT: update a camping spot
        //[Authorize]
        [HttpPut("Edit/{campingSpotId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCampingSpot(int campingSpotId, [FromBody] CampingSpotDto updatedCampingSpot)
        {
            if (updatedCampingSpot == null)
            {
                return BadRequest(ModelState);
            }

            if (campingSpotId != updatedCampingSpot.ID)
            {
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var campingSpotMap = _mapper.Map<CampingSpot>(updatedCampingSpot);

            if (!_campingSpotRepository.UpdateCampingSpot(campingSpotMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating camping spot");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        //DELETE: delete a camping spot
        //[Authorize]
        [HttpDelete("Delete/{campingSpotId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCampingSpot(int campingSpotId)
        {
            //When a camping spot gets deleted, all the reviews and bookings about this camping spot should be deleted too
            var reviewsToDelete = _reviewRepository.GetCampingSpotReviews(campingSpotId);
            var bookingsToDelete = _bookingRepository.GetCampingSpotBookings(campingSpotId);

            var campingSpotToDelete = _campingSpotRepository.GetCampingSpot(campingSpotId);

            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviews");
            }


            if (!_bookingRepository.DeleteBookings(bookingsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting bookings");
            }


            if (!_campingSpotRepository.DeleteCampingSpot(campingSpotToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting camping spot");
            }

            return NoContent();
        }
    }

}