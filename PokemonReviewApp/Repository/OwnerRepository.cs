using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
		{
            this._context = context;
        }

        public Owner? GetOwner(int ownerId)
            => _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();

        public ICollection<Owner> GetOwnerOfAPokemno(int pokeId)
            => _context.PokemonOwners
            .Where(p => p.PokemonId == pokeId)
            .Select(o => o.Owner)
            .ToList();

        public ICollection<Owner> GetOwners()
            => _context.Owners
            .OrderBy(o => o.Id)
            .ToList();

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
            => _context.PokemonOwners
            .Where(o => o.OwnerId == ownerId)
            .Select(p => p.Pokemon)
            .ToList();

        public bool OwnerExists(int ownerId)
            => _context.Owners
            .Any(o => o.Id == ownerId);

        public bool PokemonExists(int pokeId)
            => _context.Pokemons
            .Any(p => p.Id == pokeId);

        public bool OwnerCreate(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}

