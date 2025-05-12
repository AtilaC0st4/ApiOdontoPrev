using OdontoPrev.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public int Points { get; set; }

    // Propriedade somente leitura, calculada a partir dos pontos
    [NotMapped]
    public int Level => Points / 100;

    // Se quiser armazenar escovações relacionadas
    public ICollection<BrushingRecord>? BrushingRecords { get; set; }

    // Método para atualizar os pontos (opcional)
    public void AddPoints(int additionalPoints)
    {
        Points += additionalPoints;
    }
}
