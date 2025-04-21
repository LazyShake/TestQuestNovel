using Naninovel;
using UnityEngine;

[CommandAlias("checkScore")]
public class CheckScoreCommand : Command
{
    // Пороговое значение очков, передаваемое в команде, например: @checkScore 5
    [ParameterAlias("")]
    public IntegerParameter Threshold;

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var scoreManager = Engine.GetService<ScoreManager>();
        int currentScore = scoreManager.GetScore();

        if (!Threshold.HasValue)
        {
            Debug.LogWarning("Threshold not provided to checkScore command.");
            return;
        }

        int thresholdValue = Threshold.Value;

        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        var stateManager = Engine.GetService<IStateManager>();

        if (currentScore >= thresholdValue)
        {
            // Ветка "если очков достаточно или больше"
            Debug.Log($"Score {currentScore} >= {thresholdValue}, jumping to label: HighScorePath");
            await scriptPlayer.PreloadAndPlayAsync("HighScorePath");
        }
        else
        {
            // Ветка "если очков меньше"
            Debug.Log($"Score {currentScore} < {thresholdValue}, jumping to label: LowScorePath");
            await scriptPlayer.PreloadAndPlayAsync("LowScorePath");
        }
    }
}
