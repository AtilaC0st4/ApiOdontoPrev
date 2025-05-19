using OdontoPrev.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public int Level => Points / 100;
    public string Cep { get; set; }

    // Dados resolvidos do endereço (via ViaCep)
    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    public List<BrushingRecordDto>? BrushingRecords { get; set; }
}
