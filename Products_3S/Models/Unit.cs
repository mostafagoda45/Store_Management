using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_3S.Models
{
    [Table("Unit")]
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("UnitName")]
        public string UnitName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}