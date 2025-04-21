using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryCard : Card
{
    public int id; // значение карточки
    public Button button;
    public TextMeshProUGUI text;

    private GameController controller;

    public bool IsMatched { get; private set; } = false;

    // Метод для инициализации карточки
    public void Setup(int value, GameController gameController)
    {
        id = value;
        IsMatched = false;
        isFlipped = false;

        if (gameController == null)
        {
            Debug.LogError("GameController is null!");
            return;
        }

        controller = gameController;
        text.text = ""; // скрыт по умолчанию
        button.interactable = true;
    }

    // Метод при клике на карточку
    public void OnClick()
    {
        if (isFlipped || controller == null || IsMatched) return;

        isFlipped = true;
        text.text = id.ToString(); // показываем значение

        controller.CardRevealed(this);
    }

    // Метод для скрытия карточки
    public void Hide()
    {
        isFlipped = false;
        text.text = "";
    }

    // Метод для отключения карточки при совпадении
    public void Disable()
    {
        IsMatched = true;
        button.interactable = false;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component is missing on this MemoryCard!");
            return;
        }

        button.onClick.AddListener(OnClick);
    }
}
