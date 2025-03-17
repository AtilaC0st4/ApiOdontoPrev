

using System.ComponentModel.DataAnnotations;

namespace OdontoPrev.Models
{
    public class BrushingRecord
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public DateTime BrushingTime { get; set; }

        [Required]
        [StringLength(10)] 
        public string Period { get; set; } 
    }
}