using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class EnemyProjectile : Projectile
    {
        private void Awake()
        {
            // ��l�u���O���U��
            if(moveDirection != Vector2.down)
            {
                // �|�줸.�ھڶ}�l�P������V��^�@�ӱ����(�}�l��V,������V);
                transform.rotation = Quaternion.FromToRotation(Vector2.down, moveDirection);
            }
        }
    }
}
