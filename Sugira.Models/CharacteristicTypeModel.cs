using System.ComponentModel.DataAnnotations;

namespace Sugira.Models;

public class CharacteristicTypeModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!; // ex: Bitter, AlcoholContent

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<CharacteristicOptionModel> Options { get; set; } = new List<CharacteristicOptionModel>();
}
