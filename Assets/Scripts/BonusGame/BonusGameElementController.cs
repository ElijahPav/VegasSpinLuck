using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BonusGameElementController : MonoBehaviour
{
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private Sprite _safeSprite;
    [SerializeField] private Sprite _coinBagSprite;
    [SerializeField] private Image _imageComponent;

    public event Action<int> OnElementClitck;

    private int _index;

    private void Start()
    {
        _buttonComponent.onClick.AddListener(OnElementClickInvoke);
    }
    public void SetIdex(int index) => _index = index;

    public void OpenAsPrizeElemnt()
    {
        _imageComponent.sprite = _coinBagSprite;
        _imageComponent.SetNativeSize();

    } 
    public void OpenAsEmptyElement()
    {
        _imageComponent.enabled = false;
    }

    public void SetDefaultSprite()
    {
        _imageComponent.enabled = true;
        _imageComponent.sprite = _safeSprite;
        _imageComponent.SetNativeSize();
    }

    private void OnElementClickInvoke()
    {
        OnElementClitck?.Invoke(_index);
    }
}
