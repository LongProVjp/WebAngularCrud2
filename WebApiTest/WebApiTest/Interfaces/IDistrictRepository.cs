using WebApiTest.dto;
using WebApiTest.Model;

namespace WebApiTest.Interfaces
{
    public interface IDistrictRepository
    {
        ICollection<District> GetDistrict();
        ICollection<District> GetDistrict(int provinceId);
        District CreateDistrict(DistrictDto districtDto);
        District DeleteDistrict(District objDistrict);
        bool DistrictExists(int districtId);
        bool ProvinceExists(int provinceId);
    }
}
