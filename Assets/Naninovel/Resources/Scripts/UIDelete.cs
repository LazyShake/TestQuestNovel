using UnityEngine;

public class DisableNaninovelUI : MonoBehaviour
{
    void Start()
    {
        // Попробуем найти Naninovel UI, если он сохранился как DontDestroyOnLoad
        var ui = GameObject.Find("NaninovelUI");
        if (ui != null)
        {
            ui.SetActive(false);
            Debug.Log("Naninovel UI отключен.");
        }
        else
        {
            Debug.Log("Naninovel UI не найден.");
        }
    }
}