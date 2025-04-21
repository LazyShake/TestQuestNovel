using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image frontImage; // Лицевая сторона
    public Image backImage;  // Обратная сторона
    protected bool isFlipped = false;

    // Переворачиваем карточку
    public void FlipCard()
    {
        isFlipped = !isFlipped;
        frontImage.enabled = !isFlipped;
        backImage.enabled = isFlipped;
    }
}


