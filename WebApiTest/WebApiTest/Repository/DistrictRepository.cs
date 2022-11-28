using WebApiTest.dto;
using WebApiTest.Model;

namespace WebApiTest.Repository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly DataContext _context;
        public DistrictRepository(DataContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public ModelDistrict CreateDistrict(Districtdto districtdto)
        {
            var data = new ModelDistrict()
            {
                Id = districtdto.Id,
                DistrictName = districtdto.DistrictName,
                ProvinceID = districtdto.ProvinceID,
                Province = null
            };
            _context.Districts.Add(data);
            _context.SaveChanges();
            return data;
        }
        public ModelDistrict DeleteDistrict(ModelDistrict objDistrict)
        {
            _context.Remove(objDistrict);
            _context.SaveChanges();
            return objDistrict;
        }

        public bool DistrictExists(int districtid)
        {
            return _context.Districts.Any(r => r.Id == districtid);
        }

        public ICollection<ModelDistrict> GetDistrict()
        {
            return _context.Districts.ToList();
        }
        public ICollection<ModelDistrict> GetDistrict(int provinceid)
        {
            return _context.Districts.Where(p => p.ProvinceID == provinceid).ToList();
        }

        public bool ProvinceExists(int provinceid)
        {
            return _context.Provinces.Any(r => r.Id == provinceid);
            
        }
      
    }
}
