using System;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class ReviewerRepository : IReviewerRepository
    {
        DataContext _context;
        public ReviewerRepository(DataContext context)
		{
            _context = context;
		}

        public Reviewer? GetReviewer(int reviewerId)
            => _context.Reviewers
            .Where(r => r.Id == reviewerId)
            .Include(e => e.Reviews)
            .FirstOrDefault();

        public ICollection<Reviewer> GetReviewers()
            => _context.Reviewers.ToList();

        public ICollection<Reviewer> GetReviewsByReviewer(int reviewerId)
            => _context.Reviewers
            .Where(r => r.Id == reviewerId)
            .ToList();

        public bool ReviewersExists(int reviewerId)
            => _context.Reviewers.Any(r => r.Id == reviewerId);
    }
}

