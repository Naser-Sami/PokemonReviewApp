using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ReviewController : Controller
	{
        IReviewRepository _reviewRepository;
        IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
		{
            _reviewRepository = reviewRepository;
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
    }
}

