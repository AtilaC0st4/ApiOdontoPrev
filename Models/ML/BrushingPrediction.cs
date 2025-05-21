using Microsoft.ML.Data;

namespace OdontoPrev.Models.ML
{
    public class BrushingPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
