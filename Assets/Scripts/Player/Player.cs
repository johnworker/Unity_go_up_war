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
        float moveSpeed = 5f;

        [SerializeField]
        float paddingX = 0.6f;
        [SerializeField]
        float paddingY = 0.6f;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            // Viewport.Instance �Y�i���o���
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

            StartCoroutine(MovePositionLimitCoroutine());
        }

        private void StopMove()
        {
            // ����.�t�� = 2���V�q.�k�s
            rigidbody.velocity = Vector2.zero;

            StopCoroutine(MovePositionLimitCoroutine());
        }

        IEnumerator MovePositionLimitCoroutine()
        {
            while (true)
            {
                // ��e��m�ഫ = �O�f.���.���a���ʦ�m��k(�ഫ��m);
                transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);

                yield return null;
            }
        }
    }
}
