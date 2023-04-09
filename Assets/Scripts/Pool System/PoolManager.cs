using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class PoolManager : MonoBehaviour
    {
        // 對象池 陣列 玩家子彈對象池;
        [SerializeField, Header("玩家子彈對象池")]
        Pool[] playerProjectilePools;
        [SerializeField, Header("敵人子彈對象池")]
        Pool[] enemyProjectilePools;

        // 字典<預置物, 字典的值> 名字;
        static Dictionary<GameObject, Pool> dictionary;

        private void Start()
        {
            dictionary = new Dictionary<GameObject, Pool>();

            Initialize(playerProjectilePools);
            Initialize(enemyProjectilePools);
        }

#if UNITY_EDITOR
        private void OnDestroy()
        {
            CheckPoolSize(playerProjectilePools);
            CheckPoolSize(enemyProjectilePools);
        }
#endif

        // 檢測對象池運行函數
        private void CheckPoolSize(Pool[] pools)
        {
            foreach (var pool in pools)
            {
                if (pool.RuntimeSize > pool.Size)
                {
                    Debug.LogWarning(
                    string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}"!,
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
                }
            }
        }

        internal static void Release(GameObject hitVFX, Vector2 point, Quaternion quaternion)
        {
            throw new NotImplementedException();
        }

        // 初始方法(池 陣列 參數名)
        private void Initialize(Pool[] pools)
        {
            foreach (var pool in pools)
            {
#if UNITY_EDITOR
                // ContainsKey函數功能：判斷是否包含指定的鍵
                if (dictionary.ContainsKey(pool.Prefab))
                {
                    Debug.LogError("Same prefab in mutiple pools! Prefab:" + pool.Prefab.name);

                    continue;
                }
#endif
                // Add函數功能：為字典添加 鍵和值
                dictionary.Add(pool.Prefab, pool);

                Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;

                poolParent.parent = transform;

                pool.Initialize(poolParent);
            }
        }

        internal static void Release(GameObject projectile, Vector3 position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <para> Return a specified <paramref name="prefab"></paramref> gameObject in the pool </para>
        /// <para> 根據傳入的 <paramref name="prefab"></paramref> 參數, 返回對象池中預備好的遊戲對象 </para>
        /// </summary>
        /// <param name="prefab">
        /// <para> Specified gameObject prefab. </para>
        /// <para> 指定的遊戲對象預置體 </para>
        /// </param>
        /// <param name="position">
        /// <para> Specified release position. </para>
        /// <para> 指定釋放位置 </para>
        /// </param>
        /// <param name="prefab">
        /// <para> Specified rotation. </para>
        /// <para> 指定的旋轉值 </para>
        /// </param>
        /// <param name="prefab">
        /// <para> Specified scale. </para>
        /// <para> 指定的縮放值 </para>
        /// </param>
        /// <returns>
        /// <para> Prepared gameObject in the pool. </para>
        /// <para> 對象池中預備好的遊戲對象 </para>
        /// </returns>
        // 公開 靜態函式 返回遊戲物件 名稱()
        public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
        {
#if UNITY_EDITOR
            if (!dictionary.ContainsKey(prefab))
            {
                Debug.LogError("Pool Manager could NOT find prefab:" + prefab.name);
                return null;
            }
#endif
            return dictionary[prefab].PreparedObject(position, rotation, localScale);
        }
    }
}
