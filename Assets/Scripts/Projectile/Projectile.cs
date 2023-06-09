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

        [SerializeField, Header("子彈速度")]
        float moveSpeed = 10f;
        [SerializeField, Header("子彈移動方向")]
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
                // ↓合併縮寫    // ↓有BUG
                PoolManager.Release(hitVFX, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal), transform.localPosition);
                gameObject.SetActive(false);
            }
        }
    }
}
