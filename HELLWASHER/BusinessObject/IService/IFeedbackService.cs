using BusinessObject.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IFeedbackService
    {
        Task<ServiceResponse<IEnumerable<FeedbackDTO>>> GetAllFeedback();
        Task<ServiceResponse<FeedbackDTO>> GetFeedbackById(int feedbackId);
        Task<ServiceResponse<FeedbackDTO>> AddFeedback(FeedbackRequest feedbackDTO, int userId);
        Task<ServiceResponse<FeedbackRequest>> UpdateFeedback(FeedbackRequest feedbackRequest, int feedbackId);
        Task<ServiceResponse<FeedbackDTO>> DeleteFeedback(int feedbackId);
        //Task<ServiceResponse<FeedbackDTO>> UpdateFeedbackImage(int feedbackId, IFormFile image);
    }
}
