using WebApiTest.dto;
using WebApiTest.Model;

namespace WebApiTest.Interfaces
{
    public interface IProvinceRepository
    {
        ICollection<Province> GetProvinces();
        Province GetProvincebyName(string provinceName);
        Province GetProvincebyID(int Id);
        Province CreateProvince(ProvinceDto provinceDto);
        Province DeleteProvince(Province objProvince);
        bool ProvinceExists(int provinceId);
    }
}
