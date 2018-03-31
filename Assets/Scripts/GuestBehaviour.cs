using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestBehaviour : Behaviour {
    void Update() {
        Node.Status status = child.Tick(Time.deltaTime);      
        if (status == Node.Status.Success) {

        }
        Debug.Log(status);
    }
    void Start () {
        child = new PathfindingAction();
        




    }
}
