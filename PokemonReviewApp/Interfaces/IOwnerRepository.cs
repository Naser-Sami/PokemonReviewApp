using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<Owner> GetOwners();
		Owner? GetOwner(int ownerId);
		ICollection<Owner> GetOwnerOfAPokemno(int pokeId);
		ICollection<Pokemon> GetPokemonByOwner(int ownerId);
		bool OwnerExists(int ownerId);
		bool PokemonExists(int pokeId);
    }
}

