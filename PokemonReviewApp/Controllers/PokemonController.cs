﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

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
	}
}

