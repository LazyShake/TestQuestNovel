using Naninovel;
using UnityEngine;
using UnityEngine.SceneManagement;


[CommandAlias("startMiniGame")]
public class StartMiniGame : Command
{
    public string miniGameSceneName = "FindPairGame";  // Название сцены с мини-игрой

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
{
    // Загружаем миниигру
    await LoadMiniGameSceneAsync();
}


    private async UniTask LoadMiniGameSceneAsync()
    {
        // Начинаем загрузку сцены
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(miniGameSceneName, LoadSceneMode.Additive);


        // Ждём, пока сцена загрузится
        await asyncLoad.ToUniTask();

        // Убедимся, что сцена загружена и установим её активной
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
