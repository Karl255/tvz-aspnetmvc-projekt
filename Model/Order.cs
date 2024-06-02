using System.ComponentModel.DataAnnotations;

namespace AspNetMvcProjekt.Model;

public class Order
{
    [Key] public int Id { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostBr {  get; set; }

    public virtual ICollection<Item> Items { get; set; }
}
