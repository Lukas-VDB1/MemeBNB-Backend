namespace Programming_Web_API.DTO
{
    public class CampingSpotDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float CoordinatesLatitude { get; set; }
        public float CoordinatesLongitude { get; set; }
        public int PricePerNight { get; set; }
        public string MemeReference { get; set; }
        public string MemeVideoLink { get; set; }


        public int FK_Owner { get; set; }
        //public User User { get; set; } 
    }
}
