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
            // ����.���O = 0f;
            rigidbody.gravityScale = 0f;

            input.OnEnableGameplayInput();
        }

        private void Move(Vector2 moveInput)
        {
            // 2���V�q ���ʶq = ���ʿ�J * ���ʳt��;
            //    Vector2 moveAmount = moveInput * moveSpeed;

            // ����.�t�� = ���ʿ�J * ���ʳt��;
            rigidbody.velocity = moveInput * moveSpeed;
        }

        private void StopMove()
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}
