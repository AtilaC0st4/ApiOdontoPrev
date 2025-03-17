using System.ComponentModel.DataAnnotations;

namespace OdontoPrev.Models
{
    public class User
    {
        [Key] // Define a chave primária
        public int Id { get; set; }

        [Required] /
        [StringLength(100)] 
        public string Name { get; set; }

        [Required]
        public int Points { get; set; }
    }

}
