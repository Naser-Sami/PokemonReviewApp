using System;
using System.Xml.Linq;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class PokemonRepository : IPokemonRepository
	{
		private readonly DataContext _context;

		public PokemonRepository(DataContext context)
		{
			this._context = context;
		}

        public Pokemon? GetPokemon(int id)
            => _context.Pokemons.Where(p => p.Id == id)
            .FirstOrDefault();

        public Pokemon? GetPokemon(string name)
            => _context.Pokemons.Where(p => p.Name == name)
            .FirstOrDefault();

        public decimal GetPokemonRating(int id)
        {
            var review = _context.Reviews
                .Where(p => p.Pokemon.Id == id);
            if (review.Count() <= 0)
                return 0;

            return ((decimal)review
                .Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
			return _context.Pokemons
				.OrderBy(p => p.Id)
				.ToList();
        }

        public bool PokemonExist(int id)
            => _context.Pokemons.Any(p => p.Id == id);

        public bool PokemonExist(string name)
            => _context.Pokemons.Any(p => p.Name == name);
    }
}

