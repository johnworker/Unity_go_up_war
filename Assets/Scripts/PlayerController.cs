using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace hero
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10f;
        public GameObject obstaclePrefab;
        public float obstacleInterval = 3f;

        private float obstacleTimer;
        private bool gameOver;

        public Animator animator;

        void Start()
        {
            gameOver = false;
        }

        void Update()
        {
            if (!gameOver)
            {
                float moveVertical = Input.GetAxis("Vertical");
                transform.Translate(Vector3.up * moveVertical * speed * Time.deltaTime, Space.World);

                obstacleTimer += Time.deltaTime;
                if (obstacleTimer >= obstacleInterval)
                {
                    Instantiate(obstaclePrefab, transform.position + new Vector3(10, 0, 0), Quaternion.identity);
                    obstacleTimer = 0;
                }
            }
        }

        void GameOver()
        {
            gameOver = true;
            Debug.Log("Game Over");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameOver();
            }
        }

        void Attack()
        {
            animator.SetBool("isAttacking", true);
        }

        /*
         * �o�q�N�X��{�F�H�U�\��G

            1. �]�m�F�@�ӦW�� "speed" �����@�ܶq�A�Ω󱱨�a���⪺���ʳt�סF
            2. �]�m�F�@�ӦW�� "obstaclePrefab" �����@�ܶq�A�Ω���w��ê�����w�s��F
            3. �]�m�F�@�ӦW�� "obstacleInterval" �����@�ܶq�A�Ω󱱨�ͦ���ê�����ɶ����j�F
            4. �]�m�F�@�ӦW�� "obstacleTimer" ���p���ܶq�A�Ω�p��ͦ���ê�����ɶ��F
            5. �b "Update" ��Ƥ��A��������b����J�ȡA�îھڨ�ȧ�s���a���⪺��m�F
            6. �b "Update" ��Ƥ��A�p��ͦ���ê�����ɶ��A�p�G�ɶ���F�F���w���ɶ����j�A�h�ͦ��@�ӻ�ê���C
            
            �B�J 6�G�B��C��
            
            �����H�W�B�J��A�O�s�����M�}���A�M��B��C���C�A���ӯ���ݨ�@�Ӫ��V���b�C�������A�䤤���@�Ӫ��a����M�@�Ӧb��e��ͦ�����ê���C�A�i�H�ϥΤ�V��� WASD �䱱��a����W�U���ʡA�קK�I���ê���C�i�H�q�L�վ�۾�����m�M�j�p�A�H�νվ��ê���ͦ�����m�M�ɶ����j�A�ӽվ�C�������שM��{�ĪG�C
            
            �o�O�@��²�檺���V���b�C���оǡA�A�i�H�ھڦۤv���ݭn�i�@�B�X�i�M�u�ƹC���\��C���A�b Unity 3D ���ЫإX��[�X�⪺�C���I
         */
    }
}