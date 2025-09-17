using Microsoft.AspNetCore.Mvc;
using Sugira.Services.Interfaces;

namespace Sugira.Controllers;

[Route("Menu")]
public class MenuController : Controller
{
    private readonly IMenuService _menuService;

    public MenuController(
        IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("get-all-menus")]
    public async Task<IActionResult> GetMenusAsync()
    {
        var menus = await _menuService.GetAllActiveMenusAsViewModelAsync();

        return Ok(menus);
    }
}
