using System;
namespace PokemonReviewApp.Models
{
	public class PokemonCategory
	{
		public int PokemonId		{ get; set; }
		public int CategoryId		{ get; set; }
		public Pokemon Pokemon		{ get; set; } = new Pokemon();
        public Category Category	{ get; set; } = new Category();
    }
}

