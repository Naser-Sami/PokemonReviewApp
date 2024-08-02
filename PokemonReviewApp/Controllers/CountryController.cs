using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public CountryController(
			ICountryRepository countryRepository, IMapper mapper)
		{
			this._countryRepository = countryRepository;
			this._mapper = mapper;
		}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public IActionResult GetCountries()
		{
			var countries = _mapper.Map<List<CountryDto>>
				(_countryRepository.GetCountries());
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(countries);
		}

		[HttpGet("Country/{id}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public IActionResult GetCountry(int id)
		{
			if (!_countryRepository.CountryExists(id))
				return NotFound();
			var country = _mapper.Map<CountryDto>
				(_countryRepository.GetCountry(id));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(country);
		}

		[HttpGet("GetCountryByOwnerId/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByOwner(int ownerId)
		{
            if (!_countryRepository.OwnerExists(ownerId))
                return NotFound();
			var countryByOnwerId = _mapper.Map<CountryDto>
				(_countryRepository
				.GetCountryByOwner(ownerId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(countryByOnwerId);
        }

		[HttpGet("GetOwnerFromCountryId/{countryId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
		[ProducesResponseType(400)]
		public IActionResult GetOwnerFromCountry(int countryId)
		{
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();
			var owner = _mapper.Map<List<OwnerDto>>(_countryRepository
				.GetOwnerFromCountry(countryId));
			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
		public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
		{
			if (countryCreate == null)
				return BadRequest(ModelState);

			var country = _countryRepository.GetCountries()
				.Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
				.FirstOrDefault();

			if (country != null)
			{
				ModelState.AddModelError("", "Country already exists.");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest();

			var countryMap = _mapper.Map<Country>(countryCreate);

			if (!_countryRepository.CreateCCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return StatusCode(500, ModelState);
			}

			return Ok("Succssfully created.");
		}
    }
}

