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
    public class ReviewerController : Controller
	{

        IReviewerRepository _reviewerRepository;
        IMapper _mapper;
        public ReviewerController(
            IReviewerRepository reviewerRepository, IMapper mapper)
		{
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviewers()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<List<ReviewerDto>>
                (_reviewerRepository.GetReviewers()));
        }

        [HttpGet("GetReviewer/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewersExists(reviewerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<ReviewerDto>
                (_reviewerRepository.GetReviewer(reviewerId)));
        }

        [HttpGet("GetReviewsByReviewer/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewersExists(reviewerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<List<ReviewDto>>
                (_reviewerRepository
                .GetReviewsByReviewer(reviewerId)));
        }
    }
}

