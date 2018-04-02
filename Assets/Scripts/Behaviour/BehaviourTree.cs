using UnityEngine;

public abstract class Node
{
    public abstract Status Tick(float deltaTime);
    [SerializeField]
    public enum Status
    {
        Success,
        Failed,
        Running
    }
    public object[] nextparams;
    public void Initialize(GameObject g, params object[] parameters)
    {

    }
}
