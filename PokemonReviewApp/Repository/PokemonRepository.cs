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

        public bool CreatePokemno(int ownerId, int categoryId, Pokemon pokemon)
        {
            var owner = _context.Owners
                .Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories
                .Where(c => c.Id == categoryId).FirstOrDefault();


            var pokemonOwner = new PokemonOwner()
            {
                Owner = owner ?? new Owner(),
                Pokemon = pokemon,
            };

            var pokemonCategory = new PokemonCategory()
            {
                Category = category ?? new Category(),
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);
            _context.Add(pokemonCategory);
            _context.Add(pokemon);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

