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

        [SerializeField]
        float moveSpeed = 5f;

        [SerializeField, Header("�[�t�ɶ�")]
        float accelerationTime = 0.2f;
        [SerializeField, Header("��t�ɶ�")]
        float decelerationTime = 0.2f;

        [SerializeField, Header("���ʱ��ਤ��")]
        float moveRotationAngle = 70f;

        [SerializeField]
        float paddingX = 0.6f;
        [SerializeField]
        float paddingY = 0.6f;

        [SerializeField, Header("�l�u�w�m")]
        GameObject projectile;

        [SerializeField, Header("�l�u�w�m2")]
        GameObject projectile2;

        [SerializeField, Header("�l�u�w�m3")]
        GameObject projectile3;

        [SerializeField, Header("�j�f��")]
        Transform muzzleMiddle;

        [SerializeField, Header("�j�f��")]
        Transform muzzleLeft;

        [SerializeField, Header("�j�f�k")]
        Transform muzzleRight;

        [SerializeField, Range(0,2), Header("�Z���¤O�ܶq")]
        int weaponPower = 0;

        [SerializeField, Header("����ɶ��]�w")]
        float fireInterval = 0.2f;

        [SerializeField, Header("���浥�ݮɶ�")]
        WaitForSeconds waitForFireInterval;

        new Rigidbody2D rigidbody;

        Coroutine moveCoroutine;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            // Viewport.Instance �Y�i���o���
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
            // ����.���O = 0f;
            rigidbody.gravityScale = 0f;
            // ��l�� ����o�g�ɶ�
            waitForFireInterval = new WaitForSeconds(fireInterval);

            input.OnEnableGameplayInput();
        }

        #region ����
        private void Move(Vector2 moveInput)
        {
            // 2���V�q ���ʶq = ���ʿ�J * ���ʳt��;
            //    Vector2 moveAmount = moveInput * moveSpeed;

            // ����.�t�� = ���ʿ�J * ���ʳt��;
            // rigidbody.velocity = moveInput * moveSpeed;

            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // ���n��4�����ܶq
             Quaternion moveRotation = Quaternion.Euler(-180,90 + moveRotationAngle * -moveInput.x, -90);

            // (���ʿ�J.�k�@�ƳB�z * ���ʳt��);
            moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveInput.normalized * moveSpeed, moveRotation));
            StartCoroutine(MovePositionLimitCoroutine());
        }

        private void StopMove()
        {
            // ����.�t�� = 2���V�q.�k�s
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
            // �n�����a���B�I�� t
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
                // ��e��m�ഫ = �O�f.���.���a���ʦ�m��k(�ഫ��m);
                transform.position = Viewport.Instance.PlayerMoveablePosition(transform.position, paddingX, paddingY);

                yield return null;
            }
        }
        #endregion

        #region �}��
        private void Fire()
        {
            // StartCoroutine("FireCoroutine");
            StartCoroutine(nameof(FireCoroutine));
        }

        private void StopFire()
        {
            // StopCoroutine("FireCoroutine");
            // StopCoroutine(FireCoroutine()); // �Ӹ��ǤJ�I�s���|�B�@
            StopCoroutine(nameof(FireCoroutine));
        }

        IEnumerator FireCoroutine()
        {
            // �`���ͦ��g��
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
