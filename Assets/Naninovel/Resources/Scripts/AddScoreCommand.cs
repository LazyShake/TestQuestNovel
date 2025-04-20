using Naninovel;

[CommandAlias("AddScore")]
public class AddScoreCommand : Command
{
    public int Points { get; set; }  // Параметр для количества очков

    private ScoreManager scoreManager;

    public AddScoreCommand()
    {
        scoreManager = Engine.GetService<ScoreManager>();  // Получаем сервис ScoreManager
    }

    // Реализация метода ExecuteAsync
    public override UniTask ExecuteAsync(AsyncToken asyncToken)
    {
        scoreManager.AddScore(Points);  // Добавляем очки
        return UniTask.CompletedTask;  // Завершаем выполнение
    }
}
