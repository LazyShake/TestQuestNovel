using Naninovel;
using UnityEngine;

[CommandAlias("checkScore")]
public class CheckScoreCommand : Command
{
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

        var currentScriptName = scriptPlayer.PlayedScript?.Name;
        if (string.IsNullOrEmpty(currentScriptName))
        {
            Debug.LogError("Failed to get the name of the current script.");
            return;
        }

        string labelToJump = currentScore >= thresholdValue ? "HighScorePath" : "LowScorePath";
        Debug.Log($"Score {currentScore} vs {thresholdValue} → jumping to label #{labelToJump} in script {currentScriptName}");

        await scriptPlayer.PreloadAndPlayAsync(currentScriptName, label: labelToJump);
    }
}