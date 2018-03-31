using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTool : Tool {
    void Start() {

    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            //TODO: Make a raycast that uses the layermask "terrain"
            if (hit.collider == null) {
                return;
            }
            if (hit.collider.gameObject.tag == "Terrain") {
                patch_legacy patch = hit.collider.gameObject.GetComponent<patch_legacy>();
                GameController.Instance.park.PlacePath(patch.row, patch.column, patch.h[0]); // TODO: Place patch instead at highest
            }
        }

    }




    


}
