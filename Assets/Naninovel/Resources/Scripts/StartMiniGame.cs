using Naninovel;
using UnityEngine.SceneManagement;


[CommandAlias("startMiniGame")]
public class StartMiniGame : Command
{
    public override UniTask ExecuteAsync(AsyncToken token)
    {
        // Загружаем сцену мини-игры (название должно быть в Build Settings!)
        SceneManager.LoadScene("MemoryGame");
        return UniTask.CompletedTask;
    }
}
