using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.dto;
using WebApiTest.Interfaces;
using WebApiTest.Model;

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
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDistrict()
        {
            return Ok(_mapper.Map<List<DistrictDto>>(_districtRepository.GetDistrict()));
        }
        /// <summary>
        /// Getdistrictbyprovinceid
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        [HttpGet("{provinceId}")]
        public IActionResult GetDistrictByProvinceID(int provinceId)
        {
            if (!_districtRepository.ProvinceExists(provinceId))
            {
                return NotFound();
            }
            var result = _mapper.Map<List<DistrictDto>>(_districtRepository.GetDistrict(provinceId));
            return Ok(result);
        }
        /// <summary>
        /// Get district with province
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get all province
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProvince")]
        public IActionResult GetAllProvince()
        {
            return Ok(_mapper.Map<List<ProvinceDto>>(_provinceRepository.GetProvinces()));
        }
        /// <summary>
        /// Add District
        /// </summary>
        /// <param name="districtDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostDistrict(DistrictDto districtDto)
        {
            if (!_districtRepository.ProvinceExists(districtDto.ProvinceID))
            {
                return BadRequest("Province not Exists");
            }
            if (_context.Districts.Any(e => (e.ProvinceID == districtDto.ProvinceID) && (e.DistrictName == districtDto.DistrictName)))
            {
                return BadRequest("District already exist");
            }
            var result = _mapper.Map<District>(_districtRepository.CreateDistrict(districtDto));
            return Ok(result);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        [HttpDelete("{districtId}")]
        public IActionResult DeleteDistrict(int districtId)
        {
            var district = _context.Districts.Find(districtId);
            if (district == null)
            {
                return NotFound();
            }
            return Ok(_districtRepository.DeleteDistrict(district));
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="districtDto"></param>
        /// <returns></returns>
        [HttpPut("{districtId}")]
        public IActionResult PutDistrict(int districtId, DistrictDto districtDto)
        {
            if (districtId != districtDto.Id)
            {
                return BadRequest();
            }
            if (!_districtRepository.ProvinceExists(districtDto.ProvinceID))
            {
                return BadRequest();
            }
            if (_context.Districts.Any(e => (e.ProvinceID == districtDto.ProvinceID) && (e.DistrictName== districtDto.DistrictName)))
            {
                return BadRequest();
            }
            _context.Entry(_mapper.Map<District>(districtDto)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_districtRepository.DistrictExists(districtId))
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
