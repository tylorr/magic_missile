using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public Missile missilePrefab;

    public void Apply(Player player)
    {
        player.AddSpecialMissiles(missilePrefab, 2);
    }
}
