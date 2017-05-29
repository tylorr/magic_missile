using InControl;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

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

    private Stack<Missile> _specialMissiles;

    void Awake() {
        _currentMoveSpeed = normalMoveSpeed;
        _rigidBody = GetComponent<Rigidbody2D>();
        _bazookaSprite = bazooka.GetComponent<SpriteRenderer>();
        _specialMissiles = new Stack<Missile>();
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

    public void AddSpecialMissiles(Missile missilePrefab, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            _specialMissiles.Push(missilePrefab);
        }
    }

    private Missile GetNextMissilePrefab()
    {
        if (_specialMissiles.Count > 0)
        {
            return _specialMissiles.Pop();
        }

        return missilePrefab;
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

                    _activeMissile = Instantiate(GetNextMissilePrefab(), transform.position, Quaternion.FromToRotation(Vector3.up, rightStickInputDir));
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
            if (_triggerHeld == true && _activeMissile && _activeMissile.IsBoosting)
            {
                _activeMissile.GetComponent<ExplodeOnContact>().Explode();
            }
            _triggerHeld = false;
        }
    }

    public void OnExplosionHit()
    {
        Destroy(gameObject);
    }

    IEnumerator MoveSlow()
    {
        _currentMoveSpeed = slowMoveSpeed;
        yield return new WaitForSeconds(1);
        _currentMoveSpeed = normalMoveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var powerup = collider.GetComponent<Powerup>();
        if (powerup == null) return;

        powerup.Apply(this);

        Destroy(powerup.gameObject);
    }
}
