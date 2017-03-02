using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed;

    public GameObject missilePrefab;

    private Rigidbody2D rb;
    private GameObject activeMissile;
    private bool triggerHeld = false;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        float vertInput = Input.GetAxis("Left Stick Vertical");
        float horzInput = Input.GetAxis("Left Stick Horizontal");

        Vector3 inputDir = new Vector3(horzInput, vertInput);

        rb.velocity = inputDir * moveSpeed;

        if (Input.GetAxis("Right Trigger") > 0)
        {
            if (triggerHeld == false)
            {
                if (activeMissile == null)
                {
                    activeMissile = Instantiate(missilePrefab, transform.position, Quaternion.identity) as GameObject;
                }
                else
                {
                    Destroy(activeMissile);
                }

                triggerHeld = true;
            }
        }
        else
        {
            triggerHeld = false;
        }

    }
}
