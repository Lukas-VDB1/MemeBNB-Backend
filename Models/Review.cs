namespace Programming_Web_API.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public DateOnly DatePosted { get; set; }
        public string Comment { get; set; }



        //Foreign Key Setup
        public int FK_Reviewer { get; set; } //Foreign Key ID
        public User Reviewer { get; set; } // Navigation property


        public int FK_CampingSpot { get; set; } //Foreign Key ID
        public CampingSpot CampingSpot { get; set; } // Navigation property

    }
}
