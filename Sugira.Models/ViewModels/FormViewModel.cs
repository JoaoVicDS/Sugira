namespace Sugira.Models.ViewModels.APIEbeer;

public class FormViewModel
{
    public string FormId { get; set; }
    public List<FormCategoryViewModel> Categories { get; set; }
}

public class FormCategoryViewModel
{
    public string Name { get; set; }
    public List<FormQuestionViewModel> Questions { get; set; }
}

public class FormQuestionViewModel
{
    public string Characteristic { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }
}