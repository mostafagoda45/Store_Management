using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_3S.Models
{
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Product Name must be alphabetical only !")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Product Name must be at least 3 alphabet !")]
        [Column("SupplierName")]
        public string SupplierName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}