using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        PlayerInput input;

        new Rigidbody2D rigidbody;

        [SerializeField]
        float moveSpeed = 10f;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            input.onMove += Move;
            input.onStopMove += StopMove;
        }

        private void OnDisable()
        {
            input.onMove -= Move;
            input.onStopMove -= StopMove;
        }

        void Start()
        {
            // 剛體.重力 = 0f;
            rigidbody.gravityScale = 0f;

            input.OnEnableGameplayInput();
        }

        private void Move(Vector2 moveInput)
        {
            // 2維向量 移動量 = 移動輸入 * 移動速度;
            //    Vector2 moveAmount = moveInput * moveSpeed;

            // 剛體.速度 = 移動輸入 * 移動速度;
            rigidbody.velocity = moveInput * moveSpeed;
        }

        private void StopMove()
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
