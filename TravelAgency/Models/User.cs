using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TravelAgency.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non puo' avere piu' di 50 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [EmailAddress(ErrorMessage = "mail non valida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Column(TypeName = "text")]
        public string Message { get; set; }

        public int TravelId { get; set; }

        [JsonIgnore]
        public Travel? Travel { get; set; }

        public User()
        {

        }

        /*public User(string name, string email, string message, int travelId)
        {
            Name = name;
            Email = email;
            Message = message;
            TravelId = travelId;
        }*/
    }
}
