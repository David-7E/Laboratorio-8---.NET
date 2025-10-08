using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Models;

[Table("orders")]
[Index("ClientId", Name = "fk_orders_client")]
public partial class Order
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int OrderId { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClientId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Orders")]
    public virtual Client Client { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
