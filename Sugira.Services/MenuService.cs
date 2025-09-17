using Microsoft.EntityFrameworkCore;
using Sugira.Data;
using Sugira.Models;
using Sugira.Models.ViewModels;
using Sugira.Services.Interfaces;

namespace Sugira.Services;

public class MenuService : IMenuService
{
    private readonly ApplicationDbContext _databaseContext;

    public MenuService(ApplicationDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<MenuModel>> GetAllActiveMenusAsync()
    {
        return await _databaseContext.Menu
            .AsNoTracking()
            .Where(m => m.IsActive == true)
            .ToListAsync();
    }

    /// <summary>
    /// Busca todos os menus ativos e os transforma em ViewModels para o frontend.
    /// </summary>
    /// <returns>Uma lista de MenuViewModel pronta para ser serializada em JSON.</returns>
    public async Task<List<MenuViewModel>> GetAllActiveMenusAsViewModelAsync()
    {
        // A projeção (.Select) é feita ANTES do ToListAsync().
        // Isso instrui o EF Core a construir as ViewModels diretamente a partir da consulta SQL.
        return await _databaseContext.Menu
            .AsNoTracking()
            .Where(menu => menu.IsActive)
            .AsSplitQuery() // Força a divisão da consulta mesmo em projeções .Select()
            .Select(menu => new MenuViewModel // Início da projeção para a ViewModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Categories = menu.Categories.Select(category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Items = category.Items.Select(item => new ItemViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Characteristics = item.Characteristics.Select(c => new CharacteristicViewModel
                        {
                            // A projeção seleciona apenas os campos necessários, evitando erros de referência nula.
                            TypeName = c.CharacteristicType.Name,
                            Option = c.CharacteristicOption.Value
                        }).ToList()
                    }).ToList()
                }).ToList()
            })
            .ToListAsync(); // O EF Core executa a consulta otimizada e materializa a lista de ViewModels.
    }

    public async Task<object> GetMenuForApiAsync(int menuId, int? questionLimit)
    {
        var menu = await _databaseContext.Menu
            .AsNoTracking()
            .AsSplitQuery()
            .Where(m => m.Id == menuId && m.IsActive)
            .Select(m => new // Usando um objeto anônimo que corresponde à estrutura JSON da APIEbeer
            {
                restaurant = m.Name,
                questionLimitPerCategory = questionLimit,
                menu = new
                {
                    categories = m.Categories.Select(c => new
                    {
                        name = c.Name,
                        items = c.Items.Select(i => new
                        {
                            name = i.Name,
                            characteristics = i.Characteristics.Select(ch => new
                            {
                                name = ch.CharacteristicType.Name,
                                value = ch.CharacteristicOption.Value,
                                // Buscamos todas as opções possíveis para esta característica
                                options = _databaseContext.CharacteristicOption
                                    .Where(o => o.CharacteristicTypeId == ch.CharacteristicTypeId)
                                    .Select(o => o.Value)
                                    .Distinct()
                                    .ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }
            })
            .FirstOrDefaultAsync();

        return menu;
    }
}