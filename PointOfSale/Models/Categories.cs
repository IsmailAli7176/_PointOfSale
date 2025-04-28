using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
    public class Categories
    {
        [Key]
        [DisplayName("Category Id")]
        public int Cat_Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string Cat_Name { get; set; }

        [Required]
        [DisplayName("Category Description")]
        public string Cat_Description { get; set; }

        [Required]
        [DisplayName("Category Status")]
        public int Cat_Status { get; set; } // 1 = Active, 0 = Inactive

        public List<Products>? Products { get; set; }
    }
}
