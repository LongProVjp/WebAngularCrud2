using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.Arm;
using WebApiTest.dto;
using WebApiTest.Model;
using WebApiTest.Repository;
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
            return Ok(_mapper.Map<List<Provincedto>>(_provinceRepository.GetProvinces()));
        }
        [HttpGet("{Id}")]
        public IActionResult GetProvincebyID(int Id) {
            var province = _mapper.Map<Provincedto>(_provinceRepository.GetProvincebyID(Id));
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
            var province = _mapper.Map<Provincedto>(_provinceRepository.GetProvincebyName(provinceName));
            if (province == null)
            {
                return NotFound();
            }
            return Ok(province);
        }
        //Add
        [HttpPost]
        public IActionResult PostProvince(Provincedto provincedto)
        {
            if (_context.Provinces.Any(e => e.ProvinceName == provincedto.ProvinceName))
            {
                return NoContent();
            }
            if (provincedto.ProvinceName == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<ModelProvince>(_provinceRepository.CreateProvince(provincedto));
            return Ok(result);
        }
        //Update
        [HttpPut("{provinceid}")]
        public IActionResult PutProvince(int provinceid, Provincedto provincedto)
        {
            if (provinceid != provincedto.Id)
            {
                return BadRequest();
            }
            _context.Entry(_mapper.Map<ModelProvince>(provincedto)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_provinceRepository.ProvinceExists(provinceid))
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
        [HttpDelete("{provinceid}")]
        public IActionResult DeleteProvince(int provinceid)
        {
            var province = _context.Provinces.Find(provinceid);
            if (province == null)
            {
                return NotFound();
            }
            return Ok(_provinceRepository.DeleteProvince(province));
        }

    }
}
