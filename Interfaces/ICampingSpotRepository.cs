using Programming_Web_API.Models;

namespace Programming_Web_API.Interfaces
{
    public interface ICampingSpotRepository
    {


        //GET
        CampingSpot GetCampingSpot(int id); //Get a specific Camping Spot
        ICollection<CampingSpot> GetCampingSpots(); //Returns a list of all Camping Spots
        ICollection<CampingSpot> GetUserCampingSpots(int id); //Get all Camping spots made by a User


        //POST
        bool CreateCampingSpot(int FK_OwnerID, CampingSpot campingspot); 
        

        //PUT
        bool UpdateCampingSpot(CampingSpot campingspot);


        //DEL
        bool DeleteCampingSpot(CampingSpot campingspot);
        bool DeleteCampingSpots(List<CampingSpot> campingspots);




        bool Save();
    }
}
