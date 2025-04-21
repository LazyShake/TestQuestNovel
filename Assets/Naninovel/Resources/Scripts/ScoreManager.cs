using Naninovel;
using UnityEngine;

[InitializeAtRuntime]
public class ScoreManager : IEngineService
{
    public static ScoreManager Instance;
    private int score;
    private ScoreUI scoreUI;

    public int Score => score;

    public Naninovel.UniTask InitializeServiceAsync ()
    {
        if (Instance == null) 
            Instance = this;  // Инициализация Singleton

        // Попробуем найти компонент UI в сцене
        scoreUI = Object.FindObjectOfType<ScoreUI>();
        UpdateUI();
        return Naninovel.UniTask.CompletedTask;
    }

    public void ResetService () => score = 0;

    public void DestroyService () { }

    public void AddScore (int value)
    {
        score += value;
        UpdateUI();
    }

    public int GetScore () => score;

    private void UpdateUI ()
    {
        if (scoreUI != null)
            scoreUI.SetScore(score);
    }

}
