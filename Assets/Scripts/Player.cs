using InControl;
using System.Collections;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public float normalMoveSpeed;
    public float slowMoveSpeed;

    public Missile missilePrefab;
    public GameObject bazooka;

    private SpriteRenderer _bazookaSprite;

    private float _currentMoveSpeed;

    private Rigidbody2D _rigidBody;
    private Missile _activeMissile;
    private bool _triggerHeld = false;

    private InputDevice _inputDevice;
    private bool _tankControls;

    void Awake() {
        _currentMoveSpeed = normalMoveSpeed;
        _rigidBody = GetComponent<Rigidbody2D>();
        _bazookaSprite = bazooka.GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void SetTankControls(bool tankControls)
    {
        _tankControls = tankControls;
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
            if (!_bazookaSprite.enabled)
            {
                _bazookaSprite.enabled = true;
            }

            if (rightStickInputDir.magnitude > 0)
            {
                float angle = Mathf.Atan2(rightStickInputDir.y, rightStickInputDir.x) * Mathf.Rad2Deg;
                bazooka.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

                    _activeMissile.TankControls = _tankControls;

                    _bazookaSprite.enabled = false;
                }
                else
                {
                    _activeMissile.Boost();
                }

                _triggerHeld = true;
            }
        }
        else
        {
            if (_triggerHeld == true && _activeMissile && _activeMissile.isBoosting)
            {
                _activeMissile.Explode();
            }
            _triggerHeld = false;
        }
    }

    public void OnExplosionHit(Missile missile)
    {
        Destroy(gameObject);
    }

    public void OnMissileHit(Missile missile)
    {
        Destroy(gameObject);
    }

    IEnumerator MoveSlow()
    {
        _currentMoveSpeed = slowMoveSpeed;
        yield return new WaitForSeconds(1);
        _currentMoveSpeed = normalMoveSpeed;
    }
}
