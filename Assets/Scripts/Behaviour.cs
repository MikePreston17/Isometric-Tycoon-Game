using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    [SerializeField]
    public Node child;
    public new SkinnedMeshRenderer renderer;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SkinnedMeshRenderer>();
    }
}
