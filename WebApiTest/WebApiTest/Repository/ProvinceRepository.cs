using AutoMapper;
using WebApiTest.dto;
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
        public ModelProvince CreateProvince(Provincedto provincedto)
        {

            var data = new ModelProvince()
            {
                Id = provincedto.Id,
                ProvinceName = provincedto.ProvinceName,
                District = null
            };
            _context.Provinces.Add(data);
            _context.SaveChanges();
            return data;
        }
        public ICollection<ModelProvince> GetProvinces()
        {
            return _context.Provinces.ToList();
        }
        public bool ProvinceExists(int provinceid)
        {
            return _context.Provinces.Any(r => r.Id == provinceid);
        }
        public ModelProvince GetProvincebyName(string provinceName)
        {
            return _context.Provinces.Where(p => p.ProvinceName == provinceName).FirstOrDefault();
        }

        public ModelProvince DeleteProvince(ModelProvince objProvince)
        {
            _context.Remove(objProvince);
            _context.SaveChanges();
            return objProvince;
        }

        public ModelProvince GetProvincebyID(int Id)
        {
            return _context.Provinces.Where(p => p.Id == Id).FirstOrDefault();
        }
    }
}
