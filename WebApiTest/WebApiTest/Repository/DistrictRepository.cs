using WebApiTest.dto;
using WebApiTest.Interfaces;
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

        public District CreateDistrict(DistrictDto districtDto)
        {
            var data = new District()
            {
                Id = districtDto.Id,
                DistrictName = districtDto.DistrictName,
                ProvinceID = districtDto.ProvinceID,
                Province = null
            };
            _context.Districts.Add(data);
            _context.SaveChanges();
            return data;
        }
        public District DeleteDistrict(District objDistrict)
        {
            _context.Remove(objDistrict);
            _context.SaveChanges();
            return objDistrict;
        }

        public bool DistrictExists(int districtId)
        {
            return _context.Districts.Any(r => r.Id == districtId);
        }
        public ICollection<District> GetDistrict()
        {
            return _context.Districts.ToList();
        }
        public ICollection<District> GetDistrict(int provinceid)
        {
            return _context.Districts.Where(p => p.ProvinceID == provinceid).ToList();
        }

        public bool ProvinceExists(int provinceId)
        {
            return _context.Provinces.Any(r => r.Id == provinceId);
            
        }
      
    }
}
