using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class PoolManager : MonoBehaviour
    {
        // ��H�� �}�C ���a�l�u��H��;
        [SerializeField, Header("���a�l�u��H��")]
        Pool[] playerProjectilePools;
        [SerializeField, Header("�ĤH�l�u��H��")]
        Pool[] enemyProjectilePools;

        // �r��<�w�m��, �r�媺��> �W�r;
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

        // �˴���H���B����
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

        // ��l��k(�� �}�C �ѼƦW)
        private void Initialize(Pool[] pools)
        {
            foreach (var pool in pools)
            {
#if UNITY_EDITOR
                // ContainsKey��ƥ\��G�P�_�O�_�]�t���w����
                if (dictionary.ContainsKey(pool.Prefab))
                {
                    Debug.LogError("Same prefab in mutiple pools! Prefab:" + pool.Prefab.name);

                    continue;
                }
#endif
                // Add��ƥ\��G���r��K�[ ��M��
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
        /// <para> �ھڶǤJ�� <paramref name="prefab"></paramref> �Ѽ�, ��^��H�����w�Ʀn���C����H </para>
        /// </summary>
        /// <param name="prefab">
        /// <para> Specified gameObject prefab. </para>
        /// <para> ���w���C����H�w�m�� </para>
        /// </param>
        /// <param name="position">
        /// <para> Specified release position. </para>
        /// <para> ���w�����m </para>
        /// </param>
        /// <param name="prefab">
        /// <para> Specified rotation. </para>
        /// <para> ���w������� </para>
        /// </param>
        /// <param name="prefab">
        /// <para> Specified scale. </para>
        /// <para> ���w���Y��� </para>
        /// </param>
        /// <returns>
        /// <para> Prepared gameObject in the pool. </para>
        /// <para> ��H�����w�Ʀn���C����H </para>
        /// </returns>
        // ���} �R�A�禡 ��^�C������ �W��()
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
