using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
	{

		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

		public CategoryController(
			ICategoryRepository categoryRepository, IMapper mapper)
		{
			this._categoryRepository = categoryRepository;
			this._mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCategories()
		{
			var categories = _mapper.Map<List<CategoryDto>>
				(_categoryRepository.GetCategories());

			if (!ModelState.IsValid)
				return BadRequest();
			return Ok(categories);
		}

		[HttpGet("{categoryId}")]
		public IActionResult GetCategory(int categoryId)
		{
			if (!_categoryRepository.CategoryExists(categoryId))
				return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

			var category = _mapper.Map<CategoryDto>
				(_categoryRepository.GetCategory(categoryId));
			return Ok(category);
        }

		[HttpGet("GetPokemonByCategoryId/{categoryId}")]
        public IActionResult GetPokemonByCategory(int categoryId)
		{
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

			var pokemonsByCategory = _mapper.Map<List<PokemonDto>>
				(_categoryRepository.GetPokemonByCategory(categoryId));

			return Ok(pokemonsByCategory);
        }
    }
}

