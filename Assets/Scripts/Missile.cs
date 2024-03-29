using InControl;
using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool IsBoosting { get; private set; }

    public float thrust;
    public float controlThrust;
    public float boostThrustMult;
    public float boostControlThrustMult;

    public float collisionMagnitudeThreshold;
    
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

    public void Boost()
    {
        thrust *= boostThrustMult;
        controlThrust *= boostControlThrustMult;
        IsBoosting = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GetComponent<Exploder>().Explode();
            return;
        }

        float maxMagnitude = 0.0f;
        foreach (var contact in collision.contacts)
        {
            var collisionMagnitude = Vector2.Dot(contact.relativeVelocity, contact.normal);
            if (collisionMagnitude > maxMagnitude)
            {
                maxMagnitude = collisionMagnitude;
            }
        }

        if (maxMagnitude > collisionMagnitudeThreshold)
        {
            GetComponent<Exploder>().Explode();
        }
    }
}
