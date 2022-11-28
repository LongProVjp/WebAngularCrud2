using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.Arm;
using WebApiTest.dto;
using WebApiTest.Interfaces;
using WebApiTest.Model;
namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;


        public ProvinceController(IProvinceRepository provinceRepository, IMapper mapper,DataContext context)
        {
            _provinceRepository = provinceRepository ??
                throw new ArgumentNullException(nameof(provinceRepository));
            _mapper = mapper;
            _context = context;
        }
        //Get all
        [HttpGet]
        public IActionResult GetProvinces()
        {
            return Ok(_mapper.Map<List<ProvinceDto>>(_provinceRepository.GetProvinces()));
        }
        [HttpGet("{Id}")]
        public IActionResult GetProvincebyID(int Id) {
            var province = _mapper.Map<ProvinceDto>(_provinceRepository.GetProvincebyID(Id));
            if (province == null)
            {
                return NotFound();
            }
            return Ok(province);

        }
        //Search
        [HttpGet("GetbyName/{provinceName}")]
        public IActionResult GetProvincebyName(string provinceName)
        {
            var province = _mapper.Map<ProvinceDto>(_provinceRepository.GetProvincebyName(provinceName));
            if (province == null)
            {
                return NotFound();
            }
            return Ok(province);
        }
        //Add
        [HttpPost]
        public IActionResult PostProvince(ProvinceDto provinceDto)
        {
            if (_context.Provinces.Any(e => e.ProvinceName == provinceDto.ProvinceName))
            {
                return NoContent();
            }
            if (provinceDto.ProvinceName == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<Province>(_provinceRepository.CreateProvince(provinceDto));
            return Ok(result);
        }
        //Update
        [HttpPut("{provinceId}")]
        public IActionResult PutProvince(int provinceId, ProvinceDto provinceDto)
        {
            if (provinceId != provinceDto.Id)
            {
                return BadRequest();
            }
            _context.Entry(_mapper.Map<Province>(provinceDto)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_provinceRepository.ProvinceExists(provinceId))
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
        //Delete
        [HttpDelete("{provinceId}")]
        public IActionResult DeleteProvince(int provinceId)
        {
            var province = _context.Provinces.Find(provinceId);
            if (province == null)
            {
                return NotFound();
            }
            return Ok(_provinceRepository.DeleteProvince(province));
        }

    }
}
