using InControl;
using System.Collections;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public float normalMoveSpeed;
    public float slowMoveSpeed;

    public Missile missilePrefab;
    public GameObject bazooka;
    private SpriteRenderer bazookaSprite;

    private float _currentMoveSpeed;

    private Rigidbody2D _rigidBody;
    private Missile _activeMissile;
    private bool _triggerHeld = false;

    private InputDevice _inputDevice;

    void Awake() {
        _currentMoveSpeed = normalMoveSpeed;
        _rigidBody = GetComponent<Rigidbody2D>();
        bazookaSprite = bazooka.GetComponent<SpriteRenderer>();
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
        Vector3 leftStickInputDir = _inputDevice.LeftStick;
        Vector3 rightStickInputDir = _inputDevice.RightStick;
        _rigidBody.velocity = leftStickInputDir * _currentMoveSpeed;

        if (_activeMissile == null)
        {
            if (!bazookaSprite.enabled)
            {
                bazookaSprite.enabled = true;
            }

            if (rightStickInputDir.magnitude > 0)
            {
                var rightStickRotation = Quaternion.LookRotation(rightStickInputDir);
                bazooka.transform.rotation = new Quaternion(0f, 0f, rightStickRotation.z, rightStickRotation.w);
            }
        }

        if (_inputDevice.GetControl(InputControlType.RightTrigger) > 0)
        {
            if (_triggerHeld == false)
            {
                if (_activeMissile == null)
                {
                    StartCoroutine(MoveSlow());
                    _activeMissile = Instantiate(missilePrefab, transform.position, Quaternion.FromToRotation(Vector3.up, rightStickInputDir));
                    _activeMissile.inputDevice = _inputDevice;

                    _activeMissile.gameObject.layer = gameObject.layer;

                    var spriteRenderer = GetComponent<SpriteRenderer>();
                    var missileSpriteRenderer = _activeMissile.GetComponent<SpriteRenderer>();
                    missileSpriteRenderer.color = spriteRenderer.color;

                    bazookaSprite.enabled = false;
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
