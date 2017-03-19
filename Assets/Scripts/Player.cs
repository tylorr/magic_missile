using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float normalMoveSpeed;
    public float slowMoveSpeed;
    public int inputDeviceIndex;

    private float currentMoveSpeed;
    
    public Missile missilePrefab;

    private Rigidbody2D rigidBody;
    private Missile activeMissile;
    private bool triggerHeld = false;

    private InputDevice inputDevice;

    void Awake() {
        currentMoveSpeed = normalMoveSpeed;
        inputDevice = InputManager.Devices[inputDeviceIndex];
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (inputDevice.GetControl(InputControlType.Start))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate() {
        Vector3 inputDir = inputDevice.LeftStick;
        rigidBody.velocity = inputDir * currentMoveSpeed;

        if (inputDevice.GetControl(InputControlType.RightTrigger) > 0)
        {
            if (triggerHeld == false)
            {
                if (activeMissile == null)
                {
                    StartCoroutine(MoveSlow());
                    activeMissile = Instantiate(missilePrefab, transform.position, Quaternion.FromToRotation(Vector3.up, inputDevice.RightStick));
                    activeMissile.inputDevice = inputDevice;

                    activeMissile.gameObject.layer = gameObject.layer;

                    var spriteRenderer = GetComponent<SpriteRenderer>();
                    var missileSpriteRenderer = activeMissile.GetComponent<SpriteRenderer>();
                    missileSpriteRenderer.color = spriteRenderer.color;
                }
                else
                {
                    Destroy(activeMissile.gameObject);
                }

                triggerHeld = true;
            }
        }
        else
        {
            triggerHeld = false;
        }
    }

    IEnumerator MoveSlow()
    {
        currentMoveSpeed = slowMoveSpeed;
        yield return new WaitForSeconds(1);
        currentMoveSpeed = normalMoveSpeed;
    }
}
