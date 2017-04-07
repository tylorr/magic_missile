using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;

    public Color[] playerColors;
    public Transform[] playerSpawnMarkers;
    
    private bool _gameStarted = false;

    public void StartGame(PlayerConfig[] playerConfigs)
    {
        _gameStarted = true;
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var playerConfig = playerConfigs[i];
            if (playerConfig.inputDevice == null)
            {
                continue;
            }
            var player = Instantiate(playerPrefab, playerSpawnMarkers[i].position, Quaternion.identity);
            player.SetColor(playerColors[i]);
            player.SetInputDevice(playerConfig.inputDevice);
            player.SetTankControls(playerConfig.tankControls);
            player.gameObject.layer = GameLayers.PlayerLayerFromIndex(i);
        }
    }

    private void Update()
    {
        if (!_gameStarted) return;

        if (InputManager.ActiveDevice.GetControl(InputControlType.Start).WasPressed || InputManager.ActiveDevice.GetControl(InputControlType.Options).WasPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
