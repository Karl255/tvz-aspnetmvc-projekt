using System.ComponentModel.DataAnnotations;

namespace AspNetMvcProjekt.Model;

public class Category
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Item> Items { get; set; }
}
