namespace Sugira.Models.ViewModels;

/// <summary>
/// Representa uma categoria do menu (ex: "Entradas") contendo uma lista de itens.
/// </summary>
public class CategoryViewModel
{
    /// <summary>
    /// O ID da categoria.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// O nome da categoria. Ex: "Pratos Principais - Da Terra".
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// A lista de itens que pertencem a esta categoria.
    /// </summary>
    public List<ItemViewModel> Items { get; set; } = new();
}