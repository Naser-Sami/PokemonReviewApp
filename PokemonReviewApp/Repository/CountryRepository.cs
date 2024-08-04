using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
	public class CountryRepository : ICountryRepository
    {

        private readonly DataContext _context;

		public CountryRepository(DataContext context)
		{
            this._context = context;
		}

        public bool CountryExists(int id)
            => _context.Countries
            .Any(c => c.Id == id);

        public bool OwnerExists(int id)
            => _context.Owners
            .Any(o => o.Id == id);

        public ICollection<Country> GetCountries()
            => _context.Countries
            .OrderBy(c => c.Name)
            .ToList();

        public Country? GetCountry(int id)
            => _context.Countries
            .Where(c => c.Id == id)
            .FirstOrDefault();

        public Country? GetCountryByOwner(int ownerId)
            => _context.Owners
            .Where(o => o.Id == ownerId)
            .Select(c => c.Country)
            .FirstOrDefault();

        public ICollection<Owner> GetOwnerFromCountry(int countryId)
            => _context.Owners
            .Where(o => o.Country.Id == countryId)
            .ToList();

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

