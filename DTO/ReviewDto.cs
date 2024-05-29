using Programming_Web_API.Models;

namespace Programming_Web_API.DTO
{
    public class ReviewDto
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public DateOnly DatePosted { get; set; }
        public string Comment { get; set; }

        public int FK_Reviewer { get; set; } 
        //public User User { get; set; } 

        public int FK_CampingSpot { get; set; } 
        //public CampingSpot CampingSpot { get; set; } 
    }
}
