using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

		public CategoryRepository(DataContext context)
		{
            this._context = context;
		}

        public bool CategoryExists(int id)
            => _context.Categories.Any(c => c.Id == id);

        public ICollection<Category> GetCategories()
            => _context.Categories.OrderBy(c => c.Name).ToList();

        public Category? GetCategory(int id)
            => _context.Categories.Where(c => c.Id == id).FirstOrDefault();

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
            => _context.PokemonCategories
            .Where(e => e.CategoryId == categoryId)
            .Select(c => c.Pokemon)
            .ToList();

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

