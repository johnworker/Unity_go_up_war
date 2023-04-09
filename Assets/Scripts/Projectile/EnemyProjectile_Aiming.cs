using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class EnemyProjectile_Aiming : Projectile
    {
        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        protected override void OnEnable()
        {
            StartCoroutine(nameof(MoveDirectionCoroutine));
            base.OnEnable();
        }

        // 移動方向協程
        IEnumerator MoveDirectionCoroutine()
        {
            yield return null;

            if (target.activeSelf)
            {
                moveDirection = (target.transform.position - transform.position).normalized;
            }
        }
    }
}
