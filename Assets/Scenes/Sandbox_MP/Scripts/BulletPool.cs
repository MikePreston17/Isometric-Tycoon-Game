/// <summary>
/// Collection of Object Pools
/// </summary>

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sandbox_MP
{
    public class BulletPool : MonoBehaviour
    {
        #region Scrappable
        private static BulletPool _instance;
        public static BulletPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject("ObjectPool").AddComponent<BulletPool>();
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
        #endregion Scrappable
    }
}
