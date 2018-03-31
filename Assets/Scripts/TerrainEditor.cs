//C# Example

using UnityEngine;
using UnityEditor;
using System.Collections;
using Terrain;

class TerrainEditor : EditorWindow {
    int row = 5;
    int column = 5;

    [MenuItem("Window/Terrain Editor")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(TerrainEditor));
    }

    void OnGUI() {
        row = EditorGUILayout.IntSlider(row, 0, 7);
        column = EditorGUILayout.IntSlider(column, 0, 7);
        if (GUILayout.Button("NW")) {
            Park.Instance.Raise(row, column, 1, 0, 0, 0);
        }
        if (GUILayout.Button("NE")) {
            Park.Instance.Raise(row, column, 0, 1, 0, 0);
        }
        if (GUILayout.Button("SE")) {
            Park.Instance.Raise(row, column, 0, 0, 1, 0);
        }
        if (GUILayout.Button("SW")) {
            Park.Instance.Raise(row, column, 0, 0, 0, 1);
        }
        if (GUILayout.Button("Load")) {
            Park.Instance.Load("debug");
        }
        if (GUILayout.Button("Save")) {
            Park.Instance.Save("debug");
        }
    }
}