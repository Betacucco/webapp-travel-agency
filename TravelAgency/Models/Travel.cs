using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo e' obbligatorio")]
        [StringLength(50, ErrorMessage = "Il titolo non puo' avere piu' di 50 caratteri")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Il campo e' obbligatorio")]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo e' obbligatorio")]
        [Url(ErrorMessage = "URL non valido")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo e' obbligatorio")]
        [Range(20, 10000, ErrorMessage = "Il viaggio non puo' costare meno di 20 euro")]
        public double Price { get; set; }

        public List<User> Travels { get; set; }

        public Travel()
        {

        }

        public Travel(string title, string description, string image, double price)
        {
            this.Title = title;
            this.Description = description;
            this.Image = image;
            this.Price = price;
        }
    }
}