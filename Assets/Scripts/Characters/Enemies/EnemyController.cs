using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class EnemyController : MonoBehaviour
    {
        [Header("---- 移動 ----")]

        [SerializeField, Header("內距X")] float paddingX;

        [SerializeField, Header("內距Y")] float paddingY;

        [SerializeField, Header("移動速度")] float moveSpeed = 2f;

        [SerializeField, Header("移動旋轉角度")] float moveRotationAngle = 25f;

        [Header("---- 開火 ----")]

        [SerializeField, Header("敵人發射多個子彈")] GameObject[] projectiles;

        [SerializeField, Header("開火位置")] Transform muzzle;

        [SerializeField, Header("最小開火間格時間")] float minFireInterval;

        [SerializeField, Header("最大開火間格時間")] float maxFireInterval;

        private void OnEnable()
        {
            StartCoroutine(nameof(RandomMovingCoroutine));
            StartCoroutine(nameof(RandomlyFireCoroutine));
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        // 敵人隨機移動協程
        IEnumerator RandomMovingCoroutine()
        {
            transform.position = Viewport.Instance.RandomEnemySpawnPosition(paddingX, paddingY);

            Vector3 targetPosition = Viewport.Instance.RandomTopHalfPosition(paddingX, paddingY);

            while (gameObject.activeSelf)
            {
                // 如果敵人還沒到達目標位置
                if (Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
                {
                    // 繼續前往目標位置
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    // 取得Y軸讓敵人移動旋轉
                    transform.rotation = Quaternion.Euler(0, -90 + ((targetPosition - transform.position).y * moveRotationAngle), 90);
                    // transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).x * moveRotationAngle, Vector3.up);
                }
                else
                {
                    // 如果到達就給它新的目標位置
                    targetPosition = Viewport.Instance.RandomTopHalfPosition(paddingX, paddingY);
                }

                yield return null;
            }
        }

        // 敵人隨機開火協程
        IEnumerator RandomlyFireCoroutine()
        {
            // WaitForSeconds waitForRandomFireInterval = new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));

            // 當(敵人對象.活動狀態)
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
