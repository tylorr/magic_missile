using InControl;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public PlayerSelectController playerSelectController;

    public GameObject content;
    public GameObject noDeviceLabel;

    public Image icon;
    public GameObject joinLabel;
    public GameObject leaveLabel;
    public GameObject tankModeUI;
    public Text tankModeLabel;

    public bool Joined { get; private set; }
    public bool TankControls { get; private set; }

    private UIActions _uiActions;

    private void Awake()
    {
        TankControls = false;
        Joined = false;

        _uiActions = new UIActions();
        SetJoined(false);
        SetInputDevice(null);
        UpdateTankControlLabel();
    }

    public void SetInputDevice(InputDevice inputDevice)
    {
        if (inputDevice != null)
        {
            StartCoroutine(WaitForInput());
        }
        else
        {
            StopAllCoroutines();
        }

        content.SetActive(inputDevice != null);
        noDeviceLabel.SetActive(inputDevice == null);
        _uiActions.Device = inputDevice;
    }

    private static Color IntColor(int r, int g, int b, int alpha)
    {
        return new Color(r / 255f, g / 255f, b / 255f, alpha / 255f);
    }

    private void SetJoined(bool joined)
    {
        Joined = joined;

        icon.color = joined ? Color.white : IntColor(82, 82, 82, 213);
        joinLabel.SetActive(!joined);
        leaveLabel.SetActive(joined);

        tankModeUI.SetActive(joined);
    }

    public InputDevice GetInputDevice()
    {
        return _uiActions.Device;
    }

    public IEnumerator WaitForInput()
    {
        while (true)
        {
            if (Joined)
            {
                if (_uiActions.Back.WasPressed)
                {
                    SetJoined(false);
                }
            }
            else
            {
                if (_uiActions.Select.WasPressed)
                {
                    SetJoined(true);
                }
            }

            if (_uiActions.Start.WasPressed)
            {
                playerSelectController.StartGame();
            }

            if (_uiActions.Toggle.WasPressed)
            {
                TankControls = !TankControls;
                UpdateTankControlLabel();
            }

            yield return null;
        }
    }

    private void UpdateTankControlLabel()
    {
        tankModeLabel.text = "Tank controls: " + (TankControls ? "ON" : "OFF");
    }
}
