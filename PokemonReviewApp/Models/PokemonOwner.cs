using System;
namespace PokemonReviewApp.Models
{
	public class PokemonOwner
	{
        public int PokemonId        { get; set; }
        public int OwnerId          { get; set; }
        public Pokemon Pokemon      { get; set; } = new Pokemon();
        public Owner Owner          { get; set; } = new Owner();
    }
}

