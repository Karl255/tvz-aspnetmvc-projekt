using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcProjekt.Model;

public class Item
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int AmountInStorage { get; set; }
    [Precision(6, 2)]
    public decimal Price { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
