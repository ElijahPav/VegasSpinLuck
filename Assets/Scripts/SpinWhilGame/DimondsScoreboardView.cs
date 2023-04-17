using System.Collections.Generic;
using UnityEngine;

public class DimondsScoreboardView : MonoBehaviour
{
    [SerializeField] private DimondScoreView[] _dimondScoreViews;

    private Dictionary<PrizesType, DimondScoreView> _prizeViewPairs;

    public void SetData(SpinWheelPrize[] spinWheelPrizes, 
                        Dictionary<PrizesType, int> prizeScorePair)
    {

        _prizeViewPairs = new Dictionary<PrizesType, DimondScoreView>(_dimondScoreViews.Length);

        for (int i = 0; i < _dimondScoreViews.Length; i++)
        {
            var type = spinWheelPrizes[i].PrizeType;
            _dimondScoreViews[i].SetSprite(spinWheelPrizes[i].Sprite);
            _dimondScoreViews[i].SetText(prizeScorePair[type].ToString());
            _prizeViewPairs.Add(spinWheelPrizes[i].PrizeType, _dimondScoreViews[i]);
        }
    }

    public void UpdateDimondScore(PrizesType type, int newScore)
    {
        _prizeViewPairs[type].SetText(newScore.ToString());
    }
}
