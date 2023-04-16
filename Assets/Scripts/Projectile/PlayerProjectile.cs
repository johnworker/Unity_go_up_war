using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class PlayerProjectile : Projectile
    {
        // 取得子彈軌跡組件
        TrailRenderer trail;

        private void Awake()
        {
            trail = GetComponentInChildren<TrailRenderer>();

            // 當子彈不是往下飛
            if (moveDirection != Vector2.up)
            {
                // 四位元.根據開始與結束方向返回一個旋轉值(開始方向,結束方向);
                transform.rotation = Quaternion.FromToRotation(Vector2.up, moveDirection);
            }

        }

        private void OnDisable()
        {
            trail.Clear();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            PlayerEnergy.Instance.Obtain(PlayerEnergy.PERCENT);
        }
    }
}
