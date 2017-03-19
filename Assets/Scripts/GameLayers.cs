using UnityEngine;

public static class GameLayers
{
    public static readonly int Player1 = LayerMask.NameToLayer("Player 1");
    public static readonly int Player2 = LayerMask.NameToLayer("Player 2");
    public static readonly int Environment = LayerMask.NameToLayer("Environment");
}
