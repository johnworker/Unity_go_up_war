using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : Character
    {
        [SerializeField] StatsBar_HUD statsBar_HUD;

        [SerializeField, Header("是否再生(生命值)")]
        bool regenerateHealth = true;

        [SerializeField, Header("(生命值)再生時間")]
        float healthRegenerateTime;

        [SerializeField, Header("(生命值)再生百分比"),Range(0f, 1f)]
        float healthRegeneratePercent;

        [Header("--- 輸入 ---")]

        [SerializeField]
        PlayerInput input;

        [Header("--- 移動 ---")]

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

        [Header("--- 開火 ---")]

        [SerializeField, Header("子彈預置")]
        GameObject projectile;

        [SerializeField, Header("子彈預置2")]
        GameObject projectile2;

        [SerializeField, Header("子彈預置3")]
        GameObject projectile3;

        [SerializeField, Header("槍口中")]
        Transform muzzleMiddle;

        [SerializeField, Header("槍口左")]
        Transform muzzleLeft;

        [SerializeField, Header("槍口右")]
        Transform muzzleRight;

        [SerializeField, Range(0,2), Header("武器威力變量")]
        int weaponPower = 0;

        [SerializeField, Header("間格時間設定")]
        float fireInterval = 0.2f;

        [SerializeField, Header("間格等待時間")]
        WaitForSeconds waitForFireInterval;

        [Header("等待生命恢復時間")]
        WaitForSeconds waitHealthRegenerateTime;

        new Rigidbody2D rigidbody;

        Coroutine moveCoroutine;

        Coroutine healthRegenerateCoroutine;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            // Viewport.Instance 即可取得實例
        }

        protected override void OnEnable()
        {
            base.OnEnable();

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
            // 初始化 間格發射時間
            waitForFireInterval = new WaitForSeconds(fireInterval);
            waitHealthRegenerateTime = new WaitForSeconds(healthRegenerateTime);

            statsBar_HUD.Initialize(health, maxHealth);

            input.OnEnableGameplayInput();

            TakeDamage(50f);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            statsBar_HUD.UpdateStats(health, maxHealth);

            if (gameObject.activeSelf)
            {
                if (regenerateHealth)
                {
                    if(healthRegenerateCoroutine != null)
                    {
                        StopCoroutine(healthRegenerateCoroutine);
                    }
                    healthRegenerateCoroutine = StartCoroutine(HealthRegenerateCoroutine(waitHealthRegenerateTime, healthRegeneratePercent));
                }
            }
        }

        public override void RestoreHealth(float value)
        {
            base.RestoreHealth(value);
            statsBar_HUD.UpdateStats(health, maxHealth);

        }

        public override void Die()
        {
            statsBar_HUD.UpdateStats(0f, maxHealth);
            base.Die();
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
                t += Time.fixedDeltaTime;
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
                /*
                switch (weaponPower)
                {
                    case 0:
                        Instantiate(projectile, muzzleMiddle.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(projectile, muzzleLeft.position, Quaternion.identity);
                        Instantiate(projectile, muzzleRight.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(projectile, muzzleMiddle.position, Quaternion.identity);
                        Instantiate(projectile2, muzzleLeft.position, Quaternion.identity);
                        Instantiate(projectile3, muzzleRight.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
                */

                 switch (weaponPower)
                {
                    case 0:
                        PoolManager.Release(projectile, muzzleMiddle.position, Quaternion.identity, muzzleMiddle.localPosition);
                        break;
                    case 1:
                        PoolManager.Release(projectile, muzzleLeft.position, Quaternion.identity, muzzleLeft.localPosition);
                        PoolManager.Release(projectile, muzzleRight.position, Quaternion.identity, muzzleRight.localPosition);
                        break;
                    case 2:
                        PoolManager.Release(projectile, muzzleMiddle.position, Quaternion.identity, muzzleMiddle.localPosition);
                        PoolManager.Release(projectile2, muzzleLeft.position, Quaternion.identity, muzzleLeft.localPosition);
                        PoolManager.Release(projectile3, muzzleRight.position, Quaternion.identity, muzzleRight.localPosition);
                        break;
                    default:
                        break;
                }

                yield return waitForFireInterval;
            }
        }
        #endregion
    }
}
