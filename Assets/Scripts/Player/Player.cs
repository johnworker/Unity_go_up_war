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
        float accelerationTime = 0.2f;
        [SerializeField, Header("減速時間")]
        float decelerationTime = 0.2f;

        [SerializeField, Header("移動旋轉角度")]
        float moveRotationAngle = 70f;

        [SerializeField]
        float paddingX = 0.6f;
        [SerializeField]
        float paddingY = 0.6f;

        [SerializeField, Header("子彈預置")]
        GameObject projectile;

        [SerializeField, Header("槍口")]
        Transform muzzle;

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
            input.onFire += Fire;
            input.onStopFire += StopFire;
        }

        private void OnDisable()
        {
            input.onMove -= Move;
            input.onStopMove -= StopMove;
            input.onFire -= Fire;
            input.onStopFire -= StopFire;
        }

        void Start()
        {
            // 剛體.重力 = 0f;
            rigidbody.gravityScale = 0f;

            input.OnEnableGameplayInput();
        }

        #region 移動
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

            // 先聲明4元數變量
             Quaternion moveRotation = Quaternion.Euler(-180,90 + moveRotationAngle * -moveInput.x, -90);

            // (移動輸入.歸一化處理 * 移動速度);
            moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveInput.normalized * moveSpeed, moveRotation));
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

            StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero, Quaternion.Euler(-180, 90, -90)));
            StopCoroutine(MovePositionLimitCoroutine());
        }

        IEnumerator MoveCoroutine(float time, Vector2 moveVelocity, Quaternion moveRotation)
        {
            // 聲明本地的浮點數 t
            float t = 0f;

            while(t < time)
            {
                t += Time.fixedDeltaTime / time;
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelocity, t / time);
                transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t / time);

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
        #endregion

        #region 開火
        private void Fire()
        {
            // StartCoroutine("FireCoroutine");
            StartCoroutine(nameof(FireCoroutine));
        }

        private void StopFire()
        {
            // StopCoroutine("FireCoroutine");
            // StopCoroutine(FireCoroutine()); // 承載傳入呼叫不會運作
            StopCoroutine(nameof(FireCoroutine));
        }

        IEnumerator FireCoroutine()
        {
            // 循環生成射擊
            while (true)
            {
                Instantiate(projectile, muzzle.position, Quaternion.identity);

                yield return null;
            }
        }
        #endregion
    }
}
