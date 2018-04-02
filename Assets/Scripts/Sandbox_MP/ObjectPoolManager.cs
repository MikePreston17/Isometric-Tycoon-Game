using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sandbox_MP
{
    /// <summary>
    /// Class that can hold multiple reusable objects.
    /// </summary>
    public class ObjectPoolManager : MonoBehaviour
    {
        private static ObjectPoolManager _instance;
        public static ObjectPoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
                }
                return _instance;
            }
        }

        public GameObject prefabPrototype;// = new GameObject("Bullet");
        public GameObject prefabContainer;
        public List<GameObject> bullets = new List<GameObject>();

        public int InitialSpawn = 20;

        void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            for (int i = 0; i < InitialSpawn; i++)
            {
                var clone = Instantiate(prefabPrototype, Vector3.zero, Quaternion.identity) as GameObject;
                clone.transform.parent = prefabContainer.transform;
                clone.SetActive(false);
                bullets.Add(clone);
            }
        }
    }
}
