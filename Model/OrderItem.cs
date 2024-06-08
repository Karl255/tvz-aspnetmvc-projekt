using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcProjekt.Model;

[PrimaryKey(nameof(ItemId), nameof(OrderId))]
public class OrderItem
{
    [Key]
    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }
    public virtual Item Item { get; set; } = null!;

    [Key]
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;

    public int Amount { get; set; }
}
