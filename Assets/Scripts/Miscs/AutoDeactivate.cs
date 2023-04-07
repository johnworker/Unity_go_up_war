using System.Collections;
using UnityEngine;

namespace Herohunk
{
    public class AutoDeactivate : MonoBehaviour
    {
        [SerializeField, Header("摧毀物件")]
        bool destoryGameObject;

        [SerializeField, Header("生命週期時間")]
        float lifetime = 3f;

        [Header("等待生命週期")]
        WaitForSeconds waitLifetime;

        private void Awake()
        {
            waitLifetime = new WaitForSeconds(lifetime);
        }

        private void OnEnable()
        {
            StartCoroutine(DeactivateCoroutine());
        }

        IEnumerator DeactivateCoroutine()
        {
            yield return waitLifetime;

            if (destoryGameObject)
            {
                Destroy(gameObject);
            }

            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
