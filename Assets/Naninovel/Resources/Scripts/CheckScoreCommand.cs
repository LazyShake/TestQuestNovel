using Naninovel;

[CommandAlias("CheckScore")]
public class CheckScoreCommand : Command
{
    private ScoreManager scoreManager;

    public CheckScoreCommand()
    {
        scoreManager = Engine.GetService<ScoreManager>();  // Получаем сервис ScoreManager
    }

    // Реализация метода ExecuteAsync
    public override UniTask ExecuteAsync(AsyncToken asyncToken)
    {
        int currentScore = scoreManager.GetScore();  // Получаем текущие очки
        var textPrinter = Engine.GetService<ITextPrinter>();  // Получаем сервис ITextPrinter
        textPrinter.PrintText($"Current Score: {currentScore}");  // Выводим в UI

        return UniTask.CompletedTask;  // Завершаем выполнение
    }
}
