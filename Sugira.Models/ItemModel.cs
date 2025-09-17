using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sugira.Models;

public class ItemModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public CategoryModel Category { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    [Required] 
    [MaxLength(300)] 
    public string Description { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ItemCharacteristicModel> Characteristics { get; set; } = new List<ItemCharacteristicModel>();
    public ICollection<ItemPhotoModel> Photos { get; set; } = new List<ItemPhotoModel>();
}

