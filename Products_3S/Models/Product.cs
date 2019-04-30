using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_3S.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Product Name must be alphabetical only !")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Product Name must be at least 3 alphabet !")]
        public string ProductName { get; set; }

        [Required]
        public int? ReorderLevel { get; set; }

        [Required]
        [RegularExpression(@"^\d{0,5}(\.\d{1,3})?$")]
        [Column("UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Column("UnitInStock")]
        public int? UnitInStock { get; set; }

        [Required]
        [Column("UnitOnOrder")]
        public int? UnitOnOrder { get; set; }

        [ForeignKey("Unit")]
        [Required]
        public int? QuantityPerUnit { get; set; }

        [ForeignKey("Supplier")]
        [Required]
        public int? SupplierID { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Unit Unit { get; set; }
    }
}