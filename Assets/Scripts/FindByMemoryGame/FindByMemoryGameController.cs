using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FindByMemoryGameController : MonoBehaviour
{
    [SerializeField] private FindByMemoryElementController[] _elementsControllers;
    [SerializeField] private SpinWheelPrize[] _spinWheelPrizes;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _goBackButton;
    [SerializeField] private TextMeshProUGUI _resultGameText;

    private const int _timeToRememberInSeconds = 10;
    private const int _timeForGameInSeconds = 60;
    private const string _winGameText = "You Won!";
    private const string _timeOutText = "Time Out ";

    private bool _isInputAvailable;
    private bool _isPlayerWin;


    private int[] _availableAmountDimondsPairs = new int[7] { 6, 5, 4, 3, 3, 2, 2 };

    private int _numberOfFoundPairs;
    private Dictionary<PrizesType, int> _typeAmountPair;
    private SpinWheelPrize _prevousClickedElementPrize;
    private List<FindByMemoryElementController> _temporaryOpenedElements;

    private CancellationTokenSource _cancellationTokenSource;

    private void Start()
    {
        foreach (var element in _elementsControllers)
        {
            element.OnElementClitck += ElementClicked;
            element.HideDimond();
        }
        _startGameButton.onClick.AddListener(StartGame);
        _goBackButton.onClick.AddListener(DestroyGameObject);
    }
    private void OnDestroy()
    {
        foreach (var element in _elementsControllers)
        {
            element.OnElementClitck -= ElementClicked;
        }

        _cancellationTokenSource?.Dispose();

        _startGameButton.onClick.RemoveListener(StartGame);
        _goBackButton.onClick.RemoveListener(DestroyGameObject);
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
    private async void StartGame()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _isPlayerWin = false;
        _prevousClickedElementPrize = null;
        _numberOfFoundPairs = 0;

        _resultGameText.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(false);
        _goBackButton.gameObject.SetActive(false);

        var token = _cancellationTokenSource.Token;
        _temporaryOpenedElements = new List<FindByMemoryElementController>(6);

        var generatedData = GenerateDataForElements();
        SetDataForElements(generatedData);

        await SartTimer(_timeToRememberInSeconds, token);

        foreach (var element in _elementsControllers)
        {
            element.HideDimond();
        }

        _isInputAvailable = true;

        await SartTimer(_timeForGameInSeconds, token);
        _isInputAvailable = false;

        if (!_isPlayerWin)
        {
            _resultGameText.text = _timeOutText;
        }

        _resultGameText.gameObject.SetActive(true);
        _startGameButton.gameObject.SetActive(true);
        _goBackButton.gameObject.SetActive(true);

        _cancellationTokenSource.Dispose();

    }

    private async Task SartTimer(int time, CancellationToken token)
    {
        _timerText.text = time.ToString();
        do
        {
            await Task.Delay(1000);
            time--;
            _timerText.text = time.ToString();

        } while (time > 0 && !token.IsCancellationRequested);

    }



    private SpinWheelPrize[] GenerateDataForElements()
    {
        Random rnd = new Random();
        var prizesToElements = new List<SpinWheelPrize>(_elementsControllers.Length);

        var jumbledAmounsArray = _availableAmountDimondsPairs.OrderBy(n => rnd.Next()).ToArray();
        _typeAmountPair = new Dictionary<PrizesType, int>(jumbledAmounsArray.Length);

        for (int i = 0; i < jumbledAmounsArray.Length; i++)
        {
            for (int j = 0; j < jumbledAmounsArray[i]; j++)
            {
                prizesToElements.Add(_spinWheelPrizes[i]);
            }

            _typeAmountPair.Add(_spinWheelPrizes[i].PrizeType, jumbledAmounsArray[i]);
        }

        return prizesToElements.OrderBy(n => rnd.Next()).ToArray();
    }

    private void SetDataForElements(SpinWheelPrize[] prizesData)
    {
        for (int i = 0; i < _elementsControllers.Length; i++)
        {
            _elementsControllers[i].SetData(i, prizesData[i]);
            _elementsControllers[i].ShowDimond();
        }
    }


    private void ElementClicked(int index, SpinWheelPrize prizeData)
    {
        if (_isInputAvailable)
        {
            var clicedElement = _elementsControllers[index];
            clicedElement.ShowDimond();

            if (_prevousClickedElementPrize != null
                && prizeData.PrizeType == _prevousClickedElementPrize.PrizeType)
            {
                _temporaryOpenedElements.Add(clicedElement);
                if (_typeAmountPair[prizeData.PrizeType] == _temporaryOpenedElements.Count)
                {
                    _numberOfFoundPairs += 1;
                    _temporaryOpenedElements.Clear();
                    _prevousClickedElementPrize = null;

                    if (_numberOfFoundPairs == _spinWheelPrizes.Length)
                    {
                        _isInputAvailable = false;
                        GaweWinProcess();
                    }
                }
            }
            else
            {
                if (_temporaryOpenedElements.Count > 0)
                {
                    foreach (var element in _temporaryOpenedElements)
                    {
                        element.HideDimond();
                    }
                    _temporaryOpenedElements.Clear();
                }
                _temporaryOpenedElements.Add(clicedElement);
                _prevousClickedElementPrize = prizeData;

            }
        }
    }
    private void GaweWinProcess()
    {
        _isPlayerWin = true;
        _cancellationTokenSource.Cancel();
        _resultGameText.text = _winGameText;
    }
}
