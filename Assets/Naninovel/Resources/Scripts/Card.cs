using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image frontImage; 
    public Image backImage;  
    protected bool isFlipped = false;


    public void FlipCard()
    {
        isFlipped = !isFlipped;
        frontImage.enabled = !isFlipped;
        backImage.enabled = isFlipped;
    }
}


