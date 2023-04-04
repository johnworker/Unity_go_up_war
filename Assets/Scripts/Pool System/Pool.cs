using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool 
{
    [SerializeField, Header("��H���w�m��")]
    GameObject prefab;

    [SerializeField, Header("��H���ؤo")]
    int size = 1;
    // ���C(�S���C��)
    Queue<GameObject> queue;

    // Initialize ��l�Ƥ�k (����l�Ʒ|�|�ȬO�ŭ�)
    public void Initialize()
    {
        queue = new Queue<GameObject>();

        for (var i = 0; i < size; i++)
        {
            // Enqueue �J�C��� (�\��:��C���̥��K�[�@�Ӥ���)
            queue.Enqueue(Copy());
        }
    }

    // ��^�ȬO�C����H �ƻs()
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab);

        copy.SetActive(false);

        return copy;
    }
}
