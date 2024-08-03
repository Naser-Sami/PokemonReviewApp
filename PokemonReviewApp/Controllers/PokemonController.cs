using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : Controller
	{
		private readonly IPokemonRepository _pokemonRepository;
		private readonly IMapper _mapper;

		public PokemonController(
			IPokemonRepository pokemonRepository, IMapper mapper)
		{
			this._pokemonRepository = pokemonRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
		public IActionResult GetPokemons()
		{
			var pokemons = _mapper.Map<List<PokemonDto>>
				(_pokemonRepository.GetPokemons());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(pokemons);
		}

		[HttpGet("{pokeId}")]
		[ProducesResponseType(200, Type = typeof(Pokemon))]
		[ProducesResponseType(400)]
		public IActionResult GetPokemon(int pokeId)
		{
			// if not found
			if (!_pokemonRepository.PokemonExist(pokeId))
				return NotFound();

			var pokemon = _mapper.Map<PokemonDto>
				(_pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			return Ok(pokemon);
        }
        
		[HttpGet("Name={pokeName}", Name = "GetPokemonByName")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
		public IActionResult GetPokemon(string pokeName)
		{
			if (!_pokemonRepository.PokemonExist(pokeName))
				return NotFound();

            var pokemon = _mapper.Map<PokemonDto>
				(_pokemonRepository.GetPokemon(pokeName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
		[ProducesResponseType(200, Type = typeof(decimal))]
		[ProducesResponseType(400)]
		public IActionResult GetPokemonRating(int pokeId)
		{
			if (!_pokemonRepository.PokemonExist(pokeId))
				return NotFound();

			var pokemon = _pokemonRepository.GetPokemonRating(pokeId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(pokemon);
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(500)]
		public IActionResult CreatePokemon(
			[FromQuery]	int ownerId,
            [FromQuery] int categoryId,
			[FromBody]	PokemonDto pokemonDto)
		{
			if (pokemonDto == null)
				return BadRequest(ModelState);

			var pokemons = _pokemonRepository.GetPokemons()
				.Where(p => p.Name.Trim().ToUpper() == pokemonDto.Name.Trim().ToUpper())
				.FirstOrDefault();

			if (pokemons != null)
			{
				ModelState.AddModelError("", "Pokemon already exists.");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest();

			var pokeMap = _mapper.Map<Pokemon>(pokemonDto);

            if (!_pokemonRepository.CreatePokemno(ownerId, categoryId, pokeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }
	}
}

