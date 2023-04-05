using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ѩ�Pool �~�� MonoBehaviour �ҭn�� [System.Serializable] �b Pool �e���}�X��
[System.Serializable]
public class Pool 
{
    /* �g�k1
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
    */

    // �g�k2
    //public GameObject Prefab { get => prefab; }

    // �g�k3
    public GameObject Prefab => prefab;

    public int Size => size;

    // �B��ɪ��ؤo
    public int RuntimeSize => queue.Count;

    [SerializeField, Header("��H���w�m��")]
    GameObject prefab;

    [SerializeField, Header("��H���ؤo")]
    int size = 1;
    // ���C(�S���C��)
    Queue<GameObject> queue;

    Transform parent;

    // Initialize ��l�Ƥ�k (����l�Ʒ|�|�ȬO�ŭ�)
    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for (var i = 0; i < size; i++)
        {
            // Enqueue �J�C��� (�\��:��C���̥��K�[�@�Ӥ���)
            queue.Enqueue(Copy());
        }
    }

    // ��^�ȬO�C����H �ƻs()
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);

        copy.SetActive(false);

        return copy;
    }

    // ��^�ȹC������ ���Ϊ���()
    GameObject AvailableObject()
    {
        // �C������ �i�Ϊ��� = �ᤩ�ŭ�;
        GameObject availableObject = null;

        //  Peek��ơG�Ω��^��C���Ĥ@�Ӥ��� ���|�N�����q��C����
        if (queue.Count > 0 && !queue.Peek().activeSelf)
        {
            // Dequeue �X�C��� (�\��:���X��C���Ĥ@�Ӥ����ñN������)
            // ��C�Ƥj��0 �N�X�C��^�ȵ� availableObject
            availableObject = queue.Dequeue();
        }
        else
        {
            // �S���ɪ�^�ȥ��� Copy() �� availableObject ��
            availableObject = Copy();
        }

        queue.Enqueue(availableObject);

        return availableObject;
    }

    // �n����^�ȹC������ �ǳƪ���()
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
    // �n�� ��^(�C������)
    public void Return(GameObject gameObject)
    {
        queue.Enqueue(gameObject);
    }
    */
}
