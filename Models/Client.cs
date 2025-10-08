using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB8_David_Belizario.Models;

[Table("clients")]
public partial class Client
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int ClientId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [InverseProperty("Client")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
