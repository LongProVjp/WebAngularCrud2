using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest.Model
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public ICollection<District> District { get; set; }
    }
}
