using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class Character : MonoBehaviour
    {
        [SerializeField, Header("死亡特效")] GameObject deathVFX;

        [Header("--- 生命值 ---")]

        [SerializeField, Header("最大生命值")] protected float maxHealth;

        [SerializeField, Header("頭上生命血條是否顯示")] bool showOnHeadHealthBar = true;

        [SerializeField, Header("頭上生命血條")] StatsBar onHeadHealthBar;

        [Header("當前生命值")] protected float health;

        protected virtual void OnEnable()
        {
            health = maxHealth;
        }

        public void ShowOnHeadHealthBar()
        {
            onHeadHealthBar.gameObject.SetActive(true);
        }
        public void HideOnHeadHealthBar()
        {

        }

        public virtual void TakeDamage(float damage)
        {
            health -= damage;

            if(health <= 0f)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            health = 0f;
            // ↓有BUG
            PoolManager.Release(deathVFX, transform.position, Quaternion.identity, transform.position);
            gameObject.SetActive(false);
        }

        public virtual void RestoreHealth(float value)
        {
            if (health == maxHealth) return;

            // health += value;
            // health = Mathf.Clamp(health, 0f, maxHealth);

            // 將上方註釋代碼合併 (此句目的是為了防止溢出)
            health = Mathf.Clamp(health + value, 0f, maxHealth);
        }

        // 定義一段時間會持續恢復角色生命值的協程
        protected IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime, float percent)
        {
            while(health < maxHealth)
            {
                yield return waitTime;

                RestoreHealth(maxHealth * percent);
            }
        }
        // 定義一段時間會持續受傷角色生命值的協程
        protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)
        {
            while(health > 0f)
            {
                yield return waitTime;

                TakeDamage(maxHealth * percent);
            }
        }
    }
}
