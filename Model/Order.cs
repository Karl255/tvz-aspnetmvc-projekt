using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcProjekt.Model;

public class Order
{
    [Key] public int Id { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? PostBr {  get; set; }
    public OrderStatus Status { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}

public enum OrderStatus
{
    Cart = 0,
    Ordered = 1,
    Shipped = 2,
}
