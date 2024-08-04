using System;
using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository
    {
        DataContext _context;
       
		public ReviewRepository(DataContext context)
		{
            _context = context;   
		}

        public Review? GetReview(int reviewId)
            => _context.Reviews
            .Where(r => r.Id == reviewId)
            .FirstOrDefault();

        public ICollection<Review> GetReviewOfAPokemon(int pokeId)
            => _context.Reviews
            .Where(r => r.Pokemon.Id == pokeId)
            .ToList();

        public ICollection<Review> GetReviews()
            => _context.Reviews.ToList();

        public bool ReviewExists(int reviewId)
            => _context.Reviews.Any(r => r.Id == reviewId);

        public bool PokemonExists(int pokeId)
            => _context.Pokemons.Any(p => p.Id == pokeId);

        public bool CreateReview(
            //int reviewerId, int pokeId,
            Review review)
        {
            //var reviewer = _context.Reviewers
            //  .Where(o => o.Id == reviewerId).FirstOrDefault();

            //var pokemon = _context.Pokemons
            // .Where(o => o.Id == pokeId).FirstOrDefault();

            //_context.Add(reviewer ?? new Reviewer());
            //_context.Add(pokemon ?? new Pokemon());
            _context.Add(review);

            return Save();
        }

        public bool Save()
            => _context.SaveChanges() > 0;
    }
}

