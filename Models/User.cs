namespace OdontoPrev.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public List<BrushingRecord> BrushingRecords { get; set; }
    }

}
