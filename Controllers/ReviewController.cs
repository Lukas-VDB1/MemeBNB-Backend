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
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }



        //GET: Get All reviews
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var Reviews = _reviewRepository.GetReviews();  //This brings in the code from the "Repository" Folder, no need to hardcode anything

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);      //This prevents wrong data from being submitted
            }
            return Ok(Reviews);
        }


        //GET: Get A specific review
        [HttpGet("{review_id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReview(int reviewID)
        {
            var Review = _reviewRepository.GetReview(reviewID);  

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            return Ok(Review);
        }


        //GET: Get All Reviews of a camping spot
        [HttpGet("Reviews/CampingSpot/{campingspot_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetCampingSpotReviews(int camping_spot_id)
        {
            var reviews = _reviewRepository.GetCampingSpotReviews(camping_spot_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }


        //GET: Get All Reviews made by a User
        [HttpGet("Reviews/User/{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserReviews(int user_id)
        {
            var reviews = _reviewRepository.GetUserReviews(user_id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }







        //POST: Create a new review
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int campingSpotId,[FromBody] ReviewDto reviewCreate) 
        {

            //If user doesn't send data => Bad request
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(reviewCreate);         

            if (!_reviewRepository.CreateReview(reviewerId, campingSpotId, reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created a new review");      
        }



        //PUT: Update a review
        //[Authorize]
        [HttpPut("Edit/{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest(ModelState);
            }

            //if (reviewId != updatedReview.ID)
            //{
            //    return BadRequest(ModelState);
            //}

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        //DELETE: delete a review
        //[Authorize]
        [HttpDelete("Delete/{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            var reviewToDelete = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }

            return NoContent();
        }
    }
}
