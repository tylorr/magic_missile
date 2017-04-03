using InControl;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;

    public Color[] playerColors;
    public Transform[] playerSpawnMarkers;

    public void StartGame(InputDevice[] inputDevices)
    {
        for (int i = 0; i < inputDevices.Length; i++)
        {
            var inputDevice = inputDevices[i];
            if (inputDevice == null)
            {
                continue;
            }
            var player = Instantiate(playerPrefab, playerSpawnMarkers[i].position, Quaternion.identity);
            player.SetColor(playerColors[i]);
            player.SetInputDevice(inputDevice);
            player.gameObject.layer = GameLayers.PlayerLayerFromIndex(i);
        }
    }
}
