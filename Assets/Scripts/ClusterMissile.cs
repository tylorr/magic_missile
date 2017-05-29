using InControl;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Missile))]
public class ClusterMissile : MonoBehaviour
{
    public int count;
    public float distance;
    public ClusterProjectile clusterPrefab;

    //private Missile _missile;

    void Awake()
    {
        //_missile = GetComponent<Missile>();
    }

    void OnDestroy()
    {
        var angle = Quaternion.AngleAxis(360 / count, Vector3.forward);
        var rotation = transform.rotation;
        for (int i = 0; i < count; i++)
        {
            var projectile = Instantiate(clusterPrefab, transform.position, rotation);
            projectile.gameObject.layer = gameObject.layer;
            rotation = rotation * angle;
        }
    }
}
