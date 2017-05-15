using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(CircleCollider2D))]
public class Explosion : MonoBehaviour
{
    CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public IEnumerable<Player> Explode(int owningPlayerLayer)
    {
        StartCoroutine(AnimateExplosion(0.5f));
        float radius = _collider.radius * transform.localScale.x;
        var colliders = Physics2D.OverlapCircleAll(transform.position, radius, GameLayers.AllPlayers() & ~(1 << owningPlayerLayer));
        return colliders.Select((x) => x.GetComponent<Player>()).Where((x) => x != null);
    }

    private IEnumerator AnimateExplosion(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
