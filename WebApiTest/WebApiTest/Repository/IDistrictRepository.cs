using WebApiTest.dto;
using WebApiTest.Model;

namespace WebApiTest.Repository
{
    public interface IDistrictRepository
    {
        ICollection<ModelDistrict> GetDistrict();
        ICollection<ModelDistrict> GetDistrict(int provinceid);
        ModelDistrict CreateDistrict(Districtdto districtdto);
        ModelDistrict DeleteDistrict(ModelDistrict objDistrict);
        bool DistrictExists(int districtid);
        bool ProvinceExists(int provinceid);
    }
}
