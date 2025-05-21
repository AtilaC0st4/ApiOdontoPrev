using Microsoft.ML;
using OdontoPrev.Models.ML;

namespace OdontoPrev.Services
{
    public class MlModelService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<BrushingData, BrushingPrediction> _predictionEngine;

        public MlModelService()
        {
            _mlContext = new MLContext();
            var trainingData = GenerateMockData();
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(BrushingData.Points), nameof(BrushingData.WeeklyFrequency))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            _model = pipeline.Fit(dataView);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<BrushingData, BrushingPrediction>(_model);
        }

        public BrushingPrediction Predict(float points, float weeklyFrequency)
        {
            var input = new BrushingData { Points = points, WeeklyFrequency = weeklyFrequency };
            return _predictionEngine.Predict(input);
        }

        private List<BrushingData> GenerateMockData()
        {
            return new List<BrushingData>
            {
                new BrushingData { Points = 400, WeeklyFrequency = 18, IsActive = true },
                new BrushingData { Points = 120, WeeklyFrequency = 12, IsActive = true },
                new BrushingData { Points = 60, WeeklyFrequency = 5, IsActive = false },
                new BrushingData { Points = 20, WeeklyFrequency = 2, IsActive = false },
                new BrushingData { Points = 250, WeeklyFrequency = 10, IsActive = true },
                new BrushingData { Points = 80, WeeklyFrequency = 4, IsActive = false },
                new BrushingData { Points = 300, WeeklyFrequency = 15, IsActive = true },
                new BrushingData { Points = 100, WeeklyFrequency = 3, IsActive = false },
            };
        }
    }
}

