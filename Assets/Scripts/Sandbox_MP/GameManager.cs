using UnityEngine;

namespace Assets.Scripts.Sandbox_MP
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                return Singleton<GameManager>.Instance;
            }
            private set
            {
                _instance = value;
            }
        }

        public int Score { get; set; }
        public bool IsDead { get; set; }

        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            Score = 10;
        }
    }

    public class GameProperties : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.Score = 25;
        }
    }

    public class MyPlayer : MonoBehaviour
    {
        private GameManager gameManger = GameManager.Instance;

        private void Start()
        {
        }
        private void Update()
        {

        }
    }

}
