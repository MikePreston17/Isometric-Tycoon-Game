using UnityEngine;

public class Cube : MonoBehaviour
{
    public float UpForce = 1f;
    public float SideForce = .1f;

    void Start()
    {
        float xForce = Random.Range(-SideForce, SideForce);
        float yForce = Random.Range(UpForce / 2f, UpForce);
        float zForce = Random.Range(-SideForce, SideForce);

        var force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }
}
