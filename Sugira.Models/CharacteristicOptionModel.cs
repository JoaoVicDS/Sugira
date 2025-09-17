using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sugira.Models;

public class CharacteristicOptionModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CharacteristicTypeId { get; set; }

    [ForeignKey(nameof(CharacteristicTypeId))]
    public CharacteristicTypeModel CharacteristicType { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Value { get; set; } = null!; // ex: Leve, Moderado, Intenso

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
