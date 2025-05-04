using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSale.Models
{
    public class Products
    {
        [Key]
        [DisplayName("Product Id")]
        public int Product_Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Product_Name { get; set; }

        [Required]
        [DisplayName("Product Price")]
        public decimal Product_Price { get; set; }
        [Required]
        [DisplayName("Product Quantity")]
        public decimal Product_Quantity { get; set; }

        [Required]
        [DisplayName("Product Total_Price")]
        public decimal Product_Total_Price { get; set; }
        [Required]
        [DisplayName("Product Seling Price")]
        public decimal Product_Seling_Price { get; set; }

        [Required]
        [DisplayName("Category")]
        [ForeignKey("Category")]
        public int Cat_Id { get; set; }
        public Categories? Categories { get; set; }

        [Required]
        [DisplayName("Brand")]
        [ForeignKey("Brand")]
        public int Brand_Id { get; set; }
        public Brand? Brands { get; set; }
    }
}
