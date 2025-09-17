namespace Sugira.Models.ViewModels;

/// <summary>
/// ViewModel principal que representa o menu completo a ser exibido no frontend.
/// </summary>
public class MenuViewModel
{
    /// <summary>
    /// O ID do menu.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// O nome do menu. Ex: "Menu Degustação Sazonal".
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// A lista de categorias que compõem o menu.
    /// </summary>
    public List<CategoryViewModel> Categories { get; set; } = new();
}