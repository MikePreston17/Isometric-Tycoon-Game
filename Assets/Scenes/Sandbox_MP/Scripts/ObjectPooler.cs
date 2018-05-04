using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        Debug.Log("start");
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in Pools)
        {
            Debug.Log("creating pool");
            var objectPool = new Queue<GameObject>();
            Debug.Log("pool created");

            Debug.Log("pool size: " + pool.Size);
            for (int i = 0; i < pool.Size; i++)
            {
                Debug.Log(i);
                var myObject = Instantiate(pool.Prefab);
                myObject.SetActive(false);
                objectPool.Enqueue(myObject);
                Debug.Log("object enqueued");
            }

            poolDictionary.Add(pool.Tag, objectPool);
        }
    }
}
