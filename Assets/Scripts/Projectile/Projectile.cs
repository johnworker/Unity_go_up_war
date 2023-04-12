using System.Collections;
using UnityEngine;

namespace Herohunk
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        GameObject hitVFX;
        [SerializeField]
        float damage;

        [SerializeField, Header("�l�u�t��")]
        float moveSpeed = 10f;
        [SerializeField, Header("�l�u���ʤ�V")]
        protected Vector2 moveDirection;

        protected GameObject target;

        protected virtual void OnEnable()
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

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<Character>(out Character character))
            {
                character.TakeDamage(damage);

                // var contactPoint = collision.GetContact(0);
                // PoolManager.Release(hitVFX, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
                // ���X���Y�g    // ����BUG
                PoolManager.Release(hitVFX, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal), transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
