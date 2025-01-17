﻿using System;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base (options)
		{
		}


		public DbSet<Pokemon>           Pokemons            { get; set; }
		public DbSet<Owner>             Owners              { get; set; }
        public DbSet<Country>           Countries           { get; set; }
        public DbSet<Category>          Categories          { get; set; }
        public DbSet<Review>            Reviews             { get; set; }
        public DbSet<Reviewer>          Reviewers           { get; set; }
        public DbSet<PokemonOwner>      PokemonOwners       { get; set; }
        public DbSet<PokemonCategory>   PokemonCategories   { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonOwner>()
                .HasKey(po => new { po.PokemonId, po.OwnerId });                // Link ID's togother
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(o => o.Owner)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(o => o.OwnerId);


            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId });             // Link ID's togother
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(p => p.PokemonId);
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(c => c.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}

