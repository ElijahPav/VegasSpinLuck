using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DimondScoreView : MonoBehaviour
{
    [SerializeField] private Image _dimondImage;
    [SerializeField] private TextMeshProUGUI _dimondScoreText;

    public void SetSprite(Sprite sprite)
    {
        _dimondImage.sprite = sprite;
    }
    public void SetText(string score)
    {
        _dimondScoreText.text = score;
    }

}
