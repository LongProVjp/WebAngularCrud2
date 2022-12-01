
using AutoMapper;
using WebApiTest.dto;
using WebApiTest.Interfaces;
using WebApiTest.Model;

namespace WebApiTest.Repository
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProvinceRepository(DataContext context, IMapper mapper)
        {
            _context = context ??
                 throw new ArgumentNullException(nameof(context));
            _mapper = mapper;

        }
        public Province CreateProvince(ProvinceDto provinceDto)
        {

            var data = new Province()
            {
                Id = provinceDto.Id,
                ProvinceName = provinceDto.ProvinceName,
                District = null
            };
            _context.Provinces.Add(data);
            _context.SaveChanges();
            return data;
        }
        public ICollection<Province> GetProvinces()
        {
            return _context.Provinces.ToList();
        }
        public bool ProvinceExists(int provinceId)
        {
            return _context.Provinces.Any(r => r.Id == provinceId);
        }
        public Province GetProvincebyName(string provinceName)
        {
            return _context.Provinces.Where(p => p.ProvinceName == provinceName).FirstOrDefault();
        }

        public Province DeleteProvince(Province objProvince)
        {
            _context.Remove(objProvince);
            _context.SaveChanges();
            return objProvince;
        }

        public Province GetProvincebyID(int Id)
        {
            return _context.Provinces.Where(p => p.Id == Id).FirstOrDefault();
        }
    }
}
