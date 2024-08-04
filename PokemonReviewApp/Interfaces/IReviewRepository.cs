using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		Review? GetReview(int reviewId);
		ICollection<Review> GetReviewOfAPokemon(int pokeId);
		bool ReviewExists(int reviewId);
		bool PokemonExists(int pokeId);
		bool CreateReview(
			//int reviewerId, int pokeId,
			Review review);
		bool Save();
	}
}

