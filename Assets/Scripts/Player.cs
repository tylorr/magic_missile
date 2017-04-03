using InControl;
using System.Collections;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public float normalMoveSpeed;
    public float slowMoveSpeed;

    public Missile missilePrefab;

    private float _currentMoveSpeed;

    private Rigidbody2D _rigidBody;
    private Missile _activeMissile;
    private bool _triggerHeld = false;

    private InputDevice _inputDevice;

    void Awake() {
        _currentMoveSpeed = normalMoveSpeed;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void SetInputDevice(InputDevice inputDevice)
    {
        _inputDevice = inputDevice;
    }

    void FixedUpdate() {
        Vector3 inputDir = _inputDevice.LeftStick;
        _rigidBody.velocity = inputDir * _currentMoveSpeed;

        if (_inputDevice.GetControl(InputControlType.RightTrigger) > 0)
        {
            if (_triggerHeld == false)
            {
                if (_activeMissile == null)
                {
                    StartCoroutine(MoveSlow());
                    _activeMissile = Instantiate(missilePrefab, transform.position, Quaternion.FromToRotation(Vector3.up, _inputDevice.RightStick));
                    _activeMissile.inputDevice = _inputDevice;

                    _activeMissile.gameObject.layer = gameObject.layer;

                    var spriteRenderer = GetComponent<SpriteRenderer>();
                    var missileSpriteRenderer = _activeMissile.GetComponent<SpriteRenderer>();
                    missileSpriteRenderer.color = spriteRenderer.color;
                }
                else
                {
                    Destroy(_activeMissile.gameObject);
                }

                _triggerHeld = true;
            }
        }
        else
        {
            _triggerHeld = false;
        }
    }

    IEnumerator MoveSlow()
    {
        _currentMoveSpeed = slowMoveSpeed;
        yield return new WaitForSeconds(1);
        _currentMoveSpeed = normalMoveSpeed;
    }
}
