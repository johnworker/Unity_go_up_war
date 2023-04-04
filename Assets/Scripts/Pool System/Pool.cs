using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool 
{
    [SerializeField, Header("對象池預置物")]
    GameObject prefab;

    [SerializeField, Header("對象池尺寸")]
    int size = 1;
    // 隊列(特殊的列表)
    Queue<GameObject> queue;

    // Initialize 初始化方法 (未初始化會會值是空值)
    public void Initialize()
    {
        queue = new Queue<GameObject>();

        for (var i = 0; i < size; i++)
        {
            // Enqueue 入列函數 (功能:對列的最末添加一個元素)
            queue.Enqueue(Copy());
        }
    }

    // 返回值是遊戲對象 複製()
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab);

        copy.SetActive(false);

        return copy;
    }
}
