using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWash.BL.DTOs;
using CarWash.DAL.Repositories;
using CarWash.Domain.Models;

namespace CarWash.BL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IWashRequestRepository _washRequestRepository;

        public ReviewService(IReviewRepository reviewRepository, IWashRequestRepository washRequestRepository)
        {
            _reviewRepository = reviewRepository;
            _washRequestRepository = washRequestRepository;
        }

        public async Task SubmitReviewAsync(CreateReviewDto dto)
        {
            var washRequest = await _washRequestRepository.GetByIdAsync(dto.RequestId);

            // Check if the wash request is completed
            if (washRequest == null || washRequest.Status != WashStatus.Completed)
            {
                throw new InvalidOperationException("Review can only be submitted for completed wash requests.");
            }

            var review = new Review
            {
                RequestId = dto.RequestId,
                Rating = dto.Rating,
                Comments = dto.Comments,
                ReviewDate = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
        }

        public async Task<List<ReviewDto>> GetReviewsForWasherAsync(Guid washerId)
        {
            var reviews = await _reviewRepository.GetByWasherIdAsync(washerId);

            return reviews.Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                RequestId = r.RequestId,
                Rating = r.Rating,
                Comments = r.Comments,
                ReviewDate = r.ReviewDate
            }).ToList();
        }
    }
}