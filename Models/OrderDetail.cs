using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LAB8_David_Belizario.Data;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Models;

[Table("orderdetails")]
[Index("OrderId", Name = "fk_orderdetails_order")]
[Index("ProductId", Name = "fk_orderdetails_product")]
public partial class Orderdetail
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int OrderDetailId { get; set; }

    [Column(TypeName = "int(11)")]
    public int OrderId { get; set; }

    [Column(TypeName = "int(11)")]
    public int ProductId { get; set; }

    [Column(TypeName = "int(11)")]
    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Orderdetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Orderdetails")]
    public virtual Product Product { get; set; } = null!;
}
