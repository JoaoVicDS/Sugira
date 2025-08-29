using System.ComponentModel.DataAnnotations;

namespace Sugira.Models;

public class MenuModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
}