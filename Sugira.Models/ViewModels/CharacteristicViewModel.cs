namespace Sugira.Models.ViewModels;

/// <summary>
/// Representa uma única característica específica de um item do menu.
/// </summary>
public class CharacteristicViewModel
{
    /// <summary>
    /// O nome do tipo da característica. Ex: "Ponto", "Sabor", "Teor Alcoólico".
    /// </summary>
    public required string TypeName { get; set; }

    /// <summary>
    /// A opção específica que foi definida para esta característica. Ex: "Ao ponto".
    /// </summary>
    public required string Option { get; set; }
}