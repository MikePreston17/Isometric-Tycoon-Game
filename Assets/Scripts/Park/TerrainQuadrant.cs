using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
public class TerrainQuadrant : MonoBehaviour
{
    public int LeftBoundary, RightBoundary, BottomBoundary, TopBoundary;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        gameObject.layer = 8;
    }
    void Update()
    {

    }
}
