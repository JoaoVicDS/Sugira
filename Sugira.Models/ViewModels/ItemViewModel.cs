namespace Sugira.Models.ViewModels;

/// <summary>
/// Representa um único item do menu (prato, bebida, etc.) formatado para exibição.
/// </summary>
public class ItemViewModel
{
    /// <summary>
    /// O ID do item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// O nome do item. Ex: "Filé Mignon ao Molho de Cogumelos Trufados".
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// A lista de características definidas para o item.
    /// </summary>
    public List<CharacteristicViewModel> Characteristics { get; set; } = new();
}