using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer)),RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshCollider))]
public class patch_legacy : MonoBehaviour {
    public float[] h;
    public int row;
    public int column;

    public void Initialize(int _row, int _column, float[] heights) {
        name = "terrain(" + _row + "," + _column + ")";
        tag = "Terrain";
        row = _row;
        column = _column;
        h = heights;
        GenerateMesh();
        gameObject.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Terrain");
        gameObject.transform.position = new Vector3(column + .5f, 0, row + .5f);
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    void OnBecameVisible() {
        GetComponent<MeshCollider>().enabled = true;
    }
    void OnBecameInvisible() {
        GetComponent<MeshCollider>().enabled = false;
    }

    public Mesh GenerateMesh() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[] {
            new Vector3(0.5f, 0, 0.5f),
            new Vector3(-0.5f, 0, 0.5f),
            new Vector3(0.5f, h[1], 0.5f),
            new Vector3(-0.5f, h[0], 0.5f),
            new Vector3(0.5f, h[2], -0.5f),
            new Vector3(-0.5f, h[3], -0.5f),
            new Vector3(0.5f, 0, -0.5f),
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(0.5f, h[1], 0.5f),
            new Vector3(-0.5f, h[0], 0.5f),
            new Vector3(0.5f, h[2], -0.5f),
            new Vector3(-0.5f, h[3], -0.5f),
            new Vector3(0.5f, 0, -0.5f),
            new Vector3(0.5f, 0, 0.5f),
            new Vector3(-0.5f, 0, 0.5f),
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(-0.5f, 0, 0.5f),
            new Vector3(-0.5f, h[0], 0.5f),
            new Vector3(-0.5f, h[3], -0.5f),
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(0.5f, 0, -0.5f),
            new Vector3(0.5f, h[2], -0.5f),
            new Vector3(0.5f, h[1], 0.5f),
            new Vector3(0.5f, 0, 0.5f)
        };
        mesh.vertices = vertices;
        float extrema = (float)Int32.MaxValue;
        int extremaIndex = 0;
        for (int i = 0; i < 4; i++) { // finds the lowest point, extrema
            if (h[i] < extrema) {
                extrema= h[i];
                extremaIndex = i;
            }
        }
        int[] triangles = new int[36] {
            0,2,3,
            0,3,1,
            8,4,5,
            8,5,9,
            10,6,7,
            10,7,11,
            12,13,14, 
            12,14,15,
            16,17,18,
            16,18,19,
            20,21,22,
            20,22,23};
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        return mesh;
    }
    public void SetHeights(float[] heights) {
        h = heights;
        GenerateMesh();
    }



    public int GetCW(int hIndex) {
        if (hIndex + 1 > 3)
            return 0;
        else
            return hIndex + 1;
    }
    public int GetCCW(int hIndex) {
        if (hIndex - 1 < 0)
            return 3;
        else return hIndex - 1;
    }






}
