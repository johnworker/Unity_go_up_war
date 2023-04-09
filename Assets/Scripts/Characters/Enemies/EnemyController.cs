using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class EnemyController : MonoBehaviour
    {
        [Header("---- ���� ----")]

        [SerializeField, Header("���ZX")] float paddingX;

        [SerializeField, Header("���ZY")] float paddingY;

        [SerializeField, Header("���ʳt��")] float moveSpeed = 2f;

        [SerializeField, Header("���ʱ��ਤ��")] float moveRotationAngle = 25f;

        [Header("---- �}�� ----")]

        [SerializeField, Header("�ĤH�o�g�h�Ӥl�u")] GameObject[] projectiles;

        [SerializeField, Header("�}����m")] Transform muzzle;

        [SerializeField, Header("�̤p�}������ɶ�")] float minFireInterval;

        [SerializeField, Header("�̤j�}������ɶ�")] float maxFireInterval;

        private void OnEnable()
        {
            StartCoroutine(nameof(RandomMovingCoroutine));
            StartCoroutine(nameof(RandomlyFireCoroutine));
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        // �ĤH�H�����ʨ�{
        IEnumerator RandomMovingCoroutine()
        {
            transform.position = Viewport.Instance.RandomEnemySpawnPosition(paddingX, paddingY);

            Vector3 targetPosition = Viewport.Instance.RandomTopHalfPosition(paddingX, paddingY);

            while (gameObject.activeSelf)
            {
                // �p�G�ĤH�٨S��F�ؼЦ�m
                if (Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
                {
                    // �~��e���ؼЦ�m
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    // ���oY�b���ĤH���ʱ���
                    transform.rotation = Quaternion.Euler(0, -90 + ((targetPosition - transform.position).y * moveRotationAngle), 90);
                    // transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).x * moveRotationAngle, Vector3.up);
                }
                else
                {
                    // �p�G��F�N�����s���ؼЦ�m
                    targetPosition = Viewport.Instance.RandomTopHalfPosition(paddingX, paddingY);
                }

                yield return null;
            }
        }

        // �ĤH�H���}����{
        IEnumerator RandomlyFireCoroutine()
        {
            // WaitForSeconds waitForRandomFireInterval = new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));

            // ��(�ĤH��H.���ʪ��A)
            while (gameObject.activeSelf)
            {
                // yield return waitForRandomFireInterval;

                yield return new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));

                foreach (var projectile in projectiles)
                {
                    PoolManager.Release(projectile, muzzle.position, Quaternion.identity, muzzle.localPosition);
                }
            }
        }
    }
}
