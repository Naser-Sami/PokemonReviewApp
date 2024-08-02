using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : Controller
	{
		private readonly IOwnerRepository _ownerRepository;
		private readonly IMapper _mapper;

		public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
		{
			_ownerRepository = ownerRepository;
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
    }
}

