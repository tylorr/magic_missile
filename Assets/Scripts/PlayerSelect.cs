using InControl;
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

    public bool Joined { get; private set; }

    private UIActions _uiActions;

    private void Awake()
    {
        _uiActions = new UIActions();
        SetJoined(false);
        SetInputDevice(null);
    }

    public void SetInputDevice(InputDevice inputDevice)
    {
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
    }

    public InputDevice GetInputDevice()
    {
        return _uiActions.Device;
    }

    public void Update()
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
    }
}
