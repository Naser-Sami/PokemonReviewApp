﻿using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		ICollection<Country> GetCountries();
		Country? GetCountry(int id);
		Country? GetCountryByOwner(int ownerId);
		ICollection<Owner> GetOwnerFromCountry(int countryId);
		bool CountryExists(int id);
		bool OwnerExists(int id);
	}
}
