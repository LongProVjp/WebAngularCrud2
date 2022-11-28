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
            CreateMap<ModelProvince, Provincedto>();
            CreateMap<Provincedto, ModelProvince>();
            CreateMap<ModelDistrict, Districtdto>();
            CreateMap<Districtdto,ModelDistrict>();
        }
    }
}
