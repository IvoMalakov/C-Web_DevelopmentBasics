namespace SharpStore.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Knife
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
