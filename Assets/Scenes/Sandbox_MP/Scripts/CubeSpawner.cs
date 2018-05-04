using UnityEngine;

//https://www.youtube.com/watch?v=tdSmKaJvCoA
public class CubeSpawner : MonoBehaviour
{
    public GameObject prefab;

    private void FixedUpdate()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
