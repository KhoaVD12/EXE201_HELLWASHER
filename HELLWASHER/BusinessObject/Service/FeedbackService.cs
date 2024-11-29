using AutoMapper;
using BusinessObject.IService;
using BusinessObject.ViewModels.Feedback;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IBaseRepo<Feedback> _feedbackRepo;
        private readonly IBaseRepo<User> _userRepo;
        private readonly IBaseRepo<Order> _orderRepo;
        private readonly IMapper _mapper;

        public FeedbackService(IBaseRepo<Feedback> feedbackRepo, IMapper mapper, IBaseRepo<User> userRepo)
        {
            _feedbackRepo = feedbackRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public async Task<ServiceResponse<FeedbackDTO>> AddFeedback(FeedbackRequest feedbackDTO, int userId)
        {
            var response = new ServiceResponse<FeedbackDTO>();
            try
            {
                var feedback = _mapper.Map<Feedback>(feedbackDTO);

                var user = await _userRepo.GetByIdAsync(userId);
                if (user.UserId == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }
                if (feedbackDTO.ProductId == 0)
                {
                    feedback.ProductId = null;
                }
                if (feedbackDTO.WashServiceId == 0)
                {
                    feedback.WashServiceId = null;
                }
                feedback.UserId = userId;
                feedback.Date = DateTime.Now;
                await _feedbackRepo.AddAsync(feedback);
                response.Data = _mapper.Map<FeedbackDTO>(feedback);
                response.Success = true;
                response.Message = "Add feedback successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ServiceResponse<FeedbackDTO>> DeleteFeedback(int feedbackId)
        {
            var response = new ServiceResponse<FeedbackDTO>();
            try
            {
                var exist = await _feedbackRepo.GetByIdAsync(feedbackId);
                if (exist == null)
                {
                    response.Success = false;
                    response.Message = "Feedback not found";
                    return response;
                }
                await _feedbackRepo.DeleteAsync(exist.FeedbackId);
                response.Success = true;
                response.Message = "Delete feedback successfully";
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
            public async Task<ServiceResponse<IEnumerable<FeedbackDTO>>> GetAllFeedback()
        {
            var response = new ServiceResponse<IEnumerable<FeedbackDTO>>();
            try
            {
                var feedback = await _feedbackRepo.GetAllAsync();
                var feedbacks = _mapper.Map<IEnumerable<FeedbackDTO>>(feedback);
                response.Data = feedbacks;
                response.Success = true;
                response.Message = "Get all feedback successfully";
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ServiceResponse<FeedbackDTO>> GetFeedbackById(int feedbackId)
        {
            var response = new ServiceResponse<FeedbackDTO>();
            try
            {
                var exist = await _feedbackRepo.GetByIdAsync(feedbackId);
                if (exist == null)
                {
                    response.Success = false;
                    response.Message = "Feedback not found";
                    return response;
                }
                var feedbacks = _mapper.Map<FeedbackDTO>(exist);
                response.Data = feedbacks;
                response.Success = true;
                response.Message = "Get feedback by id successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
        public async Task<ServiceResponse<FeedbackRequest>> UpdateFeedback(FeedbackRequest feedbackRequest, int feedbackId)
        {
            var response = new ServiceResponse<FeedbackRequest>();
            try
            {
                var exist = await _feedbackRepo.GetByIdAsync(feedbackId);
                if (exist == null)
                {
                    response.Success = false;
                    response.Message = "Feedback not found";
                    return response;
                }

                // Update the properties of the existing entity
                _mapper.Map(feedbackRequest, exist);

                await _feedbackRepo.UpdateAsync(exist);
                response.Data = feedbackRequest;
                response.Success = true;
                response.Message = "Update feedback successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
