using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusGameController : MonoBehaviour
{
    [SerializeField] private BonusGameElementController[] _bonusGameElements;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _goBackButton;
    [SerializeField] private TextMeshProUGUI _gameResultText;

    private const string _winText = "You Won!";
    private const string _loseText = "You Lose ";

    private const int maxAmountOfAttempts = 3;

    private int prizeElmentIndex;
    private int currentAmountOfAttempts;
    private bool _isGameActive;

    private void Start()
    {
        for (int i = 0; i < _bonusGameElements.Length; i++)
        {
            _bonusGameElements[i].SetIdex(i);
            _bonusGameElements[i].OnElementClitck += ElementClicked;
        }

        _startGameButton.onClick.AddListener(StartGame);
        _goBackButton.onClick.AddListener(DestroyGameObject);
    }

    private void OnDestroy()
    {
        foreach (var bonusGameElement in _bonusGameElements)
        {
            bonusGameElement.OnElementClitck -= ElementClicked;
        }
        _startGameButton.onClick.RemoveListener(StartGame);
        _goBackButton.onClick.RemoveListener(DestroyGameObject);
    }
    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

    private void StartGame()
    {
        foreach (var bonusGameElement in _bonusGameElements)
        {
            bonusGameElement.SetDefaultSprite();
        }

        _goBackButton.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(false);
        _gameResultText.gameObject.SetActive(false);

        prizeElmentIndex = Random.Range(0, _bonusGameElements.Length);
        currentAmountOfAttempts = 0;
        _isGameActive = true;
    }

    private void ElementClicked(int index)
    {
        if (_isGameActive)
        {
            if (index == prizeElmentIndex)
            {
                _bonusGameElements[index].OpenAsPrizeElemnt();
                CompleteGame(true);
            }
            else
            {
                _bonusGameElements[index].OpenAsEmptyElement();
                currentAmountOfAttempts++;

                if (currentAmountOfAttempts >= maxAmountOfAttempts)
                {
                    CompleteGame(false);
                }
            }
        }
    }

    private void CompleteGame(bool isPlayerWin)
    {
        _isGameActive = false;

        if (isPlayerWin)
        {
            _gameResultText.text = _winText;
        }
        else
        {
            _gameResultText.text = _loseText;
        }

        _goBackButton.gameObject.SetActive(true);
        _startGameButton.gameObject.SetActive(true);
        _gameResultText.gameObject.SetActive(true);

    }
}
