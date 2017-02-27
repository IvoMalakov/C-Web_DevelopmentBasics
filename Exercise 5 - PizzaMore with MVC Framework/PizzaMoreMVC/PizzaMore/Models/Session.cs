namespace PizzaMore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Session
    {
        [Key]
        public string Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public override string ToString()
        {
            return $"{this.Id}\t{this.User.Id}";
        }
    }
}
