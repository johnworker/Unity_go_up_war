using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class PlayerProjectile : Projectile
    {
        // ���o�l�u�y��ե�
        TrailRenderer trail;

        private void Awake()
        {
            trail = GetComponentInChildren<TrailRenderer>();

            // ��l�u���O���U��
            if (moveDirection != Vector2.up)
            {
                // �|�줸.�ھڶ}�l�P������V��^�@�ӱ����(�}�l��V,������V);
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
