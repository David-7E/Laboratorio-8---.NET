using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Models;

[Table("products")]
public partial class Product
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [Precision(10, 2)]
    public decimal Price { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
