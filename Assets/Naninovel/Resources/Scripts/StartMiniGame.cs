using Naninovel;
using UnityEngine;
using UnityEngine.SceneManagement;


[CommandAlias("startMiniGame")]
public class StartMiniGame : Command
{
    public string miniGameSceneName = "FindPairGame";  

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
{
    
    await LoadMiniGameSceneAsync();
}


    private async UniTask LoadMiniGameSceneAsync()
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(miniGameSceneName, LoadSceneMode.Additive);


        
        await asyncLoad.ToUniTask();

        
        Scene scene = SceneManager.GetSceneByName(miniGameSceneName);
        if (scene.IsValid() && scene.isLoaded)
        {
            SceneManager.SetActiveScene(scene);
        }
        else
        {
            Debug.LogError($"Сцена '{miniGameSceneName}' не загружена корректно.");
        }
    }
}
