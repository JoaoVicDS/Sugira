using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sugira.Models;

public class ItemCharacteristicModel
{
    public int ItemId { get; set; }

    public int CharacteristicTypeId { get; set; }

    public int CharacteristicOptionId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(ItemId))]
    public ItemModel Item { get; set; } = null!;

    [ForeignKey(nameof(CharacteristicTypeId))]
    public CharacteristicTypeModel CharacteristicType { get; set; } = null!;

    [ForeignKey(nameof(CharacteristicOptionId))]
    public CharacteristicOptionModel CharacteristicOption { get; set; } = null!;
}
