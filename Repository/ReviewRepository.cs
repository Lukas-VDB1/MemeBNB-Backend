using Programming_Web_API.Data;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;

namespace Programming_Web_API.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }


        //GET: get a specific review
        public Review GetReview(int reviewId)
        {
            var review = _context.Review.Where(p => p.ID == reviewId).FirstOrDefault(); ;
            return review;
        }


        //GET: get all reviews
        public ICollection<Review> GetReviews()
        {
            return _context.Review.OrderBy(p => p.ID).ToList();
        }

        //GET: get all reviews of a camping spot
        public ICollection<Review> GetCampingSpotReviews(int campingSpotId)
        {
            var campingSpotReviews = _context.Review.Where(p => p.FK_CampingSpot == campingSpotId).ToList();
            return campingSpotReviews;

        }

        //GET: get all reviews of a user
        public ICollection<Review> GetUserReviews(int userId)
        {
            var userReviews = _context.Review.Where(p => p.FK_Reviewer == userId).ToList();
            return userReviews;

        }



        //POST
        public bool CreateReview(int FK_Reviewer, int FK_Campingspot, Review review)
        {
            review.FK_Reviewer = FK_Reviewer;
            review.FK_CampingSpot = FK_Campingspot;
            _context.Add(review);
            return Save();
        }


        //PUT
        public bool UpdateReview(Review Review)
        {
            _context.Update(Review);
            return Save();
        }


        //DEL
        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }
        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges(); //Data is being turned into SQL query everytime changes are saved
            return saved > 0 ? true : false;
        }
    }
}
