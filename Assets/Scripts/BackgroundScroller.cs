using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class BackgroundScroller : MonoBehaviour
    {
        [Header("���ʳt��"), SerializeField]
        Vector2 scrollVelocity;

        [Header("����")]
        Material material;

        void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        void Update()
        {
            // ����.���豲�� �[�W ���ʳt�� * �ɶ����j;
            material.mainTextureOffset += scrollVelocity * Time.deltaTime;
        }
    }
}
