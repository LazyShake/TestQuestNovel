using Naninovel;
using UnityEngine;

[CommandAlias("AddScore")]
public class AddScoreCommand : Command
{
    [ParameterAlias("")]
    public IntegerParameter points;


    private ScoreManager scoreManager;

    public AddScoreCommand()
    {
        scoreManager = Engine.GetService<ScoreManager>();  // Получаем сервис ScoreManager
    }

    // Реализация метода ExecuteAsync
    public override UniTask ExecuteAsync(AsyncToken asyncToken)
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(points);  // Добавляем очки
        }
        else
        {
            Debug.LogError("ScoreManager не найден!");
        }
        return UniTask.CompletedTask;  // Завершаем выполнение
    }
}
