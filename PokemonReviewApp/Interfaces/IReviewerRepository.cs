using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewer> GetReviewers();
		Reviewer? GetReviewer(int reviewerId);
		ICollection<Reviewer> GetReviewsByReviewer(int reviewerId);
        bool ReviewersExists(int reviewerId);
		bool CreateReviewer(Reviewer reviewer);
		bool Save();
    }
}

