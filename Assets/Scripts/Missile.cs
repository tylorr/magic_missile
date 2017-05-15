using InControl;
using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool isBoosting = false;

    public float thrust;
    public float controlThrust;
    public float boostThrustMult;
    public float boostControlThrustMult;
    public Explosion explosionPrefab;
    
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

    public void Explode()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        var playerTargets = explosion.Explode(gameObject.layer);
        foreach (var playerTarget in playerTargets)
        {
            playerTarget.OnExplosionHit(this);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherLayer = collision.collider.gameObject.layer;
        var player = collision.collider.gameObject.GetComponent<Player>();
        if (player)
        {
            player.OnMissileHit(this);
        }

        Explode();
    }

    public void Boost()
    {
        thrust *= boostThrustMult;
        controlThrust *= boostControlThrustMult;
        isBoosting = true;
    }
}
