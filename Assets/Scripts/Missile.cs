using UnityEngine;

public class Missile : MonoBehaviour
{
    public float thrust;
    public float controlThrust;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.up * thrust);

        float vertInput = Input.GetAxis("Right Stick Vertical");
        float horzInput = Input.GetAxis("Right Stick Horizontal");

        Vector3 inputDir = new Vector3(horzInput, vertInput);
        Vector3 controlDir = Vector3.Reflect(inputDir, transform.right);

        if (Vector3.Distance(-transform.up, controlDir) <= 0.3f)
        {
            controlDir = -transform.up;
        }

        Vector3 forcePosition = transform.position + (-transform.up * 0.5f);

        rb.AddForceAtPosition(controlDir * controlThrust, forcePosition);
    }
}