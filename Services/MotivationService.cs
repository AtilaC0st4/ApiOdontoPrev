namespace OdontoPrev.Services
{
    public class MotivationService
    {
        public string GenerateMessage(int level, int weeklyFrequency, int points)
        {
            if (weeklyFrequency >= 14)
                return $"Parabéns, guerreiro do sorriso! Com {weeklyFrequency} escovações na semana e {points} pontos, você está no nível {level}. Continue assim e conquiste mais recompensas!";
            else if (weeklyFrequency >= 7)
                return $"Você está indo bem! {weeklyFrequency} escovações por semana e {points} pontos já te colocam no nível {level}. Vamos subir juntos!";
            else
                return $"Você está no nível {level} com {points} pontos. Que tal reforçar sua rotina e escovar mais vezes? Seu sorriso merece!";
        }
    }
}
