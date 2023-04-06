using Herohunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float paddingX;

    [SerializeField] float paddingY;

    [SerializeField] float moveSpeed = 2f;

    [SerializeField] float moveRotationAngle = 25f;

    private void OnEnable()
    {
        StartCoroutine(nameof(RandomMovingCoroutine));
    }

    IEnumerator RandomMovingCoroutine()
    {
        transform.position = Viewport.Instance.RandomEnemySpawnPosition(paddingX, paddingY);

        Vector3 targetPosition = Viewport.Instance.RandomTopHalfPosition(paddingX, paddingY);

        Quaternion Rotation = Quaternion.Euler(moveRotationAngle, -90 , 90);

        while (gameObject.activeSelf)
        {
            // 如果敵人還沒到達目標位置
            if(Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
            {
                // 繼續前往目標位置
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                // 取得Y軸讓敵人移動旋轉
                transform.rotation = Quaternion.AngleAxis((targetPosition - transform.position).normalized.x * moveRotationAngle, Vector3.up);
            }
            else
            {
                // 如果到達就給它新的目標位置
                targetPosition = Viewport.Instance.RandomTopHalfPosition(paddingX, paddingY);
            }

            yield return null;
        }
    }
}
