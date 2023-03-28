using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class BackgroundScroller : MonoBehaviour
    {
        [Header("捲動速度"), SerializeField]
        Vector2 scrollVelocity;

        [Header("材質")]
        Material material;

        void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        void Update()
        {
            // 材質.材質捲動 加上 捲動速度 * 時間間隔;
            material.mainTextureOffset += scrollVelocity * Time.deltaTime;
        }
    }
}
