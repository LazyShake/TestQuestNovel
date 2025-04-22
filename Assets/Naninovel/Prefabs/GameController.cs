using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Naninovel;

public class GameController : MonoBehaviour
{
    public List<MemoryCard> cards;
    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    public float revealDelay = 1f;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button continueButton;

    private bool isInputBlocked = false;

    private void Start()
    {
        InitializeCards();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void InitializeCards()
    {
        List<int> values = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            values.Add(i);
            values.Add(i);
        }

        // Shuffle values
        for (int i = 0; i < values.Count; i++)
        {
            int temp = values[i];
            int randomIndex = Random.Range(i, values.Count);
            values[i] = values[randomIndex];
            values[randomIndex] = temp;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Setup(values[i], this);
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (isInputBlocked || card.IsMatched) return;

        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else if (secondRevealed == null)
        {
            secondRevealed = card;
            isInputBlocked = true;
            SetCardsInteractable(false);
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

        SetCardsInteractable(true);
        isInputBlocked = false;

        if (IsGameComplete())
        {
            ShowGameOver();
        }
    }

    void SetCardsInteractable(bool isInteractable)
    {
        foreach (var card in cards)
        {
            if (!card.IsMatched)
                card.button.interactable = isInteractable;
        }
    }

    bool IsGameComplete()
    {
        foreach (var card in cards)
        {
            if (!card.IsMatched)
                return false;
        }
        return true;
    }

    public void ReturnToNovel()
{
    // Загружаем сцену с Naninovel (например, FinalScene)
    SceneManager.UnloadSceneAsync("FindPairGame");
    MiniGameWaiter.MiniGameFinished = true;

}

    void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverText.text = "Задание выполнено!";
            gameOverPanel.SetActive(true);
            continueButton.onClick.RemoveListener(OnContinueButtonClick);
            continueButton.onClick.AddListener(OnContinueButtonClick);
            
        }
    }

    void OnContinueButtonClick()
    {
        Debug.Log("Продолжить игру или выйти в новеллу");
        gameOverPanel.SetActive(false);
        ReturnToNovel(); // Заменить на этот вызов
    }
}
