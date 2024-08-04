using System;
namespace PokemonReviewApp.Models
{
	public class Review
	{
        public         int      Id          { get; set; }
        public         string   Title       { get; set; } = string.Empty;
        public         string   Text        { get; set; } = string.Empty;
        public         int      Rating      { get; set; }
        public virtual Reviewer Reviewer    { get; set; } = new Reviewer();
        public virtual Pokemon  Pokemon     { get; set; } = new Pokemon();
    }
}