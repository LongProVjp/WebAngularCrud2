using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.dto;
using WebApiTest.Model;
using WebApiTest.Repository;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IProvinceRepository _provinceRepository;
        public DistrictController(IDistrictRepository districtRepository, IMapper mapper, DataContext context, IProvinceRepository provinceRepository)
        {
            _districtRepository = districtRepository ??
                 throw new ArgumentNullException(nameof(districtRepository));
            _mapper = mapper;
            _context  = context;
            _provinceRepository = provinceRepository;

        }
        [HttpGet]
        public IActionResult GetDistrict()
        {
            return Ok(_mapper.Map<List<Districtdto>>(_districtRepository.GetDistrict()));
        }

        [HttpGet("{provinceid}")]
        public IActionResult GetDistrictByProvinceID(int provinceid)
        {
            if (!_districtRepository.ProvinceExists(provinceid))
            {
                return NotFound();
            }
            var result = _mapper.Map<List<Districtdto>>(_districtRepository.GetDistrict(provinceid));
            return Ok(result);
        }

        [HttpGet("withProvince")]
        public IActionResult GetDistrictWithProvince()
        {
            var data = (from a in _context.Provinces
                        from b in _context.Districts
                        where a.Id == b.ProvinceID
                        select new
                        {
                            Id = b.Id,
                            DistrictName = b.DistrictName,
                            ProvinceID = b.ProvinceID,
                            ProvinceName = a.ProvinceName,
                        }
                );

            return Ok(data);
        }

        [HttpGet("GetAllProvince")]
        public IActionResult GetAllProvince()
        {
            return Ok(_mapper.Map<List<Provincedto>>(_provinceRepository.GetProvinces()));
        }

        [HttpPost]
        public IActionResult PostDistrict(Districtdto districtdto)
        {
            if (!_districtRepository.ProvinceExists(districtdto.ProvinceID))
            {
                return BadRequest("Province not Exists");
            }
            if (_context.Districts.Any(e => (e.ProvinceID == districtdto.ProvinceID) && (e.DistrictName == districtdto.DistrictName)))
            {
                return BadRequest("District already exist");
            }
            var result = _mapper.Map<ModelDistrict>(_districtRepository.CreateDistrict(districtdto));
            return Ok(result);
        }

        [HttpDelete("{districtid}")]
        public IActionResult DeleteDistrict(int districtid)
        {
            var district = _context.Districts.Find(districtid);
            if (district == null)
            {
                return NotFound();
            }
            return Ok(_districtRepository.DeleteDistrict(district));
        }
        [HttpPut("{districtid}")]
        public IActionResult PutDistrict(int districtid, Districtdto districtdto)
        {
            if (districtid != districtdto.Id)
            {
                return BadRequest();
            }
            if (!_districtRepository.ProvinceExists(districtdto.ProvinceID))
            {
                return BadRequest();
            }
            if (_context.Districts.Any(e => (e.ProvinceID == districtdto.ProvinceID) && (e.DistrictName== districtdto.DistrictName)))
            {
                return BadRequest();
            }
            _context.Entry(_mapper.Map<ModelDistrict>(districtdto)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_districtRepository.DistrictExists(districtid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
    }
}
