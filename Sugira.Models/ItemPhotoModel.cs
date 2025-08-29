using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sugira.Models;

public class ItemPhotoModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public ItemModel Item { get; set; } = null!;

    [Required]
    public byte[] Data { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string FileName { get; set; } = null!;

    [Required]
    public long FileSize { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
