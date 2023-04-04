using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ��H�� �}�C ���a�l�u��H��;
    [SerializeField, Header("���a�l�u��H��")]
    Pool[] playerProjectilePools;

    private void Start()
    {
        Initialize(playerProjectilePools);
    }

    // ��l��k(�� �}�C �ѼƦW)
    private void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;

            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }
}
