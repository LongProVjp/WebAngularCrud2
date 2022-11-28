using WebApiTest.dto;
using WebApiTest.Model;

namespace WebApiTest.Repository
{
    public interface IProvinceRepository
    {
        ICollection<ModelProvince> GetProvinces();
        ModelProvince GetProvincebyName(string provinceName);
        ModelProvince GetProvincebyID(int Id);
        ModelProvince CreateProvince(Provincedto provincedto);
        ModelProvince DeleteProvince(ModelProvince objProvince);
        bool ProvinceExists(int provinceid);
    }
}
