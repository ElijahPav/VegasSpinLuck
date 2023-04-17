using System;
using System.Threading.Tasks;
using UnityEngine;

public class SpinWheelView : MonoBehaviour
{
    [SerializeField] private RectTransform _movablePartOfWheel;
    [SerializeField] private PrizesType[] _prizesSortOnWeel;

    private const int _spinDuration = 100;

    private const float _startPrizeAngle = 22.5f;
    private const float _angleBetweenPrizes = 45f;
    private const float _rotaionAngle = 10f;

    private const int _defaultSpinDelay = 1;
    private const int _slowdown—oefficient = 1;
    private const float _percentForSlowdown = 0.7f;
    public async Task SpinToPrize(SpinWheelPrize prize)
    {
        var weelAngeWithPrize = _startPrizeAngle +
            Array.IndexOf(_prizesSortOnWeel, prize.PrizeType) * _angleBetweenPrizes;
        var crrentDelay = _defaultSpinDelay;
        int borderIndex = Mathf.RoundToInt(_spinDuration * _percentForSlowdown);

        for (int i = 0; i < _spinDuration; i++)
        {
            _movablePartOfWheel.Rotate(0, 0, _rotaionAngle);

            await Task.Delay(crrentDelay);
            if (i > borderIndex)
            {
                crrentDelay += _slowdown—oefficient;
            }
        }

        while (_movablePartOfWheel.eulerAngles.z - weelAngeWithPrize > 5f
               || _movablePartOfWheel.eulerAngles.z - weelAngeWithPrize < -5f)
        {
            _movablePartOfWheel.Rotate(0, 0, _rotaionAngle / 2);
            await Task.Delay(crrentDelay);
        }
    }
}
