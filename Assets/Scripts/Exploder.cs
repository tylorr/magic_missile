﻿using UnityEngine;

public class Exploder : MonoBehaviour
{
    public Explosion explosionPrefab;

    public virtual void Explode()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        var playerTargets = explosion.Explode(gameObject.layer);
        foreach (var playerTarget in playerTargets)
        {
            playerTarget.OnExplosionHit(GetComponent<Owner>().player);
        }

        Destroy(gameObject);
    }
}
