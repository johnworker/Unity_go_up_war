using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 由於Pool 繼承 MonoBehaviour 所要用 [System.Serializable] 在 Pool 前公開出來
[System.Serializable]
public class Pool 
{
    /* 寫法1
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
    */

    // 寫法2
    //public GameObject Prefab { get => prefab; }

    // 寫法3
    public GameObject Prefab => prefab;

    public int Size => size;

    // 運行時的尺寸
    public int RuntimeSize => queue.Count;

    [SerializeField, Header("對象池預置物")]
    GameObject prefab;

    [SerializeField, Header("對象池尺寸")]
    int size = 1;
    // 隊列(特殊的列表)
    Queue<GameObject> queue;

    Transform parent;

    // Initialize 初始化方法 (未初始化會會值是空值)
    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for (var i = 0; i < size; i++)
        {
            // Enqueue 入列函數 (功能:對列的最末添加一個元素)
            queue.Enqueue(Copy());
        }
    }

    // 返回值是遊戲對象 複製()
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);

        copy.SetActive(false);

        return copy;
    }

    // 返回值遊戲物件 有用物件()
    GameObject AvailableObject()
    {
        // 遊戲物件 可用物體 = 賦予空值;
        GameObject availableObject = null;

        //  Peek函數：用於返回對列的第一個元素 不會將元素從對列移除
        if (queue.Count > 0 && !queue.Peek().activeSelf)
        {
            // Dequeue 出列函數 (功能:取出對列的第一個元素並將它移除)
            // 對列數大於0 將出列返回值給 availableObject
            availableObject = queue.Dequeue();
        }
        else
        {
            // 沒有時返回值先取 Copy() 給 availableObject 用
            availableObject = Copy();
        }

        queue.Enqueue(availableObject);

        return availableObject;
    }

    // 聲明返回值遊戲物件 準備物件()
    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }

    /*
    // 聲明 返回(遊戲物件)
    public void Return(GameObject gameObject)
    {
        queue.Enqueue(gameObject);
    }
    */
}
