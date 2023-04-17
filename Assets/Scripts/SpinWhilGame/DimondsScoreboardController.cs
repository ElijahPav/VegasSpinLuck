using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DimondsScoreboardController : MonoBehaviour
{
    [SerializeField] private SpinWheelPrize[] _spinWheelPrizes;
    [SerializeField] private DimondsScoreboardView _dimondsScoreboardView;

    private const string _nameOfScorFile = "/ScoreData.json";
    private Dictionary<PrizesType, int> _prizeValuePairs;

    private void Start()
    {
        _spinWheelPrizes = _spinWheelPrizes.OrderBy(ob => ob.Probability).ToArray();
        LoadScoreFiel();
        _dimondsScoreboardView.SetData(_spinWheelPrizes, _prizeValuePairs);

    }
    private void LoadScoreFiel()
    {
        try
        {
            _prizeValuePairs = JsonConvert.DeserializeObject<Dictionary<PrizesType, int>>(
                File.ReadAllText(Application.streamingAssetsPath + _nameOfScorFile));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);

            _prizeValuePairs = new Dictionary<PrizesType, int>(_spinWheelPrizes.Length);
           
            foreach (var prize in _spinWheelPrizes)
            {
                _prizeValuePairs.Add(prize.PrizeType, 0);
            }
        }
    }

    private void SaveScoreToFile()
    {
        var serializedData  = JsonConvert.SerializeObject(_prizeValuePairs);
        File.WriteAllText(Application.streamingAssetsPath + _nameOfScorFile, serializedData);
    }

    public void AddDimondPoints(PrizesType type, int amount)
    {
        _prizeValuePairs[type] += amount;
        _dimondsScoreboardView.UpdateDimondScore(type, _prizeValuePairs[type]);

    }
    private void OnDestroy()
    {
        SaveScoreToFile();
    }
}
