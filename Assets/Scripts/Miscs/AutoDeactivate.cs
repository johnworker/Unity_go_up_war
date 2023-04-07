using System.Collections;
using UnityEngine;

namespace Herohunk
{
    public class AutoDeactivate : MonoBehaviour
    {
        [SerializeField, Header("�R������")]
        bool destoryGameObject;

        [SerializeField, Header("�ͩR�g���ɶ�")]
        float lifetime = 3f;

        [Header("���ݥͩR�g��")]
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
