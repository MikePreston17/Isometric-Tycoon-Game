using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainEditorPanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{
    public void OnBeginDrag(PointerEventData eventData) {
        throw new NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData) {
        throw new NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData) {
        throw new NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData) {
        throw new NotImplementedException();
    }
    public void IncRadius() {
        Debug.Log("Incremented radius!");
    }
    public void DecRadius() {
        Debug.Log("Decremented radius!");
    }

    private void Awake() {
        
        
        
        
        
        
            
    }
    void Update () {
		
	}
}
