using System;
using System.Collections.Generic;
using StarWars.Characters;
using StarWars.Reviews;

namespace StarWars.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Dictionary<Episode, List<Review>> _data = new();

        public void AddReview(Episode episode, Review review)
        {
            if (!_data.TryGetValue(episode, out var reviews))
            {
                reviews = new List<Review>();
                _data[episode] = reviews;
            }

            reviews.Add(review);
        }

        public IEnumerable<Review> GetReviews(Episode episode)
        {
            if (_data.TryGetValue(episode, out var reviews))
            {
                return reviews;
            }

            return Array.Empty<Review>();
        }
    }
}
