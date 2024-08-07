using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Mappers;

public class FeedbackMapper
{
    
    public static FeedbackViewModel MapToViewModel(Feedback feedback)
    {
        return new FeedbackViewModel
        {
            FeedbackId = feedback.FeedbackId,
            UserFullName = feedback.Name,
            Message = feedback.Comment,
            Email = feedback.Email,
            Date = feedback.Date,
        };
    }

    public static Feedback MapToModel(FeedbackViewModel viewModel)
    {
        return new Feedback
        {

            FeedbackId = viewModel.FeedbackId,
            Name = viewModel.UserFullName,
            Comment = viewModel.Message,
            Email = viewModel.Email,
            Date = viewModel.Date,
        };
    }
}