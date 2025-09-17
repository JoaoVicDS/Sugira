namespace Sugira.Models.ViewModels.APIEbeer;

public class RecommendationViewModel
{
    public List<RecommendationCategoryViewModel> Categories { get; set; }
}

public class RecommendationCategoryViewModel
{
    public string Name { get; set; }
    public List<RecommendationItemViewModel> Items { get; set; }
}

public class RecommendationItemViewModel
{
    public string Recommendation { get; set; }
}