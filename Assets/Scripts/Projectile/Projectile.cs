using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField, Header("子彈速度")]
        float moveSpeed = 10f;
        [SerializeField, Header("子彈移動方向")]
        protected Vector2 moveDirection;

        private void OnEnable()
        {
            StartCoroutine(MoveDirectly());
        }

        IEnumerator MoveDirectly()
        {
            while (gameObject.activeSelf)
            {
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
