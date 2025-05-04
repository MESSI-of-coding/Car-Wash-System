using System.Collections.Generic;
using System.Threading.Tasks;
using CarWash.BL.DTOs;

namespace CarWash.BL.Services
{
    public interface IReviewService
    {
        Task SubmitReviewAsync(CreateReviewDto dto);
        Task<List<ReviewDto>> GetReviewsForWasherAsync(int washerId);
    }
}