using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    // 取得子彈軌跡組件
    TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnDisable()
    {
        trail.Clear();
    }
}
