using System;
using UnityEngine;

public class TerrainTool : Tool
{
    public GameObject ProjectorPrefab;
    public GameObject projector;
    public GUISkin skin;

    public patch_legacy selection;
    public Vector3 prevMousePosition;

    void Start()
    {
        //projector = Instantiate(ProjectorPrefab);
        //projector.GetComponent<Projector>().orthographic = true;
        //projector.GetComponent<Projector>().orthographicSize = 1;
        GameController.Instance.G();
    }

    void Update()
    {
        Vector3 mousePosition = RoundedMousePosition(new Plane(Vector3.up, Vector3.one), 1f);
        mousePosition.y = 10f;
        //projector.transform.position = mousePosition;
        //projector.transform.rotation = Quaternion.Euler(90, 0, 0);

        if (Input.GetMouseButtonUp(0))
        {
            selection = null;
            prevMousePosition = new Vector3(0, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            prevMousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider == null)
            {
                return;
            }
            if (hit.collider.gameObject.tag == "Terrain")
            {
                selection = hit.collider.gameObject.GetComponent<patch_legacy>();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if ((Input.mousePosition.normalized.y - prevMousePosition.normalized.y > .1f) && selection != null)
            {
                prevMousePosition = Input.mousePosition;
                GameController.Instance.park.AdjustTerrain(selection.Row, selection.Column, selection.Row + 2, selection.Column + 2, new float[] { 1, 1, 1, 1 }, true);
            }

            if ((Input.mousePosition.normalized.y - prevMousePosition.y < -.1f) && selection != null)
            {
                prevMousePosition = Input.mousePosition;
                //GameController.Instance.zoo.AdjustTerrain(selection.row, selection.column, selection.row + 2, selection.column + 2, new float[] {-1,-1,-1,-1}, true);
            }
        }
    }

    void OnGUI()
    {
        //GUI.skin = skin;
        GUILayout.Window(0, new Rect(Screen.width - 150, 0, 150, 150), DrawTerrainWindow, "Terrain Tools");
    }

    private void DrawTerrainWindow(int id)
    {
        if (selection != null)
        {
            GUILayout.Label(string.Format("Patch {0},{1}", selection.Row, selection.Column));
        }

        if (GUILayout.Button("Raise"))
        {
            GameController.Instance.park.RaisePatch(selection.Row, selection.Column);
        }

        if (GUILayout.Button("Lower"))
        {
            throw new NotImplementedException("DrawTerrainWindow:Lower");
        }

        GUILayout.Toggle(false, "Mountain");
        GUI.DragWindow();
    }
}