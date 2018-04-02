using UnityEngine;

public class CameraController : MonoBehaviour
{
    Plane origin = new Plane(Vector3.up, Vector3.zero);
    Vector3 cursor;
    Vector3 focus;
    float dist;  //todo: what kind of distance?  Name it.
    float dist2; //todo: what kind of distance?  Name it.
    public float velocity;
    int terrainMask = 1 << 8;
    RaycastHit info;
    Ray ray;

    //public GameObject ProjectorObject;

    void Awake()
    {
        velocity = 0;
        //Park.Instance.Load("debug");
        Debug.Log("Camera awake");
        var park = Park.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        origin.Raycast(ray, out dist);
        cursor = ray.GetPoint(dist);
        if (Physics.Raycast(ray, out info, Mathf.Infinity, terrainMask))
        {
            //ProjectorObject.transform.position = new Vector3(info.transform.position.x, 150, info.transform.position.z);
            //ProjectorObject.transform.rotation = Quaternion.Euler(90, 0, 0);
            //Debug.Log("Hit the terrain!");
        }

        velocity += Input.GetAxis("Scroll");
        ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
        origin.Raycast(ray, out dist2);
        focus = ray.GetPoint(dist2);

        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * dist / 4, Space.Self);
        transform.Translate(new Vector3(0, 1, 1) * Input.GetAxis("Vertical") * Time.deltaTime * dist / 4, Space.Self);
        transform.Translate(Vector3.forward * velocity * Time.deltaTime * 1000, Space.Self);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.RotateAround(focus, Vector3.up, 90f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.RotateAround(focus, Vector3.up, -90f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Park.Instance.Save("debug");
        }
    }

    private void LateUpdate()
    {
        velocity *= .99f * Time.deltaTime; // todo: scale accurately to framerate
    }
}
