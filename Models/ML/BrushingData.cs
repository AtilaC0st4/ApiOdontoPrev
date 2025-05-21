using Microsoft.ML.Data;

namespace OdontoPrev.Models.ML
{
    public class BrushingData
    {
        public float Points { get; set; }
        public float WeeklyFrequency { get; set; }

        [ColumnName("Label")]
        public bool IsActive { get; set; }
    }
}
