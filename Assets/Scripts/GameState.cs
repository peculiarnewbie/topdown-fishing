
using UnityEngine;
public class EnumExample : MonoBehaviour
{
    public GameState gameState;
}

public enum GameState
{
    moving,
    fishing
}

public enum FishingPhase
{
    Start,
    PowerUp,
    Casted,
    Pulled

}