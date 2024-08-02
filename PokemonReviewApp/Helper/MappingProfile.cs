using System;
using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Pokemon, PokemonDto>();
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto, Category>();
			CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner, OwnerDto>();
			CreateMap<Review, ReviewDto>();
			CreateMap<Reviewer, ReviewerDto>();
		}
	}
}

