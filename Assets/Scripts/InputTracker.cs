using InControl;
using UnityEngine;

public class InputTracker : MonoBehaviour
{
    public PlayerSelectController playerSelectController;

    private InputDevice[] _inputDevices;

    private void Start()
    {
        _inputDevices = new InputDevice[4];

        for (int i = 0; i < _inputDevices.Length; i++)
        {
            if (i < InputManager.Devices.Count)
            {
                _inputDevices[i] = InputManager.Devices[i];
                playerSelectController.playerSelectors[i].SetInputDevice(_inputDevices[i]);
            }
            else
            {
                break;
            }
        }

        InputManager.OnDeviceAttached += OnDeviceAttached;
        InputManager.OnDeviceDetached += OnDeviceDetached;
    }

    private void OnDeviceAttached(InputDevice inputDevice)
    {
        for (int i = 0; i < _inputDevices.Length; i++)
        {
            if (_inputDevices[i] == null)
            {
                _inputDevices[i] = inputDevice;
                playerSelectController.playerSelectors[i].SetInputDevice(_inputDevices[i]);
            }
        }
    }

    private void OnDeviceDetached(InputDevice inputDevice)
    {
        for (int i = 0; i < _inputDevices.Length; i++)
        {
            if (_inputDevices[i] == inputDevice)
            {
                _inputDevices[i] = null;
                playerSelectController.playerSelectors[i].SetInputDevice(null);
            }
        }
    }
}
