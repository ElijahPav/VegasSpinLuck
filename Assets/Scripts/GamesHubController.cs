using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamesHubController : MonoBehaviour
{
    [SerializeField] private Button _spinWeelGameButton;
    [SerializeField] private Button _bonusGameButton;
    [SerializeField] private Button _findByMemoryGameButton;

    [SerializeField] private GameObject _spinWeelGamePrefab;
    [SerializeField] private GameObject _bonusGamePrefab;
    [SerializeField] private GameObject _findByMemoryGamePrefab;

    private void Start()
    {
        _spinWeelGameButton.onClick.AddListener(CreateSpinWeelGame);
        _bonusGameButton.onClick.AddListener(CreateBonusGame);
        _findByMemoryGameButton.onClick.AddListener(CreateFindByMemoryGame);
    }
    private void OnDestroy()
    {
        _spinWeelGameButton.onClick.RemoveListener(CreateSpinWeelGame);
        _bonusGameButton.onClick.RemoveListener(CreateBonusGame);
        _findByMemoryGameButton.onClick.RemoveListener(CreateFindByMemoryGame);

    }
    private void CreateSpinWeelGame()
    {
        Instantiate(_spinWeelGamePrefab);
    }
    private void CreateBonusGame()
    {
        Instantiate(_bonusGamePrefab);
    }
    private void CreateFindByMemoryGame()
    {
        Instantiate(_findByMemoryGamePrefab);
    }

}
