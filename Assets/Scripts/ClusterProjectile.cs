using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Exploder))]
public class ClusterProjectile : MonoBehaviour
{
    public float thrust;
    public float explodeDistance;

    IEnumerator Start() {
        var startPosition = transform.position;
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(transform.up * thrust);

        while (true)
        {
            var distance = Vector2.Distance(transform.position, startPosition);
            if (distance >= explodeDistance)
            {
                GetComponent<Exploder>().Explode();
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Exploder>().Explode();
    }
}
