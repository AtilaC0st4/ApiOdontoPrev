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

    [NotMapped]
    public int Level => Points / 100;

    public ICollection<BrushingRecord>? BrushingRecords { get; set; }

    // Campos de endereço
    [StringLength(9)]
    public string? Cep { get; set; }

    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    public void AddPoints(int additionalPoints)
    {
        Points += additionalPoints;
    }
}
