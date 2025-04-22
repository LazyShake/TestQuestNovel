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
        scoreManager = Engine.GetService<ScoreManager>();  
    }

    
    public override UniTask ExecuteAsync(AsyncToken asyncToken)
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(points);  
        }
        else
        {
            Debug.LogError("ScoreManager не найден!");
        }
        return UniTask.CompletedTask;  
    }
}
