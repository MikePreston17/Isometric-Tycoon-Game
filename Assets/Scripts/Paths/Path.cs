using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class Path : Block {
    int row, column;
    float height;

    public void Initialize(int _row, int _column, float _height) {
        row = _row;
        column = _column;
        height = _height;
        name = "path(" + _row + "," + _column + ")";
        GetComponent<MeshFilter>().mesh = GenerateMesh();
        gameObject.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Path");
        gameObject.transform.position = new Vector3(_column + .5f, _height, _row + .5f);
    }

    public Mesh GenerateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3(0.5f, .01f, 0.5f),
            new Vector3(-0.5f, .01f, 0.5f),
            new Vector3(0.5f, .01f, -0.5f),
            new Vector3(-0.5f, .01f, -0.5f),
        };
        mesh.triangles = new int[] {
            1,0,2,
            2,3,1
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }

    public Path() {
        //TODO: Spawn prefab based upon "tilemapper" (see Parkitect)
    }








}
