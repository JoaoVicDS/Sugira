using Sugira.Models;
using Sugira.Models.ViewModels;

namespace Sugira.Services.Interfaces;

public interface IMenuService
{
    Task<List<MenuModel>> GetAllActiveMenusAsync();

    Task<List<MenuViewModel>> GetAllActiveMenusAsViewModelAsync();

    Task<object> GetMenuForApiAsync(int menuId, int? questionLimit);
}