using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class EnemyProjectile : Projectile
    {
        private void Awake()
        {
            // 當子彈不是往下飛
            if(moveDirection != Vector2.down)
            {
                // 四位元.根據開始與結束方向返回一個旋轉值(開始方向,結束方向);
                transform.rotation = Quaternion.FromToRotation(Vector2.down, moveDirection);
            }
        }
    }
}
