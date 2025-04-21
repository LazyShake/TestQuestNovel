using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public List<MemoryCard> cards; // карточки
    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    public float revealDelay = 1f;

    // 🆕 UI элементы
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button continueButton;

    // 🆕 Флаг блокировки ввода
    private bool isInputBlocked = false;

    private void Start()
    {
        InitializeCards();
        // 🆕 Панель завершения скрыта изначально
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void InitializeCards()
    {
        List<int> values = new List<int>();

        // Создаем пары чисел (8 пар по 2)
        for (int i = 0; i < 8; i++)
        {
            values.Add(i);
            values.Add(i);
        }

        // Перемешиваем
        for (int i = 0; i < values.Count; i++)
        {
            int temp = values[i];
            int randomIndex = Random.Range(i, values.Count);
            values[i] = values[randomIndex];
            values[randomIndex] = temp;
        }

        // Назначаем значения карточкам
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Setup(values[i], this);
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        // Блокируем ввод, если идет проверка пары
        if (isInputBlocked) return;

        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else if (secondRevealed == null)
        {
            secondRevealed = card;
            isInputBlocked = true; // Блокируем ввод до завершения проверки
            SetCardsInteractable(false); // Отключаем все карты
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(revealDelay);

        if (firstRevealed.id == secondRevealed.id)
        {
            firstRevealed.Disable();
            secondRevealed.Disable();
        }
        else
        {
            firstRevealed.Hide();
            secondRevealed.Hide();
        }

        firstRevealed = null;
        secondRevealed = null;

        // Разблокируем ввод
        SetCardsInteractable(true);
        isInputBlocked = false;

        // Проверка окончания игры
        if (IsGameComplete())
        {
            Debug.Log("Игра завершена!");
            ShowGameOver();
        }
        else
        {
            Debug.Log("Пары не совпали, продолжаем игру.");
        }
    }

    void SetCardsInteractable(bool isInteractable)
    {
        foreach (var card in cards)
        {
            card.button.interactable = isInteractable;
        }
    }

    bool IsGameComplete()
    {
        foreach (var card in cards)
        {
            if (card.button.interactable)
                return false;
        }
        return true;
    }

    // 🆕 Показать завершение игры
    void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverText.text = "Поздравляем, вы нашли все пары!";
            gameOverPanel.SetActive(true);

            // Убираем старые слушатели перед добавлением нового
            continueButton.onClick.RemoveListener(OnContinueButtonClick);

            // Добавляем новый слушатель
            continueButton.onClick.AddListener(OnContinueButtonClick);
        }
    }

    // 🆕 Логика по кнопке "Продолжить"
    void OnContinueButtonClick()
    {
        // Здесь можно вставить кастомную Naninovel-команду или переход к сценарию
        Debug.Log("Продолжить игру или выйти в новеллу");

        // Скрыть панель завершения игры
        gameOverPanel.SetActive(false);

        // Можно добавить здесь переход к следующему уровню или сценарию
        // SceneManager.LoadScene("NextScene");
    }
}
