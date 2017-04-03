using InControl;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerSelect[] playerSelectors;
    public GameObject StartLabel;

    private bool _canStartGame;

    private void Update()
    {
        StartLabel.SetActive(false);
        _canStartGame = false;
        foreach (var playerSelect in playerSelectors)
        {
            if (playerSelect.Joined)
            {
                StartLabel.SetActive(true);
                _canStartGame = true;
                break;
            }
        }   
    }

    public void StartGame()
    {
        if (_canStartGame)
        {
            gameObject.SetActive(false);

            var inputDevices = new InputDevice[4];
            for (int i = 0; i < playerSelectors.Length; i++)
            {
                var playerSelect = playerSelectors[i];
                if (playerSelect.Joined)
                {
                    inputDevices[i] = playerSelect.GetInputDevice();
                }
                else
                {
                    inputDevices[i] = null;
                }
            }

            gameManager.StartGame(inputDevices);
        }
    }
}
