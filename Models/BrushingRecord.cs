namespace OdontoPrev.Models
{

    public class BrushingRecord
    {
        public int Id { get; set; }
        public DateTime BrushingTime { get; set; }
        public string Period { get; set; } // "Morning", "Afternoon", "Night"
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
