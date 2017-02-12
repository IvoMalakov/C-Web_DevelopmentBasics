namespace SharpStore.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Sender { get; set; }

        public string Subject { get; set; }

        public string MessageText { get; set; }
    }
}
