using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpinWheelController : MonoBehaviour
{
    [SerializeField] private SpinWheelView _spinWheelView;
    [SerializeField] private Button _stratGameButton;
    [SerializeField] private Button _goBackButton;
    [SerializeField] private SpinWheelPrize[] _spinWheelPrizes;
    [SerializeField] private DimondsScoreboardController _scoreboardController;


    private void Start()
    {
        _stratGameButton.onClick.AddListener(StartGame);
        _goBackButton.onClick.AddListener(DestroyGameObject);

    }

    private void OnDestroy()
    {
        _stratGameButton.onClick.RemoveListener(StartGame);
        _goBackButton.onClick.RemoveListener(DestroyGameObject);
    }
    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

    private async void StartGame()
    {
        _stratGameButton.gameObject.SetActive(false);
        _goBackButton.gameObject.SetActive(false);  
        var prize = GeneratePrize();
        await _spinWheelView.SpinToPrize(prize);
        if (prize.PrizeType != PrizesType.Bomb)
        {
            _scoreboardController.AddDimondPoints(prize.PrizeType, 1);
        }
        _stratGameButton.gameObject.SetActive(true);
        _goBackButton.gameObject.SetActive(true);


    }

    private SpinWheelPrize GeneratePrize()
    {
        var sortedPrizes = _spinWheelPrizes.OrderBy(ob => ob.Probability).ToArray();
        int probabilitySum = 0;
        foreach (var prize in sortedPrizes)
        {
            probabilitySum += prize.Probability;
        }

        int randomPoint = Random.Range(1, probabilitySum);

        SpinWheelPrize generatedPrize = null;
        for (int i = 0; i < sortedPrizes.Length; i++)
        {
            if (randomPoint <= sortedPrizes[i].Probability)
            {
                generatedPrize = sortedPrizes[i];
                break;
            }
            randomPoint -= sortedPrizes[i].Probability;
        }

        return generatedPrize;
    }
}
