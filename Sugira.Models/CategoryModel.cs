using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sugira.Models;

public class CategoryModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MenuId { get; set; }

    [ForeignKey(nameof(MenuId))]
    public MenuModel Menu { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
}
