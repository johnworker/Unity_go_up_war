using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField, Header("�l�u�t��")]
        float moveSpeed = 10f;
        [SerializeField, Header("�l�u���ʤ�V")]
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
