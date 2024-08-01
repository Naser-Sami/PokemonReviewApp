﻿using System;
namespace PokemonReviewApp.Models
{
	public class Category
	{
        public int Id                                           { get; set; }
        public string Name                                      { get; set; } = string.Empty;
        public ICollection<PokemonCategory> PokemonCategories   { get; set; } = new List<PokemonCategory>();
    }
}