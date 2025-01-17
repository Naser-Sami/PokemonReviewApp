﻿using System;
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
	public class OwnerController : Controller
	{
		private readonly IOwnerRepository _ownerRepository;
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public OwnerController(
			IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
		{
			_ownerRepository = ownerRepository;
			_countryRepository = countryRepository;
            _mapper = mapper;
		}

		[HttpGet()]
		public IActionResult GetOwners()
		{
			if (!ModelState.IsValid) return BadRequest();
			return Ok(_mapper.Map<List<OwnerDto>>
                (_ownerRepository.GetOwners()));
        }

		[HttpGet("GetOwner/{ownerId}")]
		public IActionResult GetOwner(int ownerId)
		{
			if (!_ownerRepository.OwnerExists(ownerId))
				return NotFound();
			if (!ModelState.IsValid)
				return BadRequest();
			
			return Ok(_mapper.Map<OwnerDto>
				(_ownerRepository.GetOwner(ownerId)));
		}

		[HttpGet("GetOwnerOfAPokemno/{pokeId}")]
		public IActionResult GetOwnerOfAPokemno(int pokeId)
		{
            if (!_ownerRepository.PokemonExists(pokeId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<List<OwnerDto>>
				(_ownerRepository.GetOwnerOfAPokemno(pokeId)));
        }

		[HttpGet("GetPokemonByOwner/{ownerId}")]
		public IActionResult GetPokemonByOwner(int ownerId)
		{
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

			return Ok(_mapper.Map<List<PokemonDto>>
				(_ownerRepository.GetPokemonByOwner(ownerId)));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
		public IActionResult OwnerCreate([FromForm] int countryId, [FromBody] OwnerDto ownerCreate)
		{
			if (ownerCreate == null)
				return BadRequest(ModelState);

			var owner = _ownerRepository.GetOwners()
				.Where(
				o =>
					o.FirstName.Trim().ToLower() == ownerCreate.FirstName.Trim().ToLower() &&
					o.LastName.Trim().ToLower() == ownerCreate.LastName.Trim().ToLower()
				).FirstOrDefault();

			if (owner != null)
			{
                ModelState.AddModelError("", "Owner already exists.");
                return StatusCode(422, ModelState);
            }

			if (!ModelState.IsValid)
				return BadRequest();

			var ownerMap = _mapper.Map<Owner>(ownerCreate);

			// what is the country of the owner else it will add empty country or throw an error
			if (_countryRepository.GetCountry(countryId) != null)
			{
                ownerMap.Country = _countryRepository.GetCountry(countryId)!;
            }

			if (!_ownerRepository.OwnerCreate(ownerMap))
			{
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }
    }
}

