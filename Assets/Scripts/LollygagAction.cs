using UnityEngine;

public class LollygagAction : Node
{
    GameObject gameObject;
    Block path;
    [SerializeField]
    float timer = 0;

    public override Status Tick(float deltaTime)
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            gameObject.transform.Translate(Vector3.forward);
            timer = 0;
        }
        return Status.Running;
    }
}
