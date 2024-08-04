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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerDto)
        {
            if (reviewerDto == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers()
                .Where(r => r.FirstName.Trim().ToUpper() == reviewerDto.FirstName.Trim().ToUpper())
                .FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewerMap = _mapper.Map<Reviewer>(reviewerDto);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Succssfully created.");
        }
    }
}

