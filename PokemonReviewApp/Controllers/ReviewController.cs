using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ReviewController : Controller
	{
        IReviewRepository _reviewRepository;
        IPokemonRepository _pokemonRepository;
        IReviewerRepository _reviewerRepository;

        IMapper _mapper;
        public ReviewController(
            IReviewRepository reviewRepository, IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository, IMapper mapper)
		{
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<List<ReviewDto>>
                (_reviewRepository.GetReviews()));
        }

        [HttpGet("GetReview/{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<ReviewDto>
                (_reviewRepository.GetReview(reviewId)));
        }

        [HttpGet("GetReviewOfAPokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewOfAPokemon(int pokeId)
        {
            if (!_reviewRepository.PokemonExists(pokeId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<List<ReviewDto>>
                (_reviewRepository.GetReviewOfAPokemon(pokeId)));
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewDto.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Pokemon already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(reviewDto);

            if (_pokemonRepository.GetPokemon(pokeId) != null)
            {
                reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokeId)!;
            }

            if (_reviewerRepository.GetReviewer(reviewerId) != null)
            {
                reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId)!;
            }
            
            if (!_reviewRepository.CreateReview(
                //reviewerId, pokeId,
                reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }
    }
}

