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

        [SerializeField, Header("加速時間")]
        float accelerationTime = 3f;
        [SerializeField, Header("減速時間")]
        float decelerationTime = 3f;

        [SerializeField]
        float paddingX = 0.6f;
        [SerializeField]
        float paddingY = 0.6f;

        Coroutine moveCoroutine;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            // Viewport.Instance 即可取得實例
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
            // rigidbody.velocity = moveInput * moveSpeed;

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // (移動輸入.歸一化處理 * 移動速度);
            moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveInput.normalized * moveSpeed));
            StartCoroutine(MovePositionLimitCoroutine());
        }

        private void StopMove()
        {
            // 剛體.速度 = 2維向量.歸零
            // rigidbody.velocity = Vector2.zero;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero));
            StopCoroutine(MovePositionLimitCoroutine());
        }

        IEnumerator MoveCoroutine(float time, Vector2 moveVelocity)
        {
            // 聲明本地的浮點數 t
            float t = 0f;

            while(t < time)
            {
                t += Time.fixedDeltaTime / time;
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelocity, t / time);

                yield return null;
            }
        }

        IEnumerator MovePositionLimitCoroutine()
        {
            while (true)
            {
                // 當前位置轉換 = 是口.實例.玩家移動位置方法(轉換位置);
                transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);

                yield return null;
            }
        }
    }
}
