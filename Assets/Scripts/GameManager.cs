using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;

    public Color[] playerColors;
    public Transform[] playerSpawnMarkers;

    public PlayerConfig[] _playerConfigs;

    private MetaPlayer[] metaPlayers;
    private bool _gameStarted = false;

    void Awake() 
    {
        metaPlayers = new MetaPlayer[4];
    }

    public void StartGame(PlayerConfig[] playerConfigs)
    {
        if (_playerConfigs == null)
        {
            _playerConfigs = playerConfigs;    
        }

        _gameStarted = true;

        for (int i = 0; i < playerConfigs.Length; i++)
        {
            if (metaPlayers[i] == null)
            {
                var metaPlayer = new MetaPlayer()
                {
                    config = playerConfigs[i],
                    color = playerColors[i]
                };
                metaPlayers[i] = metaPlayer;
            } 
            else
            {
                if (metaPlayers[i].player != null)
                {
                    Destroy(metaPlayers[i].player.gameObject);
				}
            }

            if (metaPlayers[i].config == null)
            {
                continue;
            }
            var player = Instantiate(playerPrefab, playerSpawnMarkers[i].position, Quaternion.identity);
            player.SetColor(metaPlayers[i].color);
            player.SetInputDevice(metaPlayers[i].config.inputDevice);
            player.SetTankControls(metaPlayers[i].config.tankControls);
            player.SetMetaPlayer(metaPlayers[i]);
            player.gameObject.layer = GameLayers.PlayerLayerFromIndex(i);
            metaPlayers[i].player = player;
        }
    }

    private void Update()
    {
        if (!_gameStarted) return;

        if (InputManager.ActiveDevice.GetControl(InputControlType.Start).WasPressed || InputManager.ActiveDevice.GetControl(InputControlType.Options).WasPressed)
        {
            StartGame(_playerConfigs);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
