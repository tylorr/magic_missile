using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
                GetComponent<ExplodeOnContact>().Explode();
            }
            yield return null;
        }
	}
}
