using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Herohunk
{
    public class Viewport : Singleton<Viewport>
    {
        float minX;
        float maxX;
        float minY;
        float maxY;

        private void Start()
        {
            // 定義攝影機
            Camera mainCamera = Camera.main;

            // 聲明2維向量變量
            Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
            Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

            minX = bottomLeft.x;
            minY = bottomLeft.y;
            maxX = topRight.x;
            maxY = topRight.y;
        }

        // 玩家移動位置方法 (參數為 3維向量 玩家位置)
        public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY)
        {
            // 3維變量初始化
            Vector3 position = Vector3.back;

            // 可將浮點數界定在一個區間內 (即可獲取限定範圍內的X和Y值)
            position.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);
            position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);

            // 返回 位置;
            return position;
        }

    }
}
