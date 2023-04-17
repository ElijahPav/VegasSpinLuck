using System;
using UnityEngine;
using UnityEngine.UI;

public class FindByMemoryElementController : MonoBehaviour
{
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private Image _imageComponent;
    [SerializeField] private Sprite _hidenDimondSprite;

    public event Action<int, SpinWheelPrize> OnElementClitck;

    private bool IsAlreadyOpen;
    private int _index;
    private Sprite _dimondSprite;
    private SpinWheelPrize _wheelPrizeData;

    private void Start()
    {
        _buttonComponent.onClick.AddListener(OnElementClickInvoke);
    }
    private void OnDestroy()
    {
        _buttonComponent.onClick.RemoveListener(OnElementClickInvoke);
    }

    private void OnElementClickInvoke()
    {
        if (!IsAlreadyOpen)
        {
            OnElementClitck?.Invoke(_index, _wheelPrizeData);
        }
    }

    public void SetData(int index, SpinWheelPrize wheelPrizeData)
    {
        _index = index;
        _wheelPrizeData = wheelPrizeData;
        _dimondSprite = wheelPrizeData.Sprite;
    }

    public void ShowDimond()
    {
        IsAlreadyOpen = true;
        _imageComponent.sprite = _dimondSprite;
        _imageComponent.SetNativeSize();
    }
    public void HideDimond()
    {
        IsAlreadyOpen = false;
        _imageComponent.sprite = _hidenDimondSprite;
        _imageComponent.SetNativeSize();
    }




}
