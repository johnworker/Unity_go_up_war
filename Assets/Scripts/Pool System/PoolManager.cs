using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 對象池 陣列 玩家子彈對象池;
    [SerializeField, Header("玩家子彈對象池")]
    Pool[] playerProjectilePools;

    private void Start()
    {
        Initialize(playerProjectilePools);
    }

    // 初始方法(池 陣列 參數名)
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
