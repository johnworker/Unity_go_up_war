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
            // �w�q��v��
            Camera mainCamera = Camera.main;

            // �n��2���V�q�ܶq
            Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
            Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

            minX = bottomLeft.x;
            minY = bottomLeft.y;
            maxX = topRight.x;
            maxY = topRight.y;
        }

        // ���a���ʦ�m��k (�ѼƬ� 3���V�q ���a��m)
        public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY)
        {
            // 3���ܶq��l��
            Vector3 position = Vector3.back;

            // �i�N�B�I�Ƭɩw�b�@�Ӱ϶��� (�Y�i������w�d�򤺪�X�MY��)
            position.x = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);
            position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);

            // ��^ ��m;
            return position;
        }

    }
}
