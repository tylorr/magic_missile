using UnityEngine;

public static class GameLayers
{
    public static readonly int Player1 = LayerMask.NameToLayer("Player 1");
    public static readonly int Player2 = LayerMask.NameToLayer("Player 2");
    public static readonly int Player3 = LayerMask.NameToLayer("Player 3");
    public static readonly int Player4 = LayerMask.NameToLayer("Player 4");
    public static readonly int Environment = LayerMask.NameToLayer("Environment");

    public static int AllPlayers()
    {
        return Player1 | Player2 | Player3 | Player4;
    }

    public static int PlayerLayerFromIndex(int index)
    {
        switch (index)
        {
            case 0: return Player1;
            case 1: return Player2;
            case 2: return Player3;
            case 3: return Player4;
        }
        return -1;
    }
}
