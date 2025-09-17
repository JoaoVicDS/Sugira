namespace Sugira.Models.ViewModels.APIEbeer;

public class AnswersViewModel
{
    public string FormId { get; set; }
    public List<AnswersCategoryViewModel> Categories { get; set; }
}

public class AnswersCategoryViewModel
{
    public string Name { get; set; }
    public List<SelectedAnswerViewModel> SelectedAnswers { get; set; }
}

public class SelectedAnswerViewModel
{
    public string CharacteristicAsked { get; set; }
    public string SelectedOption { get; set; }
}