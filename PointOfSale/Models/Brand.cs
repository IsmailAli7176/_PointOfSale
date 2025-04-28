using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class Brand
    {
        [Key]
        [DisplayName("Brand Id")]
        public int Brand_Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Brand Name")]
        public string Brand_Name { get; set; }

        [Required]
        [DisplayName("Brand Description")]
        public string Brand_Description { get; set; }

        public List<Products>? Products { get; set; }
    }
}
