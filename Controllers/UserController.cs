using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Programming_Web_API.Data;
using Programming_Web_API.DTO;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;
using Programming_Web_API.Repository;

namespace Programming_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ICampingSpotRepository _campingSpotRepository;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }



        //GET: Get All Users
        //[Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var Users = _userRepository.GetUsers();  //This brings in the code from the "Repository" Folder, no need to hardcode anything

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);      //This prevents wrong data from being submitted
            }
            return Ok(Users);
        }


        //GET: Check if an email is already being used

        [HttpGet("userExists/{user_email}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public bool UserEmailExists(string email)
        {
            return _userRepository.UserEmailExists(email);
        }



        //GET: Get User by User ID
        //[Authorize]
        [HttpGet("user/{user_id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id) 
        {
            var user = _userRepository.GetUser(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }


        //GET: Get User by Email
        //[Authorize]
        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByEmail(string email)
        {

            var user = _userRepository.GetUserByEmail(email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }



        //GET: Get All Bookings based on a user ID
        //[Authorize]
        [HttpGet("booking/{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserBookings(int id)
        {
            var userBookings = _userRepository.GetUserBookings(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);      
            }
            return Ok(userBookings);
        }


        //GET: Get All Reviews based on a user ID
        [HttpGet("review/{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserReviews(int id)
        {
            var userReviews = _userRepository.GetUserReviews(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(userReviews);
        }







        //POST: Create a new user
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {

            //If user doesn't send data => Bad request
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserEmailExists(userCreate.Email))
            {
                var userMap = _mapper.Map<User>(userCreate);

                //Hash the password of the user
                userMap.Password = BCrypt.Net.BCrypt.HashPassword(userCreate.Password);

                if (!_userRepository.CreateUser(userMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("", "This user email already exists.");
                return StatusCode(500, ModelState);
            }

          

            return Ok("Succesfully created a new user");
        }




        //PUT: update a user
        //[Authorize]
        [HttpPut("Edit/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if(updatedUser == null)
            {
                return BadRequest(ModelState);
            }

            if (userId != updatedUser.ID)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(updatedUser);
            userMap.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
            
            

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        //DELETE: Delete a user
        //[Authorize]
        [HttpDelete("Delete/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            //When a user gets deleted, all the reviews, bookings and campingspots he created should be deleted too
            var reviewsToDelete = _reviewRepository.GetUserReviews(userId);
            var bookingsToDelete = _bookingRepository.GetUserBookings(userId);
            var campingSpotsToDelete = _campingSpotRepository.GetUserCampingSpots(userId);

            var userToDelete = _userRepository.GetUser(userId);



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

            if (!_campingSpotRepository.DeleteCampingSpots(campingSpotsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting camping spots");
            }

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return NoContent();
        }

    }
}
