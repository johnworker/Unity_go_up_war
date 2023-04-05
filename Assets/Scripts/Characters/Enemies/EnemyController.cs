using Herohunk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float paddingX;

    [SerializeField] float paddingY;

    [SerializeField] float moveSpeed = 2f;

    private void OnEnable()
    {
        StartCoroutine(nameof(RandomMovingCoroutine));
    }

    IEnumerator RandomMovingCoroutine()
    {
        transform.position = Viewport.Instance.RandomEnemySpawnPosition(paddingX, paddingY);

        Vector3 targetPosition = Viewport.Instance.RandomRightHalfPosition(paddingX, paddingY);

        while (gameObject.activeSelf)
        {
            // �p�G�ĤH�٨S��F�ؼЦ�m
            if(Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
            {
                // �~��e���ؼЦ�m
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                // �p�G��F�N�����s���ؼЦ�m
                targetPosition = Viewport.Instance.RandomRightHalfPosition(paddingX, paddingY);
            }

            yield return null;
        }
    }
}
