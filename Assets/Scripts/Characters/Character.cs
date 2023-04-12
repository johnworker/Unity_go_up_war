using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class Character : MonoBehaviour
    {
        [SerializeField, Header("���`�S��")] GameObject deathVFX;

        [Header("--- �ͩR�� ---")]

        [SerializeField, Header("�̤j�ͩR��")] protected float maxHealth;

        [SerializeField, Header("�Y�W�ͩR����O�_���")] bool showOnHeadHealthBar = true;

        [SerializeField, Header("�Y�W�ͩR���")] StatsBar onHeadHealthBar;

        [Header("��e�ͩR��")] protected float health;

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
            // ����BUG
            PoolManager.Release(deathVFX, transform.position, Quaternion.identity, transform.position);
            gameObject.SetActive(false);
        }

        public virtual void RestoreHealth(float value)
        {
            if (health == maxHealth) return;

            // health += value;
            // health = Mathf.Clamp(health, 0f, maxHealth);

            // �N�W������N�X�X�� (���y�ت��O���F����X)
            health = Mathf.Clamp(health + value, 0f, maxHealth);
        }

        // �w�q�@�q�ɶ��|�����_����ͩR�Ȫ���{
        protected IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime, float percent)
        {
            while(health < maxHealth)
            {
                yield return waitTime;

                RestoreHealth(maxHealth * percent);
            }
        }
        // �w�q�@�q�ɶ��|������˨���ͩR�Ȫ���{
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
