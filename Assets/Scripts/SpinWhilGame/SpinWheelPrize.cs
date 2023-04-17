using Unity.VisualScripting;
using UnityEngine;

public enum PrizesType
{
    Green, 
    Blue,
    Yellow,
    SkyBlue,
    Orange,
    Violet,
    Red,
    Bomb
}

[CreateAssetMenu(fileName = "WheelPrize", menuName = "ScriptableObjects/WheelPrize", order = 1)]
public class SpinWheelPrize : ScriptableObject
{
   public Sprite Sprite;
   public int Probability;
   public bool isBomb;
   public PrizesType PrizeType;
}
