using AutoMapper;
using System.Net;
using WebApiTest.dto;
using WebApiTest.Model;

namespace WebApiTest.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Province, ProvinceDto>();
            CreateMap<ProvinceDto, Province>();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictDto,District>();
        }
    }
}
