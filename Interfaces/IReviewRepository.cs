using Programming_Web_API.Models;

namespace Programming_Web_API.Interfaces
{
    public interface IReviewRepository
    {

        //GET
        ICollection<Review> GetReviews(); //Returns a list of all Reviews
        Review GetReview(int id); //Get a specific review
        ICollection<Review> GetCampingSpotReviews(int id); //Get all reviews of a Camping Spot ID
        ICollection<Review> GetUserReviews(int id); //Get all reviews made by a User ID


        //POST
        bool CreateReview(int FK_Reviewer, int FK_Campingspot, Review review);

        //PUT
        bool UpdateReview(Review review);


        //DEL
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);



        bool Save();
    }
}
