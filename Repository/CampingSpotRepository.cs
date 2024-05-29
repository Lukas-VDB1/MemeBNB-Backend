using Programming_Web_API.Data;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Models;


namespace Programming_Web_API.Repository
{
    public class CampingSpotRepository : ICampingSpotRepository     //Inherit Interface from ICampingSpotRepository
    {
        private readonly DataContext _context;
        public CampingSpotRepository(DataContext context) 
        {
            _context = context;
        }


        //GET: get a specific Camping Spot
        public CampingSpot GetCampingSpot(int campingSpotId)
        {
            var campingSpot = _context.CampingSpot.Where(p => p.ID == campingSpotId).FirstOrDefault();
            return campingSpot;
        }

        //GET: get all camping spots
        public ICollection<CampingSpot> GetCampingSpots() 
        {
            return _context.CampingSpot.OrderBy(p => p.ID).ToList();
        }



        //GET: get all camping spots of a user
        public ICollection<CampingSpot> GetUserCampingSpots(int userId)
        {
            var userCampingSpots = _context.CampingSpot.Where(p => p.FK_Owner == userId).ToList();
            return userCampingSpots;

        }



        //POST
        public bool CreateCampingSpot(int ownerID, CampingSpot campingspot)
        {
            campingspot.FK_Owner = ownerID;
            _context.Add(campingspot); //(context.add only takes 1 parameter so FK is added in line above)
            return Save();
        }


        //PUT
        public bool UpdateCampingSpot(CampingSpot campingspot)
        {
            _context.Update(campingspot);
            return Save();
        }



        //DEL
        public bool DeleteCampingSpot(CampingSpot campingspot)
        {
            _context.Remove(campingspot);
            return Save();
        }
        public bool DeleteCampingSpots(List<CampingSpot> campingspots)
        {
            _context.RemoveRange(campingspots);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
