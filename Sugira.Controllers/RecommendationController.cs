using Microsoft.AspNetCore.Mvc;
using Sugira.Models.ViewModels.APIEbeer;
using Sugira.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Sugira.Controllers
{
    public class StartRecommendationRequest
    {
        public int MenuId { get; set; }
        public int? QuestionLimit { get; set; }
    }

    [Route("recommendation")]
    public class RecommendationController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IHttpClientFactory _httpClientFactory;

        // URL base da sua APIEbeer. Coloque em appsettings.json no futuro!
        private const string ApiEbeerBaseUrl = "https://localhost:7041"; // ATENÇÃO: Altere a porta se necessário

        public RecommendationController(IMenuService menuService, IHttpClientFactory httpClientFactory)
        {
            _menuService = menuService;
            _httpClientFactory = httpClientFactory;
        }

        // Ação para popular o <select> de menus no modal
        [HttpGet("menus")]
        public async Task<IActionResult> GetMenusForSelection()
        {
            var menus = await _menuService.GetAllActiveMenusAsync();
            return Ok(menus.Select(m => new { m.Id, m.Name }));
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartRecommendation([FromBody] StartRecommendationRequest request)
        {
            // 1. Pega os dados do menu no formato que a APIEbeer espera
            var menuPayload = await _menuService.GetMenuForApiAsync(request.MenuId, request.QuestionLimit);
            if (menuPayload == null)
            {
                return NotFound($"Menu com ID {request.MenuId} não encontrado.");
            }

            // 2. Envia o menu para a APIEbeer criar o formulário
            var client = _httpClientFactory.CreateClient();
            var jsonPayload = JsonSerializer.Serialize(menuPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{ApiEbeerBaseUrl}/api/form/create", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Erro ao criar o formulário na APIEbeer: {errorContent}");
            }

            // 3. Retorna o formulário recebido da APIEbeer para o frontend
            var formJson = await response.Content.ReadAsStringAsync();
            var formViewModel = JsonSerializer.Deserialize<FormViewModel>(formJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(formViewModel);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAnswers([FromBody] AnswersViewModel answers)
        {
            // 1. Envia as respostas para a APIEbeer
            var client = _httpClientFactory.CreateClient();
            var jsonPayload = JsonSerializer.Serialize(answers);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{ApiEbeerBaseUrl}/api/recommendation/create", content);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Erro ao obter a recomendação da APIEbeer.");
            }

            // 2. Retorna a recomendação final para o frontend
            var recommendationJson = await response.Content.ReadAsStringAsync();
            var recommendationViewModel = JsonSerializer.Deserialize<RecommendationViewModel>(recommendationJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(recommendationViewModel);
        }
    }
}