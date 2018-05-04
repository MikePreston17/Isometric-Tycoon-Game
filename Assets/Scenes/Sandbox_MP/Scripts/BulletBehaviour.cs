using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnEnable()
    {

    }
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 10);

        if (transform.position.y > 10)
        {
            OnDisable();
        }
    }

    private void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
}
