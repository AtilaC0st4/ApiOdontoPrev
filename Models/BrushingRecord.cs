using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrev.Models
{
    public class BrushingRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BrushingTime { get; set; }

        // Relacionamento com usuário
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
