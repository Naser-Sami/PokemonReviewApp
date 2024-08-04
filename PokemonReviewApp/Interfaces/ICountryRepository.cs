using System;
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
		bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}

