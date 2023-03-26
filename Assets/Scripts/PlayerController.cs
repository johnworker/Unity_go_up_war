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
         * 這段代碼實現了以下功能：

            1. 設置了一個名為 "speed" 的公共變量，用於控制玩家角色的移動速度；
            2. 設置了一個名為 "obstaclePrefab" 的公共變量，用於指定障礙物的預製體；
            3. 設置了一個名為 "obstacleInterval" 的公共變量，用於控制生成障礙物的時間間隔；
            4. 設置了一個名為 "obstacleTimer" 的私有變量，用於計算生成障礙物的時間；
            5. 在 "Update" 函數中，獲取垂直軸的輸入值，並根據其值更新玩家角色的位置；
            6. 在 "Update" 函數中，計算生成障礙物的時間，如果時間到達了指定的時間間隔，則生成一個障礙物。
            
            步驟 6：運行遊戲
            
            完成以上步驟後，保存場景和腳本，然後運行遊戲。你應該能夠看到一個直向卷軸遊戲場景，其中有一個玩家角色和一個在其前方生成的障礙物。你可以使用方向鍵或 WASD 鍵控制玩家角色上下移動，避免碰到障礙物。可以通過調整相機的位置和大小，以及調整障礙物生成的位置和時間間隔，來調整遊戲的難度和表現效果。
            
            這是一個簡單的直向卷軸遊戲教學，你可以根據自己的需要進一步擴展和優化遊戲功能。祝你在 Unity 3D 中創建出更加出色的遊戲！
         */
    }
}