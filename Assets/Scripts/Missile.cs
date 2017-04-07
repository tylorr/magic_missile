using InControl;
using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float thrust;
    public float controlThrust;
    private Rigidbody2D rigidBody;

    [HideInInspector]
    public InputDevice inputDevice;

    private bool inputActive = false;

    public bool TankControls { get; set; }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(InputDelay());
    }

    IEnumerator InputDelay()
    {
        inputActive = false;
        yield return new WaitForSeconds(0.25f);
        inputActive = true;
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(transform.up * thrust);

        if (inputActive)
        {
            Vector3 inputDir;
            if (TankControls)
            {
                inputDir = transform.TransformDirection(inputDevice.RightStick);
            }
            else
            {
                inputDir = inputDevice.RightStick;
            }
            Vector3 controlDir = Vector3.Reflect(inputDir, transform.right);

            if (Vector3.Distance(-transform.up, controlDir) <= 0.3f)
            {
                controlDir = -transform.up;
            }

            Vector3 forcePosition = transform.position + (-transform.up * 0.5f);
            rigidBody.AddForceAtPosition(controlDir * controlThrust, forcePosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherLayer = collision.collider.gameObject.layer;
        if (otherLayer != GameLayers.Environment)
        {
            Destroy(collision.collider.gameObject);
        }
        Destroy(gameObject);
    }
}
