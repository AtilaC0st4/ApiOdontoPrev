namespace OdontoPrev.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Level => Points / 100;

        public List<BrushingRecordDto>? BrushingRecords { get; set; }
    }
}
